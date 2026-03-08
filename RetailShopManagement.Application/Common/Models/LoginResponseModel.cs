namespace RetailShopManagement.Application.Common.Models
{
    public class LoginResponseModel
    {
        public string? Username { get; set; }
        public string? AccessToken { get; set; }   
        public string? RefreshToken { get; set; }   
        public int ExpiresIn { get; set; }   
    }
}
