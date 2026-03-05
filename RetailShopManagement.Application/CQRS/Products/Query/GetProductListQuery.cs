using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Application.CQRS.Products.Query
{
    public class GetProductListQuery : IRequest<IList<ProductDto>>

    {
        public int CategoryId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GetProductListQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetProductListQuery, IList<ProductDto>>
    {
        public async Task<IList<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var fromDate = request.FromDate ?? DateTime.Now.Date.AddDays(-14);
            var toDate = request.ToDate ?? DateTime.Now;

            return await context.Products
                .Include(x => x.Category)
                .Where(x => x.Category.Id == request.CategoryId &&
                            (x.CreatedOn >= fromDate && x.CreatedOn < toDate.Date.AddDays(1)))
                .AsNoTracking()
                .Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                    CreatedOn = x.CreatedOn,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name
                })
                .OrderBy(x => x.CategoryId)
                .ToListAsync(cancellationToken);

            //return await productService.GetAllProductsAsync();
        }
    }
}
