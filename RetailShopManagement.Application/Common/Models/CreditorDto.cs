using RetailShopManagement.Domain.Abstractions;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Common.Models;

public class CreditorDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string MobileNo { get; set; } = null!;
    public string? Email { get; set; }
    public string Address { get; set; } = null!;

    //Remaining amount to be paid to the creditor
    public decimal DueAmount { get; set; } 

    //Total amount paid to the creditor (total sum of PaySlips' AmountPaid)
    public decimal PaidAmount { get; set; } 

    //Total amount owed to the creditor (total sum of invoices' TotalAmount)
    public decimal TotalAmount { get; set; } 

    public string Status { get; set; } = PaymentStatus.Pending;

    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; } = string.Empty;

    //List of payment made to this creditor
    public IList<PaySlipDto> PaySlips { get; set; } = new List<PaySlipDto>();
    public IList<InvoicesDto> InvoiceLists { get; set; } = new List<InvoicesDto>();

}
public class PaySlipDto 
{
    public Guid Id { get; set; }
    //public Guid CreditorId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public decimal AmountPaid { get; set; }
    public string Remarks { get; set; } = null!;
}