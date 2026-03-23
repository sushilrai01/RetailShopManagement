namespace RetailShopManagement.Application.Common.Models;

public class SalesSummaryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string? ProductName { get; set; }

    public decimal TotalQuantity { get; set; }
    public decimal TotalSales { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalDiscount { get; set; }
    public IList<SalesSummaryDto> ReportList { get; set; } = new List<SalesSummaryDto>();
}