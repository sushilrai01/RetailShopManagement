using RetailShopManagement.Domain.Models.Common;

namespace RetailShopManagement.Domain.Constants;

public static class InvoiceTypeConst 
{
    public static string Sale => "Sale";
    public static string Purchase => "Purchase";


    // DROPDOWN LIST
    public static IList<DropDownField> DropDownFields =>
        new List<DropDownField>
        {
            new() { Value = Sale, Text = Sale },
            new() { Value = Purchase, Text = Purchase },
        };
}