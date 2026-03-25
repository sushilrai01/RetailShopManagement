using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Invoices.Query
{
    public class GetInvoiceByIdQuery : IRequest<InvoicesDto>
    {
        public Guid Id { get; set; }
    }

    public class GetInvoiceByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetInvoiceByIdQuery, InvoicesDto>
    {
        public async Task<InvoicesDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var invoice = await context.Invoices
                .Include(x => x.Creditor)
                .Include(x => x.InvoiceItems)
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
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
                    
                    Status = x.Status,
                    Remarks = x.Remarks,
                    
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
                        SubTotal = z.SubTotal,
                        Notes = z.Notes
                    }).ToList(),

                    PaySlipHistory = x.PaySlips.Select(y => new PaySlipDto()
                    {
                        Id = y.Id,
                        AmountPaid = y.AmountPaid,
                        PaymentDate = y.PaymentDate,
                        Remarks = y.Remarks
                    }).OrderBy(y => y.PaymentDate).ToList(),

                }).FirstOrDefaultAsync(cancellationToken);

            return invoice ?? throw new Exception($"Invoice not found with id: {request.Id}");
        }
    }
}
