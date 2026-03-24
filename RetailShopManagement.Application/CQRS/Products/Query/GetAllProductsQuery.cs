
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Application.CQRS.Products.Query
{
    public class GetAllProductsQuery : IRequest<IList<ProductDto>>
    {
        public int? CategoryId { get; set; }
    }

    public class GetAllProductsQueryHandler(
    IDbContextFactory<ApplicationDbContext> contextFactory,
    ICacheService cacheService)
    : IRequestHandler<GetAllProductsQuery, IList<ProductDto>>
    {
        private const string CacheKeyPrefix = CacheKeyConst.ProductPrefix;

        public async Task<IList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"{CacheKeyPrefix}{request.CategoryId?.ToString() ?? CacheKeyConst.All}";

            // Try to get from cache first
            if (cacheService.TryGetValue(cacheKey, out IList<ProductDto> cachedProducts))
            {
                return cachedProducts;
            }

            // Fetch from database
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            IList<ProductDto> products;

            if (request.CategoryId == null)
            {
                products = await context.Products
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
                        CreatedOn = x.CreatedOn,
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name
                    })
                    .OrderBy(x => x.CategoryId)
                    .ToListAsync(cancellationToken);
            }
            else
            {
                products = await context.Products
                    .Include(x => x.Category)
                    .Where(x => x.Category.Id == request.CategoryId)
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
            }

            // Store in cache
            cacheService.Set(cacheKey, products);

            return products;
        }
    }
}
