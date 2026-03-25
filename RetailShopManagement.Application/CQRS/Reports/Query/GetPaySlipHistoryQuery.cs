using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Application.CQRS.Reports.Query
{
    public class GetPaySlipHistoryQuery : IRequest<IList<PaySlipDto>>
    {
        public string Status { get; set; } = string.Empty;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsPurchase { get; set; } // true for purchase, false for sales
    }

    public class GetPaySlipHistoryQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetPaySlipHistoryQuery, IList<PaySlipDto>>
    {
        public async Task<IList<PaySlipDto>> Handle(GetPaySlipHistoryQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            if (request.FromDate == null || request.ToDate == null)
            {
                throw new Exception("FromDate or ToDate is null.");
            }

            var baseQuery = context.PaySlips
                .Include(x => x.Invoice)
                .Include(x => x.Creditor)
                .AsNoTracking()
                .Where(x =>
                    (x.PaymentDate >= request.FromDate && x.PaymentDate <= request.ToDate));

            if (!string.IsNullOrWhiteSpace(request.Status) && request.Status != PaymentStatus.All)
            {
                baseQuery = baseQuery.Where(x => x.Invoice!.Status == request.Status);
            }

            baseQuery = request.IsPurchase
                        ? baseQuery.Where(x => x.Invoice!.IsPurchaseInvoice)
                        : baseQuery.Where(x => !x.Invoice!.IsPurchaseInvoice);

            var paySlipHistory = await baseQuery.Select(x => new PaySlipDto()
            {
                Id = x.Id,
                CreditorName = x.Creditor != null ? x.Creditor.FullName : "Initial Payment",
                InvoiceNumber = x.Invoice!.InvoiceNumber,
                PaymentDate = x.PaymentDate,
                AmountPaid = x.AmountPaid,
                Remarks = x.Remarks,

            })
            .OrderByDescending(x => x.PaymentDate)
            .ToListAsync(cancellationToken);

            return paySlipHistory;
        }
    }
}
