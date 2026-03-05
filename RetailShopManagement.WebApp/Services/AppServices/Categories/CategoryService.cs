using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Categories.Command;
using RetailShopManagement.Application.CQRS.Categories.Query;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;

namespace RetailShopManagement.WebApp.Services.AppServices.Categories
{
    public class CategoryService(IMediator mediator) : BaseService(mediator), ICategoryService
    {
        public async Task<ApiResponse<IList<CategoryDto>>> GetAllCategories(CancellationToken cancellationToken = default)
        {
            var method = "GetAllCategories";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetCategoryListQuery() { }, cancellationToken);

                return new ApiResponse<IList<CategoryDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<CategoryDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }

        }

        public async Task<ApiResponse> CreateCategoryAsync(CategoryDto categoryModel, CancellationToken cancellationToken = default)
        {
            var method = "Create Category";
            var apiAction = ApiAction.Create;

            try
            {
                await Mediator.Send(new CreateCategoryCommand()
                {
                    Name = categoryModel.Name
                }, cancellationToken);

                return new ApiResponse<IList<CategoryDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<CategoryDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }
        }
    }
}
