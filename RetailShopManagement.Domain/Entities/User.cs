using RetailShopManagement.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Domain.Entities
{
    public class User : BaseDerivedEntity<Guid>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
        public string Role { get; set; } = "User";
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
    }
}