using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Extensions;

namespace RetailShopManagement.Application.CQRS.Invoices.Query
{
    public class GetInvoicesQuery : IRequest<IList<InvoicesDto>>
    {
        public Guid? CreditorId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GetInvoicesQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetInvoicesQuery, IList<InvoicesDto>>
    {
        public async Task<IList<InvoicesDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var query = context.Invoices
                .Include(x => x.Creditor)
                .Include(x => x.InvoiceItems)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Status) && request.Status != PaymentStatus.All)
            {
                query = query.Where(x => x.Status == request.Status);
            }

            if (!request.CreditorId.Empty())
            {
                query = query.Where(x => x.CreditorId == request.CreditorId);
            }

            if (request is { FromDate: not null, ToDate: not null })
            {
                query = query.Where(x => x.InvoiceDate >= request.FromDate && x.InvoiceDate < request.ToDate);
            }

            var invoices = await query
                .Select(x => new InvoicesDto()
                {
                    Id = x.Id,
                    InvoiceNumber = x.InvoiceNumber,
                    InvoiceDate = x.InvoiceDate,
                    DueDate = x.DueDate,
                    CreditorId = x.CreditorId,
                    CreditorName = x.Creditor != null ? x.Creditor.FullName : string.Empty,
                    SubTotal = x.SubTotal,
                    TaxAmount = x.TaxAmount,
                    TaxRate = x.TaxRate,
                    DiscountPercent = x.DiscountPercent,
                    DiscountAmount = x.DiscountAmount,
                    TotalAmount = x.TotalAmount,
                    PaidAmount = x.PaidAmount,
                    BalanceAmount = x.BalanceAmount,

                    Status = PaymentStatus.GetPaymentStatus(x.PaidAmount, x.TotalAmount),
                    Remarks = x.Remarks,
                    SupplierId = x.SupplierId,

                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    LastModifiedBy = x.LastModifiedBy ?? string.Empty,

                    InvoiceItems = x.InvoiceItems.Select(z => new ProductSalesDto()
                    {
                        Id = z.Id,
                        ProductId = z.ProductId,
                        ProductName = z.ProductName,
                        Unit = z.Unit,
                        Quantity = z.Quantity,
                        UnitPrice = z.UnitPrice,
                        Subtotal = z.Subtotal,
                        Notes = z.Notes
                    }).ToList()

                })
                .OrderByDescending(x => x.InvoiceNumber)
                .ToListAsync(cancellationToken);

            return invoices;
        }
    }
}
