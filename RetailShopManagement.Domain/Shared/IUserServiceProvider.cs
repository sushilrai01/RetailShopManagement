using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Domain.Shared
{
    public interface IUserServiceProvider
    {
        string UserId { get; }
        string FullName { get; }
        string UserName { get; }
        string Email { get; }
        string MobileNo { get; }
        bool IsActive { get; }
        string Role { get; }
        string Address { get; }

    }
}
