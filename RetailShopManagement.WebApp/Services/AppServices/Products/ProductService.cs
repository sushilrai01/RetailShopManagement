using MediatR;
using RetailShopManagement.Application.CQRS.Products.Query;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.WebApp.Services.AppServices.Products
{
    public class ProductService(IMediator mediator) : BaseService(mediator), IProductService
    {
        public async Task<IList<Product>> GetAllProductsAsync()
        {
            try
            {
                var result = await Mediator.Send(new GetProductListQuery() { });

                return result;
            }
            catch (Exception ex)
            {
                return new List<Product>();
                //throw new Exception($"An error occurred while retrieving products: {ex.Message}", ex);
            }

        }
    }
}
