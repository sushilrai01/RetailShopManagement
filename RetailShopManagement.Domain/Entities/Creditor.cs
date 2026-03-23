using RetailShopManagement.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Domain.Entities
{
    public class Creditor : BaseDerivedEntity<Guid>
    {
        public string FullName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string? Email { get; set; }
        public string Address { get; set; } = null!;

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal DueAmount { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal PaidAmount { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal BalanceAmount { get; set; }

        [Required]
        public string Status { get; set; } = PaymentStatus.Pending;

        //List of payment made to this creditor
        public ICollection<PaySlip> PaySlips { get; set; } = new HashSet<PaySlip>();
        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();

        // Computed properties (not mapped to DB)
        [NotMapped] 
        public decimal TotalAmount => Invoices.Sum(i => i.TotalAmount);

        [NotMapped]
        public decimal TotalPaid => Invoices.Sum(p => p.PaidAmount);

        [NotMapped]
        public decimal DueAmount => (TotalAmount - TotalPaid);

    }
}