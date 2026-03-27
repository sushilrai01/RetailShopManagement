using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Purchases
{
    public interface IPurchaseService
    {
        Task<ApiResponse<InvoiceResponseModel>> CreatePurchaseOrderInvoiceAsync(InvoicesDto invoiceForm, CancellationToken cancellationToken = default);
        Task<ApiResponse<InvoiceResponseModel>> UpdatePurchaseOrderInvoiceAsync(InvoicesDto invoiceForm, CancellationToken cancellationToken = default);
        Task<ApiResponse<ProductPurchaseInfoDto>> GetProductPurchaseHistoryAsync(int supplierId, string orderStatus = "All",
            DateTime? fromDate = null, DateTime? toDate = null, CancellationToken ct = default);
    }
}
