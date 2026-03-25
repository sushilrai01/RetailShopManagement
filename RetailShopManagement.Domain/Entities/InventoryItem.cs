namespace RetailShopManagement.Domain.Entities;

public class InventoryItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public int QuantityInStock { get; set; }

    // Navigation
    public Product Product { get; set; } = null!;
}