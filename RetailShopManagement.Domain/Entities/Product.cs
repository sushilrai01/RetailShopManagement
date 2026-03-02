using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetailShopManagement.Domain.Abstractions;

namespace RetailShopManagement.Domain.Entities
{
    public class Product : BaseDerivedEntity<Guid>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

    }
}