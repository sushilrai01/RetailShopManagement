namespace RetailShopManagement.Application.Common.Models;

public class RegisterModel
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = string.Empty;
    public string MobileNo { get; set; } = null!;

    public string Address { get; set; } = null!;
    public bool IsActive { get; set; }
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string Role { get; set; } = "User";
    public string CreatedBy { get; set; } = null!;

}