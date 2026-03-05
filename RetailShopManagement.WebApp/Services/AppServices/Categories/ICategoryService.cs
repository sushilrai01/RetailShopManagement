using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Categories
{
    public interface ICategoryService
    {
        Task<ApiResponse<IList<CategoryDto>>> GetAllCategories(CancellationToken cancellationToken = default);
        Task<ApiResponse> CreateCategoryAsync(CategoryDto categoryModel, CancellationToken cancellationToken = default);
    }
}
