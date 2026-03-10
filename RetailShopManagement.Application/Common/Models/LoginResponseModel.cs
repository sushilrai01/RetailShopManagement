namespace RetailShopManagement.Application.Common.Models
{
    public class LoginResponseModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
        public string Role { get; set; }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
