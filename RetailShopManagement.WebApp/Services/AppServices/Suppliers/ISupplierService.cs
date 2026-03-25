using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Suppliers
{
    public interface ISupplierService
    {
        Task<ApiResponse<IList<SupplierDto>>> GetAllSuppliersAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<SupplierDto>> GetSupplierByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ApiResponse> CreateSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken = default);
        Task<ApiResponse> UpdateSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken = default);
        Task<ApiResponse> DeleteSupplierAsync(int id);

    }
}
