using RetailShopManagement.Domain.Abstractions;

namespace RetailShopManagement.Domain.Entities;

public class Supplier : BaseDerivedEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    // Navigation
    public ICollection<ProductPurchase> Purchases { get; set; } = new List<ProductPurchase>();
    public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();

}