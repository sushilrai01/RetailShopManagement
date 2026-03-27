using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Application.CQRS.Purchases.Query
{
    public class GetProductPurchaseListQuery : IRequest<ProductPurchaseInfoDto>
    {
        public string OrderStatus { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GetProductPurchaseListQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetProductPurchaseListQuery, ProductPurchaseInfoDto>
    {
        public async Task<ProductPurchaseInfoDto> Handle(GetProductPurchaseListQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var fromDate = request.FromDate ?? DateTime.Now.Date.AddDays(-14);
            var toDate = request.ToDate ?? DateTime.Now;

            var baseQuery = context.ProductPurchases
                .Include(x => x.Invoice)
                .Include(x => x.Product)
                .Include(x => x.Supplier)
                .Where(x => x.CreatedOn >= fromDate && x.CreatedOn < toDate.Date.AddDays(1))
                .AsNoTracking();

            if (request.OrderStatus == PaymentStatus.Quoted)
            {
                baseQuery = baseQuery.Where(x => x.Invoice.Status == request.OrderStatus);
            }

            if (request.SupplierId != 0)
            {
                baseQuery = baseQuery.Where(x => x.Supplier.Id == request.SupplierId);
            }

            var purchaseItemsList = await baseQuery
                .Select(x => new ProductPurchaseDto()
                {
                    Id = x.Id,
                    SupplierId = x.SupplierId,
                    SupplierName = x.Supplier.Name,
                    InvoiceId = x.InvoiceId,
                    InvoiceNumber = x.Invoice.InvoiceNumber,
                    ProductName = x.Product.Name,
                    Unit = x.Unit,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                    SubTotal = x.SubTotal,
                    Notes = x.Notes,
                    CreatedOn = x.CreatedOn
                })
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

            return new ProductPurchaseInfoDto()
            {
                TotalQuantity = purchaseItemsList.Sum(x => x.Quantity),
                GrandTotal = purchaseItemsList.Sum(x => x.SubTotal),
                PurchaseItems = purchaseItemsList
            };
        }
    }
}
