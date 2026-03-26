using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Helpers
{
    public interface IInventoryHelper
    {
        Task UpdateInventoryForPurchasesAsync(List<ProductPurchase> purchases, ApplicationDbContext context, CancellationToken cancellationToken = default);
        Task UpdateInventoryForSalesAsync(List<ProductSale> sales,ApplicationDbContext context, CancellationToken cancellationToken = default);
        Task ReverseInventoryForPurchasesAsync(List<ProductPurchase> purchases,ApplicationDbContext context, CancellationToken cancellationToken = default);
        Task ReverseInventoryForSalesAsync(List<ProductSale> sales,ApplicationDbContext context, CancellationToken cancellationToken = default);
    }
}