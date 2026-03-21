using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Invoices
{
    public interface IInvoiceService
    {
        //Task<ApiResponse<IList<CreditorDto>>> GetCreditorsAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<InvoicesDto>> GetInvoiceByIdAsync(Guid id);
        Task<ApiResponse<InvoiceResponseModel>> CreateInvoiceAsync(InvoicesDto invoiceForm, CancellationToken cancellationToken = default);
    }
}
