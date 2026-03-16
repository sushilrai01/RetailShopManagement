using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Admin.Command;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;
using System.Security.Claims;
using RetailShopManagement.Application.CQRS.Admin.Query;

namespace RetailShopManagement.WebApp.Services.AppServices.AuthServices
{
    public class AuthService(IMediator mediator, IHttpContextAccessor httpContextAccessor) : BaseService(mediator), IAuthService
    {
        public async Task<ApiResponse> LoginAsync(string email, string password)
        {

            var method = "Login";
            var apiAction = ApiAction.Fetch;

            #region login command
            try
            {
                // 1. Send command through MediatR
                var response = await Mediator.Send(new LoginUserCommand
                {
                    Username = email,
                    Password = password
                });

                if (!response.IsSuccess)
                    return new ApiResponse { IsSuccess = false, Message = "Invalid email or password" };

                // 2. Build claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,  response.Username),
                    new Claim(ClaimTypes.Email, response.Email),
                    new Claim(ClaimTypes.Role,  response.Role),
                    new Claim("UserId",         response.Id.ToString()),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // 3. Sign in — writes Set-Cookie directly to the browser response
                var httpContext = httpContextAccessor.HttpContext
                                  ?? throw new InvalidOperationException("No active HttpContext");

                await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                });

                return new ApiResponse { IsSuccess = true, Message = "Signed in successfully" };

            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed"
                };
                //throw new Exception($"An error occurred while retrieving products: {ex.Message}", ex);
            }

            #endregion

        }

        public async Task<ApiResponse<UserRegisterResponseModel>> RegisterUserAsync(RegisterModel registerModel)
        {
            var method = "Register user";
            var apiAction = ApiAction.Create;

            try
            {
                var result = await Mediator.Send(new UserRegisterCommand()
                {
                    Username = registerModel.Username,
                    Email = registerModel.Email,
                    FullName = registerModel.FullName,
                    Address = registerModel.Address,
                    CreatedBy = registerModel.CreatedBy,
                    IsActive = registerModel.IsActive,
                    MobileNo = registerModel.MobileNo,
                    Password = registerModel.Password,
                    Role = registerModel.Role
                });

                return new ApiResponse<UserRegisterResponseModel>()
                {
                    Message = result.Message,
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserRegisterResponseModel>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                };
            }
        }

        public async Task<ApiResponse> UpdateUserAsync(UsersDto userUpdateModel)
        {

            var method = "Update user";
            var apiAction = ApiAction.Update;

            try
            {
                await Mediator.Send(new UserUpdateCommand()
                {
                    Id = userUpdateModel.Id,
                    Username = userUpdateModel.Username,
                    Email = userUpdateModel.Email,
                    FullName = userUpdateModel.FullName,
                    Address = userUpdateModel.Address,
                    IsActive = userUpdateModel.IsActive,
                    MobileNo = userUpdateModel.MobileNo,
                    Role = userUpdateModel.Role
                });

                return new ApiResponse()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                };
            }
        }
        public async Task<ApiResponse> DeleteUserAsync(Guid id)
        {
            var method = "Delete User";
            var apiAction = ApiAction.Delete;

            try
            {
                await Mediator.Send(new DeleteUserCommand()
                {
                    Id = id,
                });

                return new ApiResponse()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                };
            }
        }
        public async Task<ApiResponse<IList<UsersDto>>> GetUsersAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {

            var method = "Get Users";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetUsersListQuery()
                {
                    FromDate = fromDate,
                    ToDate = toDate
                });

                return new ApiResponse<IList<UsersDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<UsersDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                };
            }
        }

        public async Task<ApiResponse<UsersDto>> GetUserByIdAsync(Guid id)
        {
            var method = "Get User By Id";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetUserByIdQuery()
                {
                    Id = id
                });

                return new ApiResponse<UsersDto>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UsersDto>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                };
            }
        }
    }
}
