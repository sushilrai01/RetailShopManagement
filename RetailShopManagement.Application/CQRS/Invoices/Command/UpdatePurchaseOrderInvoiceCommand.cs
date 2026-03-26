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
    public class UpdatePurchaseOrderInvoiceCommand : IRequest<InvoiceResponseModel>
    {
        public Guid Id { get; set; }
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

    public class UpdatePurchaseOrderInvoiceCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider,
        IInventoryHelper inventoryHelper)
        : IRequestHandler<UpdatePurchaseOrderInvoiceCommand, InvoiceResponseModel>
    {
        public async Task<InvoiceResponseModel> Handle(UpdatePurchaseOrderInvoiceCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            // Begin transaction
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var existingInvoice = await context.Invoices
                    .Include(i => i.PurchaseItems)
                    .Include(i => i.PaySlips)
                    .Include(i => i.Supplier)
                    .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

                if (existingInvoice is null)
                {
                    throw new Exception($"Invoice not found with Id: {request.Id}.");
                }


                existingInvoice.IsPurchaseInvoice = true;
                existingInvoice.SupplierId = request.SupplierId;
                existingInvoice.Status = request.Status;
                existingInvoice.SubTotal = request.SubTotal;
                existingInvoice.TaxAmount = request.TaxAmount;
                existingInvoice.TaxRate = request.TaxRate;
                existingInvoice.DiscountAmount = request.DiscountAmount;
                existingInvoice.DiscountPercent = request.DiscountPercent;
                existingInvoice.TotalAmount = request.TotalAmount;
                existingInvoice.PaidAmount = request.PaidAmount;
                existingInvoice.BalanceAmount = request.BalanceAmount;
                existingInvoice.Remarks = request.Remarks;

                existingInvoice.LastModifiedBy = userServiceProvider.UserName;
                existingInvoice.LastModifiedOn = DateTime.Now;

                if (request.PaidAmount > 0)
                {
                    //existingInvoice.PaySlips.Clear();
                    context.PaySlips.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        InvoiceId = existingInvoice.Id,
                        AmountPaid = request.PaidAmount,
                        PaymentDate = DateTime.Now,
                        Remarks = "Initial Payment",
                        CreatedBy = userServiceProvider.UserName,
                        CreatedOn = DateTime.Now
                    });

                }

                if (request.Status != PaymentStatus.Quoted)
                {
                    var purchaseItems = UpdatePurchaseItems(request, existingInvoice, context);
                    await context.SaveChangesAsync(cancellationToken);

                    await inventoryHelper.UpdateInventoryForPurchasesAsync(purchaseItems, context, cancellationToken);
                }

                await context.SaveChangesAsync(cancellationToken);

                // Commit (make permanent)
                await transaction.CommitAsync(cancellationToken);

                return new InvoiceResponseModel()
                {
                    Id = existingInvoice.Id,
                    InvoiceNumber = existingInvoice.InvoiceNumber
                };
            }
            catch (Exception ex)
            {
                // Rollback (undo everything)
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private List<ProductPurchase> UpdatePurchaseItems(UpdatePurchaseOrderInvoiceCommand request, Invoice existingInvoice,
            ApplicationDbContext context)
        {
            var existingItems = existingInvoice.PurchaseItems
                                                .Where(x => x.InvoiceId == request.Id)
                                                .ToList();
            var requestItems = request.PurchaseItems.ToList();

            var requestItemsIds = requestItems
                                            .Where(x => !x.Id.Empty())
                                            .Select(x => x.Id)
                                            .ToHashSet();

            // Items to delete (exist in DB but not in request)
            var itemsToDelete = existingItems
                .Where(existing => !requestItemsIds.Contains(existing.Id))
                .ToList();

            context.ProductPurchases.RemoveRange(itemsToDelete);
            //foreach (var item in itemsToDelete)
            //{
            //    var existsInDb = context.ProductPurchases
            //        .Any(x => x.Id == item.Id);

            //    if (existsInDb)
            //    {
            //        context.ProductPurchases.Remove(item);
            //    }
            //}

            var purchaseItems = new List<ProductPurchase>();
            foreach (var requestItem in requestItems)
            {
                var existingItem = existingItems.FirstOrDefault(e => e.Id == requestItem.Id);

                if (existingItem != null)
                {
                    // Update existing item
                    existingItem.Quantity = requestItem.Quantity;
                    existingItem.UnitPrice = requestItem.UnitPrice;
                    existingItem.SubTotal = requestItem.SubTotal;
                    existingItem.Unit = requestItem.Unit;
                    existingItem.ProductName = requestItem.ProductName;
                    existingItem.LastModifiedBy = userServiceProvider.UserName;
                    existingItem.LastModifiedOn = DateTime.Now;

                    purchaseItems.Add(existingItem);
                }
                else
                {
                    var newItem = new ProductPurchase()
                    {
                        Id = Guid.NewGuid(),
                        InvoiceId = existingInvoice.Id,
                        ProductId = requestItem.ProductId,
                        SupplierId = request.SupplierId,
                        ProductName = requestItem.ProductName,
                        Quantity = requestItem.Quantity,
                        Unit = requestItem.Unit,
                        UnitPrice = requestItem.UnitPrice,
                        SubTotal = requestItem.SubTotal,
                        CreatedBy = userServiceProvider.UserName,
                        CreatedOn = DateTime.Now,

                    };
                    // Add new item
                    context.ProductPurchases.Add(newItem);

                    purchaseItems.Add(newItem);
                }
            }

            return purchaseItems;
        }
    }
}
