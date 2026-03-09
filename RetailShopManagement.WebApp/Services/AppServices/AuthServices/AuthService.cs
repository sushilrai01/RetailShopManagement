using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Admin.Command;
using RetailShopManagement.Application.CQRS.Products.Command;
using RetailShopManagement.Application.CQRS.Products.Query;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;
using RetailShopManagement.WebApp.Services.AppServices.Categories;

namespace RetailShopManagement.WebApp.Services.AppServices.AuthServices
{
    public class AuthService(IMediator mediator) : BaseService(mediator), IAuthService
    {
        public async Task<ApiResponse<bool>> LoginAsync(int categoryId, DateTime? fromDate = null, DateTime? toDate = null)
        {

            var method = "Login";
            var apiAction = ApiAction.Fetch;

            try
            {
                //TODO: Implement actual login logic here, such as validating user credentials and generating a token.
                var result = await Mediator.Send(new GetProductListQuery()
                {
                    CategoryId = categoryId,
                    FromDate = fromDate,
                    ToDate = toDate
                });

                return new ApiResponse<bool>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                    Data = false
                };
                //throw new Exception($"An error occurred while retrieving products: {ex.Message}", ex);
            }

        }

        public async Task<ApiResponse<Guid>> RegisterUserAsync(RegisterModel registerModel)
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

                return new ApiResponse<Guid>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                 };
            }
        }
    }
}
