using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Reports
{
    public interface IReportService
    {
        Task<ApiResponse<SalesSummaryDto>> GetSaleReportSummary(string reportType, DateTime? fromDate = null, DateTime? toDate = null, CancellationToken cancellationToken = default);
    }
}
