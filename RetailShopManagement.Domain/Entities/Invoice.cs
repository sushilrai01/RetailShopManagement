using RetailShopManagement.Domain.Abstractions;
using RetailShopManagement.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailShopManagement.Domain.Entities;

public class Invoice : BaseDerivedEntity<Guid>
{

    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; } = DateTime.Now;

    public DateTime? DueDate { get; set; }

    public Guid? CreditorId { get; set; }
    public Creditor? Creditor { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal BalanceAmount { get; set; }

    [Required] public string Status { get; set; } = PaymentStatus.Pending;

    [StringLength(500)]
    public string? Remarks { get; set; }
     
    public Guid? SupplierId { get; set; }

    //List of InvoiceItems
    public ICollection<ProductSale> InvoiceItems { get; set; } = new HashSet<ProductSale>();
}