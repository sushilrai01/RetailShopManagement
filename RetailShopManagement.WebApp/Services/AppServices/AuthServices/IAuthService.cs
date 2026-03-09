using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResponse<bool>> LoginAsync(int categoryId, DateTime? fromDate = null, DateTime? toDate = null);
        Task<ApiResponse<Guid>> RegisterUserAsync(RegisterModel registerModel);
    }
}
