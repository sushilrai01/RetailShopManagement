using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Domain.Models.Common
{
    public class DropDownField
    {
        public string? Value { get; set; } = null;
        public string Text { get; set; } = "--Select--";
    }
    public class IntDropDownField
    {
        public int Value { get; set; }
        public string Text { get; set; } = "--Select--";
    }
    public class GuidDropDownField
    {
        public Guid Value { get; set; }
        public string Text { get; set; } = "--Select--";
    }
}
