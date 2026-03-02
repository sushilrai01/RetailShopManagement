using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.CQRS.Products.Query
{
    public class GetProductListQuery : IRequest<IList<Product>>

    {

    }

    public class GetProductListQueryHandler(ApplicationDbContext context
    /*, IDbContextFactory<ApplicationDbContext> contextFactory*/)
        : IRequestHandler<GetProductListQuery, IList<Product>>
    {
        public async Task<IList<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            //await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            return await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            //return await productService.GetAllProductsAsync();
        }
    }
}
