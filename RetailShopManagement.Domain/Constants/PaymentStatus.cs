using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.Domain.Constants;

public static class PaymentStatus
{
    public const string Pending = "Pending";
    public const string PaidPartial = "PaidPartial";
    public const string Paid = "Paid";
    public const string Overdue = "Overdue";
    public const string Cancelled = "Cancelled";

    public static IList<DropDownField> GetDropdown()
    {
        return new List<DropDownField>
        {
            new DropDownField { Value = Pending, Text = "Pending" },
            new DropDownField { Value = PaidPartial, Text = "Paid Partial" },
            new DropDownField { Value = Paid, Text = "Paid" },
            new DropDownField { Value = Overdue, Text = "Overdue" },
            new DropDownField { Value = Cancelled, Text = "Cancelled" }
        };
    }
}