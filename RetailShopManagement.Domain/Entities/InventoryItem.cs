using RetailShopManagement.Domain.Abstractions;

namespace RetailShopManagement.Domain.Entities;

public class InventoryItem : BaseDerivedEntity<int>
{
    public Guid ProductId { get; set; }

    public decimal QuantityInStock { get; set; }

    public string Unit { get; set; } = null!;

    // Navigation
    public Product Product { get; set; } = null!;
}