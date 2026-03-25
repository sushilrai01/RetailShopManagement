using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetailShopManagement.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePurchaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Invoices_InvoiceId",
                table: "ProductPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Products_ProductId",
                table: "ProductPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPurchase",
                table: "ProductPurchase");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "ProductPurchase",
                newName: "ProductPurchases");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPurchase_ProductId",
                table: "ProductPurchases",
                newName: "IX_ProductPurchases_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPurchase_InvoiceId",
                table: "ProductPurchases",
                newName: "IX_ProductPurchases_InvoiceId");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "ProductPurchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPurchases",
                table: "ProductPurchases",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    ProductId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchases_SupplierId",
                table: "ProductPurchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ProductId1",
                table: "InventoryItems",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchases_Invoices_InvoiceId",
                table: "ProductPurchases",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchases_Products_ProductId",
                table: "ProductPurchases",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchases_Suppliers_SupplierId",
                table: "ProductPurchases",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchases_Invoices_InvoiceId",
                table: "ProductPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchases_Products_ProductId",
                table: "ProductPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchases_Suppliers_SupplierId",
                table: "ProductPurchases");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPurchases",
                table: "ProductPurchases");

            migrationBuilder.DropIndex(
                name: "IX_ProductPurchases_SupplierId",
                table: "ProductPurchases");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "ProductPurchases");

            migrationBuilder.RenameTable(
                name: "ProductPurchases",
                newName: "ProductPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPurchases_ProductId",
                table: "ProductPurchase",
                newName: "IX_ProductPurchase_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPurchases_InvoiceId",
                table: "ProductPurchase",
                newName: "IX_ProductPurchase_InvoiceId");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPurchase",
                table: "ProductPurchase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Invoices_InvoiceId",
                table: "ProductPurchase",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Products_ProductId",
                table: "ProductPurchase",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
