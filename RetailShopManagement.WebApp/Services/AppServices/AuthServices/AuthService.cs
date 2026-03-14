using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Admin.Command;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;

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
    }
}
