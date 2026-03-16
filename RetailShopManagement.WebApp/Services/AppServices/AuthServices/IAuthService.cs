using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResponse> LoginAsync(string email, string password);
        Task<ApiResponse<UserRegisterResponseModel>> RegisterUserAsync(RegisterModel registerModel);
        Task<ApiResponse> UpdateUserAsync(UsersDto userUpdateModel);
        Task<ApiResponse> DeleteUserAsync(Guid id);
        Task<ApiResponse> ResetPasswordAsync(Guid id);
        Task<ApiResponse<IList<UsersDto>>> GetUsersAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<ApiResponse<UsersDto>> GetUserByIdAsync(Guid id);

    }
}
