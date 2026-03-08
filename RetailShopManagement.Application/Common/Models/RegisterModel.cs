namespace RetailShopManagement.Application.Common.Models;

public class RegisterModel
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Password { get; set; } = null!;
}