using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetailShopManagement.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaySlipCreditorInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceAmount",
                table: "Creditors");

            migrationBuilder.DropColumn(
                name: "DueAmount",
                table: "Creditors");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Creditors");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "PaySlips",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_InvoiceId",
                table: "PaySlips",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaySlips_Invoices_InvoiceId",
                table: "PaySlips",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaySlips_Invoices_InvoiceId",
                table: "PaySlips");

            migrationBuilder.DropIndex(
                name: "IX_PaySlips_InvoiceId",
                table: "PaySlips");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "PaySlips");

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceAmount",
                table: "Creditors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DueAmount",
                table: "Creditors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Creditors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
