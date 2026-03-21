using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.WebApp.Services.AppServices.Creditors
{
    public interface ICreditorService
    {
        Task<ApiResponse<IList<CreditorDto>>> GetCreditorsAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<Guid>> CreateCreditorAsync(CreditorDto creditorModel, CancellationToken cancellationToken = default);
        Task<ApiResponse<Guid>> CreatePaySlipAsync(PaySlipDto paySlipDto, CancellationToken cancellationToken = default);
    }
}
