using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Infrastructure.Helpers;

public class InventoryHelper(IUserServiceProvider userServiceProvider) : IInventoryHelper
{
    public async Task UpdateInventoryForPurchasesAsync(List<ProductPurchase> purchases, ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (purchases == null || !purchases.Any())
            return;

        var productIds = purchases.Select(p => p.ProductId).Distinct().ToList();

        // Get existing inventory items
        var inventoryItems = await context.InventoryItems
            .Where(i => productIds.Contains(i.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var purchase in purchases)
        {
            var inventoryItem = inventoryItems.FirstOrDefault(i => i.ProductId == purchase.ProductId);

            if (inventoryItem == null)
            {
                // Create new inventory item
                inventoryItem = new InventoryItem
                {
                    ProductId = purchase.ProductId,
                    QuantityInStock = purchase.Quantity,
                    Unit = purchase.Unit,
                    CreatedBy = userServiceProvider.UserName,
                    CreatedOn = DateTime.Now
                };
                context.InventoryItems.Add(inventoryItem);
            }
            else
            {
                // Increment existing inventory
                inventoryItem.QuantityInStock += purchase.Quantity;
            }
        }

    }

    public async Task UpdateInventoryForSalesAsync(List<ProductSale> sales, ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (sales == null || !sales.Any())
            return;

        var productIds = sales.Select(s => s.ProductId).Distinct().ToList();

        // Get existing inventory items
        var inventoryItems = await context.InventoryItems
            .Where(i => productIds.Contains(i.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var sale in sales)
        {
            var inventoryItem = inventoryItems.FirstOrDefault(i => i.ProductId == sale.ProductId);

            if (inventoryItem == null)
            {
                throw new InvalidOperationException($"Inventory item not found for Product ID: {sale.ProductId}");
            }

            if (inventoryItem.QuantityInStock < sale.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock for product '{sale.ProductName}'. Available: {inventoryItem.QuantityInStock}, Required: {sale.Quantity}");
            }

            // Decrement inventory
            inventoryItem.QuantityInStock -= sale.Quantity;
        }

    }

    public async Task ReverseInventoryForPurchasesAsync(List<ProductPurchase> purchases, ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (purchases == null || !purchases.Any())
            return;

        var productIds = purchases.Select(p => p.ProductId).Distinct().ToList();

        var inventoryItems = await context.InventoryItems
            .Where(i => productIds.Contains(i.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var purchase in purchases)
        {
            var inventoryItem = inventoryItems.FirstOrDefault(i => i.ProductId == purchase.ProductId);

            if (inventoryItem == null)
            {
                throw new InvalidOperationException($"Inventory item not found for Product ID: {purchase.ProductId}");
            }

            // Decrement inventory (reverse purchase)
            inventoryItem.QuantityInStock -= purchase.Quantity;

            if (inventoryItem.QuantityInStock < 0)
            {
                throw new InvalidOperationException($"Cannot reverse purchase for product '{purchase.ProductName}'. Would result in negative stock.");
            }
        }

    }

    public async Task ReverseInventoryForSalesAsync(List<ProductSale> sales, ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (sales == null || !sales.Any())
            return;

        var productIds = sales.Select(s => s.ProductId).Distinct().ToList();

        var inventoryItems = await context.InventoryItems
            .Where(i => productIds.Contains(i.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var sale in sales)
        {
            var inventoryItem = inventoryItems.FirstOrDefault(i => i.ProductId == sale.ProductId);

            if (inventoryItem == null)
            {
                throw new InvalidOperationException($"Inventory item not found for Product ID: {sale.ProductId}");
            }

            // Increment inventory (reverse sale)
            inventoryItem.QuantityInStock += sale.Quantity;
        }


    }
}