using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Categories.Query;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Categories
{
    public class CategoryService(IMediator mediator) : BaseService(mediator), ICategoryService
    {
        public async Task<ApiResponse<IList<CategoryDto>>> GetAllCategories(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(new GetCategoryListQuery() { },cancellationToken);

                return new ApiResponse<IList<CategoryDto>>()
                {
                    Message = $"GetAllCategories fetch success.",
                    IsSuccess = true,
                    Title = $"{nameof(GetAllCategories)} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<CategoryDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{nameof(GetAllCategories)} Success",
                };                
            }

        }
    }
}
