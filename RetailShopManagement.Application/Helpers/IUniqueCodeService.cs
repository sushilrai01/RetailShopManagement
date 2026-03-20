using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Helpers
{
    public interface IUniqueCodeService
    {
        Task<string> GetUniqueInvoiceNumberAsync(CancellationToken cancellationToken = default);

    }
}
