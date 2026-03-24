using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Reports
{
    public interface IReportService
    {
        Task<ApiResponse<SalesSummaryDto>> GetSaleReportSummary(string reportType, string paymentStatus = "All",
            DateTime? fromDate = null, DateTime? toDate = null,
            CancellationToken cancellationToken = default);
    }
}
