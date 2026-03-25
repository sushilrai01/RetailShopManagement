using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Inventory.Query;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;

namespace RetailShopManagement.WebApp.Services.AppServices.Inventory
{
    public class InventoryService(IMediator mediator) : BaseService(mediator), IInventoryService
    {
        public async Task<ApiResponse<IList<InventoryItemDto>>> GetInventoryStockAsync(int categoryId, DateTime? fromDate = null, DateTime? toDate = null, CancellationToken cancellationToken = default)
        {
            var method = "Get inventory stocks";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetInventoryStocksQuery()
                {
                    CategoryId = categoryId,
                    FromDate = fromDate,
                    ToDate = toDate
                }, cancellationToken);

                return new ApiResponse<IList<InventoryItemDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<InventoryItemDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }

        }

    }
}
