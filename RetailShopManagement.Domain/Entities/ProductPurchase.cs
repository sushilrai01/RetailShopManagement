
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RetailShopManagement.Domain.Abstractions;

namespace RetailShopManagement.Domain.Entities;

public class ProductPurchase : BaseDerivedEntity<Guid>
{
    // Foreign key to Invoice
    [Required]
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    // Foreign key to Product
    [Required]
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    // Foreign key to Supplier
    [Required]
    public int SupplierId { get; set; }

    public Supplier Supplier { get; set; } = null!;


    [Required]
    [StringLength(200)]
    public string ProductName { get; set; } = string.Empty;

    [StringLength(50)]
    public string? ProductCode { get; set; }

    [Column(TypeName = "decimal(10,3)")]
    public decimal Quantity { get; set; }

    [Required]
    [StringLength(20)]
    public string Unit { get; set; } = null!; // pcs, kg, ltr, meter, etc.

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal TaxRate { get; set; } // Tax percentage (e.g., 13.00 for 13%)

    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; } // Subtotal + Tax - Discount

    [StringLength(500)]
    public string? Notes { get; set; }
}