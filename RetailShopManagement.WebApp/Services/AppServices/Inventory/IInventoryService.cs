using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Inventory
{
    public interface IInventoryService
    {
        Task<ApiResponse<IList<InventoryItemDto>>> GetInventoryStockAsync(int categoryId, DateTime? fromDate = null, DateTime? toDate = null, CancellationToken cancellationToken = default);
    }
}
