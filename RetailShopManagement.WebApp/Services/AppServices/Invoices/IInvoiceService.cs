using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Invoices
{
    public interface IInvoiceService
    {
        Task<ApiResponse<IList<InvoicesDto>>> GetInvoicesListAsync(string status = PaymentStatus.All, Guid? creditorId = null,
            DateTime? fromDate = null, DateTime? toDate = null, bool isPurchase = false,
            CancellationToken cancellationToken = default);
        Task<ApiResponse<InvoicesDto>> GetInvoiceByIdAsync(Guid id);
        Task<ApiResponse<InvoiceResponseModel>> CreateInvoiceAsync(InvoicesDto invoiceForm, CancellationToken cancellationToken = default);
    }
}
