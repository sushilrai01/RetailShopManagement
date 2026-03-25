using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Application.Common.Models;

public class SupplierDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}