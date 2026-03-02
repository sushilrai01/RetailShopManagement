using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.WebApp.Services.AppServices.Products
{
    public interface IProductService
    {
        Task<IList<Product>> GetAllProductsAsync();
    }
}
