namespace RetailShopManagement.Application.Common.Models;

public class UsersDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = string.Empty;
    public string MobileNo { get; set; } = null!;

    public string Address { get; set; } = null!;
    public bool IsActive { get; set; }
    public string Role { get; set; }  
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedOn { get; set; }  
    public string? LastModifiedBy { get; set; } 
    public DateTime? LastModifiedOn { get; set; } 

}