using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.Domain.Constants;

public static class ReportTypeConst
{
    public const string SalesByProduct = "Sales By Product";
    public const string SalesByCategory = "Sales By Category";

    //Timeline
    public const string Daily = "Daily";
    public const string Weekly = "Weekly";
    public const string Monthly = "Monthly";

    public static IList<DropDownField> GetDropdown()
    {
        return new List<DropDownField>
        {
            new(),
            new() { Value = SalesByProduct, Text = SalesByProduct },
            new() { Value = SalesByCategory, Text = SalesByCategory},
        };
    }
    public static IList<DropDownField> GetTimelineFields()
    {
        return new List<DropDownField>
        {
            new(){ Value = string.Empty , Text = "--Select Timeline--"},
            new() { Value = Daily, Text = Daily },
            new() { Value = Weekly, Text = Weekly},
            new() { Value = Monthly, Text = Monthly},
        };
    }

}
