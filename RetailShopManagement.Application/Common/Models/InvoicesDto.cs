using RetailShopManagement.Domain.Abstractions;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailShopManagement.Application.Common.Models;

public class InvoicesDto
{
    public Guid Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; } = DateTime.Now;

    public DateTime? DueDate { get; set; }

    public Guid? CreditorId { get; set; }

    public decimal SubTotal { get; set; }
    public bool IsTaxApplicable { get; set; } = true;

    public decimal TaxRate { get; set; } = 13.00m; // Default tax rate (e.g., 13.00 for 13%)
    public decimal TaxAmount { get; set; }
    public decimal DiscountPercent { get; set; }
    public bool IsDiscountApplied { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TotalAmount { get; set; }
     
    public decimal PaidAmount { get; set; }
     
    public decimal BalanceAmount { get; set; }

    public string Status { get; set; } = PaymentStatus.Pending;

    public string? Remarks { get; set; }

    public Guid? SupplierId { get; set; }

    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; } = string.Empty;

    //List of Invoice Items
    public IList<ProductSalesDto> InvoiceItems { get; set; } = new List<ProductSalesDto>();
}
public class ProductSalesDto 
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;

    //public string? ProductCode { get; set; }

    public decimal Quantity { get; set; }

    public string Unit { get; set; } = null!; 

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }

    public decimal TaxRate { get; set; } // Tax percentage (e.g., 13.00 for 13%)

    public decimal TaxAmount { get; set; }

    public decimal DiscountAmount { get; set; }
     
    public decimal TotalAmount { get; set; } // Subtotal + Tax - Discount
    public string? Notes { get; set; }
}
public class InvoiceResponseModel 
{
    public Guid Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;

}