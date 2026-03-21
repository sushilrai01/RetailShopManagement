using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Application.CQRS.Creditors.Query
{
    public class GetCreditorsListQuery : IRequest<IList<CreditorDto>>

    {

    }

    public class GetCreditorsListQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetCreditorsListQuery, IList<CreditorDto>>
    {
        public async Task<IList<CreditorDto>> Handle(GetCreditorsListQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var creditorsList = await context.Creditors
                .Include(x => x.PaySlips)
                .Include(x => x.Invoices)
                .AsNoTracking()
                .Select(x => new
                {
                    x,
                    TotalAmount = x.Invoices.Sum(y => (decimal?)y.BalanceAmount) ?? 0,
                    PaidAmount = x.PaySlips.Sum(y => (decimal?)y.AmountPaid) ?? 0,
                    DueAmount = (x.Invoices.Sum(y => (decimal?)y.BalanceAmount) ?? 0)
                                - (x.PaySlips.Sum(y => (decimal?)y.AmountPaid) ?? 0),
                })
                .Select(t => new CreditorDto()
                {
                    Id = t.x.Id,
                    FullName = t.x.FullName,
                    MobileNo = t.x.MobileNo,
                    Email = t.x.Email,
                    Address = t.x.Address,
                    Status = PaymentStatus.GetPaymentStatus(t.PaidAmount, t.TotalAmount),

                    TotalAmount = t.TotalAmount,
                    PaidAmount = t.PaidAmount,
                    DueAmount = t.DueAmount,

                    CreatedOn = t.x.CreatedOn,
                    CreatedBy = t.x.CreatedBy,
                    LastModifiedOn = t.x.LastModifiedOn,
                    LastModifiedBy = t.x.LastModifiedBy ?? string.Empty,

                    PaySlips = t.x.PaySlips.Select(y => new PaySlipDto()
                    {
                        Id = y.Id,
                        AmountPaid = y.AmountPaid,
                        PaymentDate = y.PaymentDate,
                        Remarks = y.Remarks
                    }).ToList(),

                    //TODO: Incomplete mapping of InvoiceItems; separated query
                    InvoiceLists = t.x.Invoices.Select(y => new InvoicesDto()
                    {
                        Id = y.Id,
                        InvoiceNumber = y.InvoiceNumber,
                        InvoiceDate = y.InvoiceDate,
                        DueDate = y.DueDate,
                        CreditorId = y.CreditorId,
                        SubTotal = y.SubTotal,
                        TaxAmount = y.TaxAmount,
                        DiscountAmount = y.DiscountAmount,
                        TotalAmount = y.TotalAmount,
                        PaidAmount = y.PaidAmount,
                        BalanceAmount = y.BalanceAmount,

                        Status = y.Status,
                        Remarks = y.Remarks,
                        SupplierId = y.SupplierId,

                        CreatedOn = y.CreatedOn,
                        CreatedBy = y.CreatedBy,
                        LastModifiedOn = y.LastModifiedOn,
                        LastModifiedBy = y.LastModifiedBy ?? string.Empty,

                        //InvoiceItems = y.InvoiceItems.Select(z => new ProductSalesDto()
                        //{
                        //    Id = z.Id,
                        //    ProductId = z.ProductId,
                        //    ProductName = z.ProductName,
                        //    Quantity = z.Quantity,
                        //    UnitPrice = z.UnitPrice,
                        //    Subtotal = z.Subtotal
                        //}).ToList()

                    }).ToList()

                }).OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

            return creditorsList;
        }
    }
}
