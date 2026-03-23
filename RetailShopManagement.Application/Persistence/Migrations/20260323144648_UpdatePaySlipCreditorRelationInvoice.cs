using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetailShopManagement.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaySlipCreditorRelationInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaySlips_Creditors_CreditorId",
                table: "PaySlips");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreditorId",
                table: "PaySlips",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "InvoicePdfPath",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPdfGenerated",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPurchaseInvoice",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_PaySlips_Creditors_CreditorId",
                table: "PaySlips",
                column: "CreditorId",
                principalTable: "Creditors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaySlips_Creditors_CreditorId",
                table: "PaySlips");

            migrationBuilder.DropColumn(
                name: "InvoicePdfPath",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsPdfGenerated",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsPurchaseInvoice",
                table: "Invoices");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreditorId",
                table: "PaySlips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaySlips_Creditors_CreditorId",
                table: "PaySlips",
                column: "CreditorId",
                principalTable: "Creditors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
