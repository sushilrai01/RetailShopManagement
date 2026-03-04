using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Products
{
    public interface IProductService
    {
        Task<ApiResponse<IList<ProductDto>>> GetAllProductsAsync(int categoryId, DateTime? fromDate = null, DateTime? toDate = null);
    }
}
