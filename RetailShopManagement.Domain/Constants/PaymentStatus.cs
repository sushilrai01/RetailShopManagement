using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.Domain.Constants;

public static class PaymentStatus
{
    public const string Pending = "Pending";
    public const string PaidPartial = "Paid Partial";
    public const string Paid = "Paid";
    public const string Overdue = "Overdue";
    public const string Cancelled = "Cancelled";
    public const string All = "All";

    public static IList<DropDownField> GetDropdown()
    {
        return new List<DropDownField>
        {
            new DropDownField { Value = All, Text = All },
            new DropDownField { Value = Pending, Text = Pending},
            new DropDownField { Value = PaidPartial, Text = PaidPartial },
            new DropDownField { Value = Paid, Text = Paid },
            new DropDownField { Value = Overdue, Text = Overdue},
            new DropDownField { Value = Cancelled, Text = Cancelled}
        };
    }

    public static string GetPaymentStatus(decimal paidAmount, decimal totalAmount)
    {
        string status;
        if (paidAmount == totalAmount)
        {
            status = Paid;
        }
        else if (paidAmount > 0 && paidAmount < totalAmount)
        {
            status = PaidPartial;
        }
        else if (paidAmount > totalAmount)
        {
            status = Overdue;
        }
        else
        {
            status = Pending;
        }

        return status;
    }
}
