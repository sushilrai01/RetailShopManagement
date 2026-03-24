using RetailShopManagement.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Domain.Constants
{
    public static class CacheKeyConst
    {
        public const string ProductPrefix = "Products_";

        public const string All = "All";

        public const string AllProduct = $"{ProductPrefix}{All}";

    }
}
