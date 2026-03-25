using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Application.Common.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; } = UnitsConst.None;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
    }

}