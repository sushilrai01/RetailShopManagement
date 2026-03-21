
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RetailShopManagement.Application.Common.Models;
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
    IMemoryCache memoryCache)
    : IRequestHandler<GetAllProductsQuery, IList<ProductDto>>
    {
        private const string CacheKeyPrefix = "Products_";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);

        public async Task<IList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // Generate cache key based on CategoryId
            string cacheKey = $"{CacheKeyPrefix}{request.CategoryId?.ToString() ?? "All"}";

            // Try to get from cache first
            if (memoryCache.TryGetValue(cacheKey, out IList<ProductDto> cachedProducts))
            {
                return cachedProducts;
            }

            // If not in cache, fetch from database
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
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(CacheExpiration)
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            memoryCache.Set(cacheKey, products, cacheOptions);

            return products;
        }
    }
}
