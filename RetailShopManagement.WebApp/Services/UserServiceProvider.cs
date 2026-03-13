using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.WebApp.Services
{
    public class UserServiceProvider(IHttpContextAccessor httpContextAccessor) : IUserServiceProvider
    {
        public string UserId => httpContextAccessor.HttpContext?.Items["UserId"] as string;
        public string FullName => httpContextAccessor.HttpContext?.Items["FullName"] as string;
        public string UserName => httpContextAccessor.HttpContext?.Items["UserName"] as string;
        public string Email => httpContextAccessor.HttpContext?.Items["Email"] as string;
        public string MobileNo => httpContextAccessor.HttpContext?.Items["MobileNo"] as string;
        public string Role => httpContextAccessor.HttpContext?.Items["Role"] as string;
        public string Address => httpContextAccessor.HttpContext?.Items["Address"] as string;

        public bool IsActive
        {
            get
            {
                var isActive = httpContextAccessor.HttpContext?.Items["IsActive"] as string;
                return isActive == "true";
            }
        }

    }
}
