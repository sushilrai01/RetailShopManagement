namespace RetailShopManagement.Domain.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    // Navigation
    public ICollection<ProductPurchase> Purchases { get; set; } = new List<ProductPurchase>();

}