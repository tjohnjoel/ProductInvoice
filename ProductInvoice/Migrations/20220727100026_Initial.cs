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
                    DiscountExpiration = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    { new Guid("352e8789-e944-496c-8d6d-67da0b3fc2be"), 9999m, "Mobiles", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6175), "Novice", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6191) },
                    { new Guid("53249ff4-b284-4202-843a-e3cb05688923"), 49999m, "Television", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6196), "Stans Pro", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6196) },
                    { new Guid("6c5c6945-6376-4e38-be16-80ff85341ffa"), 59999m, "Air Conditioner", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6230), "Cool Breeze", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6230) },
                    { new Guid("a532e0ec-73ee-43f3-9dd2-b8a1d5ed470c"), 99999m, "Laptop", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6232), "T14", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6233) },
                    { new Guid("dcfe7f16-1125-4805-b756-7b7114cc6cf6"), 19999m, "Washing Machine", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6227), "Ultra Washing", new DateTime(2022, 7, 27, 15, 30, 25, 889, DateTimeKind.Local).AddTicks(6228) }
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
