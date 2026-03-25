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
    public class CreateInvoiceCommand : IRequest<InvoiceResponseModel>
    {
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        public Guid? CreditorId { get; set; }

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
        public IList<ProductSalesDto> InvoiceItems { get; set; } = new List<ProductSalesDto>();
    }

    public class CreateInvoiceCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider,
        IUniqueCodeService uniqueCodeService)
        : IRequestHandler<CreateInvoiceCommand, InvoiceResponseModel>
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async Task<InvoiceResponseModel> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var invoice = new Invoice()
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = await uniqueCodeService.GetUniqueInvoiceNumberAsync(cancellationToken),
                InvoiceDate = request.InvoiceDate,
                DueDate = request.DueDate,
                CreditorId = request.CreditorId,
                Status = PaymentStatus.GetPaymentStatus(request.PaidAmount, request.TotalAmount),
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

                InvoiceItems = request.InvoiceItems.Select(item => new ProductSale
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    UnitPrice = item.UnitPrice,
                    SubTotal = item.SubTotal,
                    CreatedBy = userServiceProvider.UserName,
                    CreatedOn = DateTime.Now
                }).ToList()
            };

            if (request.PaidAmount > 0)
            {
                invoice.PaySlips = new List<PaySlip>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        InvoiceId = invoice.Id,
                        CreditorId = request.CreditorId,
                        AmountPaid = request.PaidAmount,
                        PaymentDate = DateTime.Now,
                        Remarks = "Initial Payment",
                        CreatedBy = userServiceProvider.UserName,
                        CreatedOn = DateTime.Now
                    }
                };
            }

            await context.Invoices.AddAsync(invoice, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new InvoiceResponseModel()
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber
            };
        } 
    }
}
