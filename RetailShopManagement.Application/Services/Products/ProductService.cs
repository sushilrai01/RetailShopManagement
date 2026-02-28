using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Services.Products
{
    public class ProductService : IProductService
    {
        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 10.99m,
                    Quantity = 100,
                    Unit = "pcs",
                    CategoryId = 1,
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Category 1"
                    }
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = 20.99m,
                    Quantity = 50,
                    Unit = "pcs",
                    CategoryId = 2,
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Category 2"
                    }
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Description = "Description for Product 3",
                    Price = 5.99m,
                    Quantity = 200,
                    Unit = "pcs",
                    CategoryId = 1,
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Category 1"
                    }
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 4",
                    Description = "Description for Product 4",
                    Price = 15.99m,
                    Quantity = 75,
                    Unit = "pcs",
                    CategoryId = 3,
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Category 3"
                    }
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 5",
                    Description = "Description for Product 5",
                    Price = 8.99m,
                    Quantity = 150,
                    Unit = "pcs",
                    CategoryId = 2,
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Category 2"
                    }
                }

            };
        }
    }
}
