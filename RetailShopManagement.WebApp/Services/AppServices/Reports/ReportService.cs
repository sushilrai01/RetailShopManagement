using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Categories.Command;
using RetailShopManagement.Application.CQRS.Categories.Query;
using RetailShopManagement.Application.CQRS.Reports.Query;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;

namespace RetailShopManagement.WebApp.Services.AppServices.Reports
{
    public class ReportService(IMediator mediator) : BaseService(mediator), IReportService
    {
        public async Task<ApiResponse<SalesSummaryDto>> GetSaleReportSummary(string reportType, string paymentStatus = "All",
            DateTime? fromDate = null, DateTime? toDate = null,
            CancellationToken cancellationToken = default)
        {
            var method = "Get Sales Report";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetReportSummaryQuery()
                {
                    ReportType = reportType,
                    Status = paymentStatus,
                    FromDate = fromDate,
                    ToDate = toDate
                }, cancellationToken);

                return new ApiResponse<SalesSummaryDto>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SalesSummaryDto>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }

        }

        public async Task<ApiResponse<IList<PaySlipDto>>> GetPaySlipSummaryAsync(string paymentStatus = "All",
            DateTime? fromDate = null, DateTime? toDate = null,
            CancellationToken cancellationToken = default)
        {
            var method = "Get payslip summary";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetPaySlipHistoryQuery()
                {
                    Status = paymentStatus,
                    FromDate = fromDate,
                    ToDate = toDate
                }, cancellationToken);

                return new ApiResponse<IList<PaySlipDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<PaySlipDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }

        }

    }
}
