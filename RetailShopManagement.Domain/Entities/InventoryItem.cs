using RetailShopManagement.Domain.Abstractions;

namespace RetailShopManagement.Domain.Entities;

public class InventoryItem : BaseDerivedEntity<int>
{
    public int ProductId { get; set; }

    public int QuantityInStock { get; set; }

    // Navigation
    public Product Product { get; set; } = null!;
}