using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Products.Query;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.WebApp.Services.AppServices.Categories;

namespace RetailShopManagement.WebApp.Services.AppServices.Products
{
    public class ProductService(IMediator mediator) : BaseService(mediator), IProductService
    {
        public async Task<ApiResponse<IList<ProductDto>>> GetAllProductsAsync(int categoryId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                var result = await Mediator.Send(new GetProductListQuery()
                {
                    CategoryId = categoryId,
                    FromDate = fromDate,
                    ToDate = toDate
                });

                return new ApiResponse<IList<ProductDto>>()
                {
                    Message = $"GetAllProductsAsync fetch success.",
                    IsSuccess = true,
                    Title = $"{nameof(GetAllProductsAsync)} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<ProductDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{nameof(GetAllProductsAsync)} Success",
                };                
                //throw new Exception($"An error occurred while retrieving products: {ex.Message}", ex);
            }

        }
    }
}
