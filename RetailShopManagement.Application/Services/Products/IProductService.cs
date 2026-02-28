using RetailShopManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Application.Services.Products
{
    public interface IProductService
    {
        Task<IList<Product>> GetAllProductsAsync();
    }
}
