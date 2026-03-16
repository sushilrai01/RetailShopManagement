using RetailShopManagement.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Domain.Constants
{
    public static class UserRolesConst
    {
        public static string User => "User";
        public static string Admin => "Admin";


        // DROPDOWN LIST
        public static IList<DropDownField> RolesFields =>
            new List<DropDownField>
            {
                new() { Value = User, Text = User },
                new() { Value = Admin, Text = Admin },
            };
    }
}
