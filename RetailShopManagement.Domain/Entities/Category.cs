using RetailShopManagement.Domain.Abstractions;

namespace RetailShopManagement.Domain.Entities;

public class Category: BaseDerivedEntity<int>
{
    public string Name { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}