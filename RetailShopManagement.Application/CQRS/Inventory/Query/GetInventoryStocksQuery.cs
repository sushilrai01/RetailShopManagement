using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Inventory.Query
{
    public class GetInventoryStocksQuery : IRequest<IList<InventoryItemDto>>

    {
        public int CategoryId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GetInventoryStocksQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetInventoryStocksQuery, IList<InventoryItemDto>>
    {
        public async Task<IList<InventoryItemDto>> Handle(GetInventoryStocksQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            //var fromDate = request.FromDate ?? DateTime.Now.Date.AddDays(-14);
            //var toDate = request.ToDate ?? DateTime.Now;

            var baseQuery = context.InventoryItems
                .Include(x => x.Product)
                .ThenInclude(p => p.Category)
                .AsNoTracking();

            if (request.CategoryId == 0) //list all
                return await baseQuery
                    .Select(x => new InventoryItemDto()
                    {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        QuantityInStock = x.QuantityInStock,
                        Unit = x.Unit,
                        CategoryName = x.Product.Category.Name,
                        LastModifiedOn = x.LastModifiedOn,
                    })
                    .OrderByDescending(x => x.QuantityInStock)
                    .ToListAsync(cancellationToken);

            return await baseQuery
                .Where(x => x.Product.Category.Id == request.CategoryId)
                .Select(x => new InventoryItemDto()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    QuantityInStock = x.QuantityInStock,
                    Unit = x.Unit,
                    CategoryName = x.Product.Category.Name,
                    LastModifiedOn = x.LastModifiedOn,
                })
                .OrderByDescending(x => x.QuantityInStock)
                .ToListAsync(cancellationToken);

        }
    }
}
