using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetailShopManagement.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercent",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxRate",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "Invoices");
        }
    }
}
