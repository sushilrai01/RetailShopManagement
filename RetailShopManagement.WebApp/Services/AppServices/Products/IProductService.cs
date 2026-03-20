using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Products
{
    public interface IProductService
    {
        Task<ApiResponse<IList<ProductDto>>> GetAllProductsAsync(int categoryId, DateTime? fromDate = null, DateTime? toDate = null);
        Task<ApiResponse<IList<ProductDto>>> GetAllProductsListAsync(int? categoryId = null);
        Task<ApiResponse<ProductDto>> GetProductByIdAsync(Guid id);
        Task<ApiResponse<Guid>> CreateProductAsync(ProductDto createProductModel);
        Task<ApiResponse> UpdateProductAsync(ProductDto updateProductModel);
        Task<ApiResponse> DeleteProductAsync(Guid id);
    }
}
