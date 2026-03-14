using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResponse> LoginAsync(string email, string password);
        Task<ApiResponse<UserRegisterResponseModel>> RegisterUserAsync(RegisterModel registerModel);
    }
}
