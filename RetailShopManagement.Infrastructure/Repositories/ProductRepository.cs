using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
//using RetailShopManagement.Domain.RepositoryInterfaces;
using RetailShopManagement.Infrastructure.Persistence;

//namespace RetailShopManagement.Infrastructure.Repositories
//{
    //public class ProductRepository(ApplicationDbContext context) : IProductRepository
    //{
    //    public async Task<IList<Product>> GetAllProductsAsync()
    //    {
    //        return await context.Products
    //            .Include(x => x.Category)
    //            .AsNoTracking()
    //            .ToListAsync();
    //    }
    //}
//}
