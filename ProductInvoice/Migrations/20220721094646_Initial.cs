using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductInvoice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductBasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    DiscountCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountExpiration = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Discounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfItems = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.InvoiceItemsId);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductBasePrice", "ProductCategory", "ProductCreated", "ProductName", "ProductUpdated" },
                values: new object[,]
                {
                    { new Guid("333ae2c4-50c2-4f44-8499-35e06bce0c7e"), 49999m, "Television", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7671), "Stans Pro", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7671) },
                    { new Guid("75373a09-95d7-4e03-b489-7f99a81ed3b8"), 9999m, "Mobiles", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7659), "Novice", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7669) },
                    { new Guid("b69b0f72-5162-4e38-867b-25c123b9f939"), 99999m, "Laptop", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7676), "T14", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7677) },
                    { new Guid("c28a8af3-b1a2-4d87-83fd-64c36153d0bd"), 19999m, "Washing Machine", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7673), "Ultra Washing", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7673) },
                    { new Guid("e1ac9917-d9fe-4aea-a43e-5680580665da"), 59999m, "Air Conditioner", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7674), "Cool Breeze", new DateTime(2022, 7, 21, 15, 16, 46, 114, DateTimeKind.Local).AddTicks(7675) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductId",
                table: "Discounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductId",
                table: "InvoiceItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
