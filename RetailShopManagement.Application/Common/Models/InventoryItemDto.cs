namespace RetailShopManagement.Application.Common.Models;

public class InventoryItemDto
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public decimal QuantityInStock { get; set; }
    public string Unit { get; set; } = null!;
    public DateTime? LastModifiedOn { get; set; }

}