using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Application.CQRS.Reports.Query
{
    public class GetReportSummaryQuery : IRequest<SalesSummaryDto>
    {
        public string Status { get; set; } = string.Empty;
        public string ReportType { get; set; } = string.Empty;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GetReportSummaryQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetReportSummaryQuery, SalesSummaryDto>
    {
        public async Task<SalesSummaryDto> Handle(GetReportSummaryQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            if (request.FromDate == null || request.ToDate == null)
            {
                throw new Exception("FromDate or ToDate is null.");
            }

            var baseQuery = context.ProductSales
                .Include(x => x.Invoice)
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .AsNoTracking()
                .Where(x =>
                    (x.Invoice.InvoiceDate >= request.FromDate && x.Invoice.InvoiceDate <= request.ToDate));

            if (!string.IsNullOrWhiteSpace(request.Status) && request.Status != PaymentStatus.All)
            {
                baseQuery = baseQuery.Where(x => x.Invoice.Status == request.Status);
            }

            var salesSummaryResult = new SalesSummaryDto();

            var reportList = new List<SalesSummaryDto>();

            // CATEGORY ONLY
            if (request.ReportType == ReportTypeConst.SalesByCategory)
            {
                reportList = await baseQuery
                    .GroupBy(x => x.Product.Category.Name)
                    .Select(g => new SalesSummaryDto
                    {
                        CategoryName = g.Key,
                        ProductName = null,

                        TotalQuantity = g.Sum(x => x.Quantity),
                        TotalSales = g.Sum(x => x.Subtotal),
                        TotalTax = g.Sum(x => x.TaxAmount),
                        TotalDiscount = g.Sum(x => x.DiscountAmount)
                    })
                    .OrderBy(x => x.CategoryName)
                    .ToListAsync(cancellationToken);

                salesSummaryResult.ReportList = reportList;

                salesSummaryResult.TotalQuantity = reportList.Sum(x => x.TotalQuantity);
                salesSummaryResult.TotalSales = reportList.Sum(x => x.TotalSales);
                salesSummaryResult.TotalTax = reportList.Sum(x => x.TotalTax);
                salesSummaryResult.TotalDiscount = reportList.Sum(x => x.TotalDiscount);

                return salesSummaryResult;
            }

            // PRODUCT (Category + Product)
            if (request.ReportType == ReportTypeConst.SalesByProduct)
            {
                reportList = await baseQuery
                      .GroupBy(x => x.Product.Name)
                  .Select(g => new SalesSummaryDto
                  {
                      ProductName = g.Key,
                      CategoryName = string.Empty,

                      TotalQuantity = g.Sum(x => x.Quantity),
                      TotalSales = g.Sum(x => x.Subtotal),
                      TotalTax = g.Sum(x => x.TaxAmount),
                      TotalDiscount = g.Sum(x => x.DiscountAmount)
                  })
                  .OrderBy(x => x.ProductName)
                  .ToListAsync(cancellationToken);

                salesSummaryResult.ReportList = reportList;

                salesSummaryResult.TotalQuantity = reportList.Sum(x => x.TotalQuantity);
                salesSummaryResult.TotalSales = reportList.Sum(x => x.TotalSales);
                salesSummaryResult.TotalTax = reportList.Sum(x => x.TotalTax);
                salesSummaryResult.TotalDiscount = reportList.Sum(x => x.TotalDiscount);
            }

            return salesSummaryResult;
        }
    }
}
