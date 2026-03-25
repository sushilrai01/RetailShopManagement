using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Products.Command;
using RetailShopManagement.Application.CQRS.Suppliers.Command;
using RetailShopManagement.Application.CQRS.Suppliers.Query;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;

namespace RetailShopManagement.WebApp.Services.AppServices.Suppliers
{
    public class SupplierService(IMediator mediator) : BaseService(mediator), ISupplierService
    {
        public async Task<ApiResponse<IList<SupplierDto>>> GetAllSuppliersAsync(CancellationToken cancellationToken = default)
        {
            var method = "Get all suppliers";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetSupplierListQuery() { }, cancellationToken);

                return new ApiResponse<IList<SupplierDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<SupplierDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }

        }

        public async Task<ApiResponse<SupplierDto>> GetSupplierByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var method = "Get supplier by Id";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetSupplierByIdQuery()
                {
                    Id = id
                }, cancellationToken);

                return new ApiResponse<SupplierDto>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SupplierDto>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }

        }

        public async Task<ApiResponse> CreateSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken = default)
        {
            var method = "Create Supplier";
            var apiAction = ApiAction.Create;

            try
            {
                await Mediator.Send(new CreateSupplierCommand()
                {
                    Name = supplierDto.Name,
                    Email = supplierDto.Email,
                    Address = supplierDto.Address,
                    Phone = supplierDto.Phone
                }, cancellationToken);

                return new ApiResponse<IList<SupplierDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<SupplierDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }
        }

        public async Task<ApiResponse> UpdateSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken = default)
        {
            var method = "Update Supplier";
            var apiAction = ApiAction.Update;

            try
            {
                await Mediator.Send(new UpdateSupplierCommand()
                {
                    Id = supplierDto.Id,
                    Name = supplierDto.Name,
                    Email = supplierDto.Email,
                    Address = supplierDto.Address,
                    Phone = supplierDto.Phone
                }, cancellationToken);

                return new ApiResponse<IList<SupplierDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<SupplierDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }
        }

        public async Task<ApiResponse> DeleteSupplierAsync(int id)
        {
            var method = "Delete Supplier";
            var apiAction = ApiAction.Delete;

            try
            {
                await Mediator.Send(new DeleteSupplierCommand()
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
