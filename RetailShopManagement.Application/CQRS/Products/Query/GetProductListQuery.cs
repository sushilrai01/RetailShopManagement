using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.CQRS.Products.Query
{
    public class GetProductListQuery : IRequest<IList<ProductDto>>

    {

    }

    public class GetProductListQueryHandler(ApplicationDbContext context
    /*, IDbContextFactory<ApplicationDbContext> contextFactory*/)
        : IRequestHandler<GetProductListQuery, IList<ProductDto>>
    {
        public async Task<IList<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            //await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            return await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name
                })
                .ToListAsync(cancellationToken);

            //return await productService.GetAllProductsAsync();
        }
    }
}
