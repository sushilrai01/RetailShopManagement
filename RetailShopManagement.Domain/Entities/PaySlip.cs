using RetailShopManagement.Domain.Abstractions;

namespace RetailShopManagement.Domain.Entities;

public class PaySlip: BaseDerivedEntity<Guid>
{
    public Guid? CreditorId { get; set; }
    public Creditor? Creditor { get; set; }
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public decimal AmountPaid { get; set; }
    public string Remarks { get; set; } = null!;
}