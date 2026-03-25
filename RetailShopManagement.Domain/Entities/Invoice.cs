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
    public decimal TaxRate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountPercent { get; set; }

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


    public bool IsPurchaseInvoice { get; set; } = false;
    public bool IsPdfGenerated { get; set; } = false;
    public string InvoicePdfPath { get; set; } = string.Empty;
    //List of InvoiceItems
    public ICollection<ProductSale> InvoiceItems { get; set; } = new HashSet<ProductSale>();
    public ICollection<ProductPurchase> PurchaseItems { get; set; } = new HashSet<ProductPurchase>();
    public ICollection<PaySlip> PaySlips { get; set; } = new HashSet<PaySlip>();

}