using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Products.Command;
using RetailShopManagement.Application.CQRS.Products.Query;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;
using RetailShopManagement.WebApp.Services.AppServices.Categories;

namespace RetailShopManagement.WebApp.Services.AppServices.Products
{
    public class ProductService(IMediator mediator) : BaseService(mediator), IProductService
    {
        public async Task<ApiResponse<IList<ProductDto>>> GetAllProductsAsync(int categoryId, DateTime? fromDate = null, DateTime? toDate = null)
        {

            var method = "Get All Products";
            var apiAction = ApiAction.Fetch;

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
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<ProductDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                };
                //throw new Exception($"An error occurred while retrieving products: {ex.Message}", ex);
            }

        }
        public async Task<ApiResponse<ProductDto>> GetProductByIdAsync(Guid id)
        {

            var method = "Get Product By Id";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetProductByIdQuery()
                {
                    Id = id
                });

                return new ApiResponse<ProductDto>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductDto>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                };
            }

        }

        public async Task<ApiResponse<Guid>> CreateProductAsync(ProductDto createProductModel)
        {
            var method = "Create Product";
            var apiAction = ApiAction.Create;

            try
            {
                var result = await Mediator.Send(new CreateProductCommand()
                {
                    Name = createProductModel.Name,
                    CategoryId = createProductModel.CategoryId,
                    Description = createProductModel.Description,
                    Price = createProductModel.Price,
                    Quantity = createProductModel.Quantity,
                    Unit = createProductModel.Unit
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
        public async Task<ApiResponse> UpdateProductAsync(ProductDto updateProductModel)
        {
            var method = "Update Product";
            var apiAction = ApiAction.Update;

            try
            {
                await Mediator.Send(new UpdateProductCommand()
                {
                    Id = updateProductModel.Id,
                    Name = updateProductModel.Name,
                    CategoryId = updateProductModel.CategoryId,
                    Description = updateProductModel.Description,
                    Price = updateProductModel.Price,
                    Quantity = updateProductModel.Quantity,
                    Unit = updateProductModel.Unit
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

        public async Task<ApiResponse> DeleteProductAsync(Guid id)
        {
            var method = "Delete Product";
            var apiAction = ApiAction.Delete;

            try
            {
                await Mediator.Send(new DeleteProductCommand()
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
    }
}
