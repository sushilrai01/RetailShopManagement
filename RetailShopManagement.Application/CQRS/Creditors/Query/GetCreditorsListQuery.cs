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
                .Select(x => new CreditorDto()
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    MobileNo = x.MobileNo,
                    Email = x.Email,
                    Address = x.Address,
                    Status = PaymentStatus.GetPaymentStatus(x.TotalPaid, x.TotalAmount),

                    TotalAmount = x.TotalAmount,
                    PaidAmount = x.TotalPaid,
                    DueAmount = x.DueAmount,

                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    LastModifiedBy = x.LastModifiedBy ?? string.Empty,

                    PaySlips = x.PaySlips.Select(y => new PaySlipDto()
                    {
                        Id = y.Id,
                        AmountPaid = y.AmountPaid,
                        PaymentDate = y.PaymentDate,
                        Remarks = y.Remarks
                    }).ToList(),

                    //TODO: Incomplete mapping of InvoiceItems; separated query
                    InvoiceLists = x.Invoices.Select(y => new InvoicesDto()
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
