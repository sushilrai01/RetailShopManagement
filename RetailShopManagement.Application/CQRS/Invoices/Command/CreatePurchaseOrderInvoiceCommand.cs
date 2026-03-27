using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Extensions;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Application.CQRS.Invoices.Command
{
    public class CreatePurchaseOrderInvoiceCommand : IRequest<InvoiceResponseModel>
    {
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        public int SupplierId { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPercent { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal BalanceAmount { get; set; }

        public string Status { get; set; } = PaymentStatus.Pending;

        public string? Remarks { get; set; }

        //List of Invoice Items
        public IList<ProductPurchaseDto> PurchaseItems { get; set; } = new List<ProductPurchaseDto>();
    }

    public class CreatePurchaseOrderInvoiceCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider,
        IInventoryHelper inventoryHelper,
        IUniqueCodeService uniqueCodeService)
        : IRequestHandler<CreatePurchaseOrderInvoiceCommand, InvoiceResponseModel>
    {
        public async Task<InvoiceResponseModel> Handle(CreatePurchaseOrderInvoiceCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            // Begin transaction
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var invoice = new Invoice()
                {
                    Id = Guid.NewGuid(),

                    InvoiceNumber = await uniqueCodeService.GetUniquePurchaseOrderInvoiceNumberAsync(cancellationToken),
                    IsPurchaseInvoice = true,

                    InvoiceDate = request.InvoiceDate,
                    DueDate = request.DueDate,
                    SupplierId = request.SupplierId,
                    Status = request.Status,
                    SubTotal = request.SubTotal,
                    TaxAmount = request.TaxAmount,
                    TaxRate = request.TaxRate,
                    DiscountAmount = request.DiscountAmount,
                    DiscountPercent = request.DiscountPercent,
                    TotalAmount = request.TotalAmount,
                    PaidAmount = request.PaidAmount,
                    BalanceAmount = request.BalanceAmount,
                    Remarks = request.Remarks,

                    CreatedBy = userServiceProvider.UserName,
                    CreatedOn = DateTime.Now,

                };

                if (request.PaidAmount > 0)
                {
                    invoice.PaySlips = new List<PaySlip>
                    {
                        new()
                        {
                            Id = Guid.NewGuid(),
                            InvoiceId = invoice.Id,
                            AmountPaid = request.PaidAmount,
                            PaymentDate = DateTime.Now,
                            Remarks = "Initial Payment",
                            CreatedBy = userServiceProvider.UserName,
                            CreatedOn = DateTime.Now
                        }
                    };
                }

                invoice.PurchaseItems = request.PurchaseItems.Select(item => new ProductPurchase()
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    SupplierId = request.SupplierId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    UnitPrice = item.UnitPrice,
                    SubTotal = item.SubTotal,
                    CreatedBy = userServiceProvider.UserName,
                    CreatedOn = DateTime.Now
                }).ToList();

                if (request.Status != PaymentStatus.Quoted)
                {
                    await inventoryHelper.UpdateInventoryForPurchasesAsync(invoice.PurchaseItems.ToList(),context, cancellationToken);
                }

                await context.Invoices.AddAsync(invoice, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                // Commit (make permanent)
                await transaction.CommitAsync(cancellationToken);

                return new InvoiceResponseModel()
                {
                    Id = invoice.Id,
                    InvoiceNumber = invoice.InvoiceNumber
                };
            }
            catch (Exception ex)
            {
                // Rollback (undo everything)
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
