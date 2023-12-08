using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Persistence.DataBase.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductInStocks",
                schema: "Catalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInStocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_ProductInStocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description for product 1", "Product 1", 923m },
                    { 2, "Description for product 2", "Product 2", 935m },
                    { 3, "Description for product 3", "Product 3", 816m },
                    { 4, "Description for product 4", "Product 4", 948m },
                    { 5, "Description for product 5", "Product 5", 336m },
                    { 6, "Description for product 6", "Product 6", 538m },
                    { 7, "Description for product 7", "Product 7", 302m },
                    { 8, "Description for product 8", "Product 8", 313m },
                    { 9, "Description for product 9", "Product 9", 376m },
                    { 10, "Description for product 10", "Product 10", 240m },
                    { 11, "Description for product 11", "Product 11", 531m },
                    { 12, "Description for product 12", "Product 12", 225m },
                    { 13, "Description for product 13", "Product 13", 172m },
                    { 14, "Description for product 14", "Product 14", 614m },
                    { 15, "Description for product 15", "Product 15", 307m },
                    { 16, "Description for product 16", "Product 16", 222m },
                    { 17, "Description for product 17", "Product 17", 870m },
                    { 18, "Description for product 18", "Product 18", 113m },
                    { 19, "Description for product 19", "Product 19", 740m },
                    { 20, "Description for product 20", "Product 20", 702m },
                    { 21, "Description for product 21", "Product 21", 963m },
                    { 22, "Description for product 22", "Product 22", 971m },
                    { 23, "Description for product 23", "Product 23", 592m },
                    { 24, "Description for product 24", "Product 24", 122m },
                    { 25, "Description for product 25", "Product 25", 496m },
                    { 26, "Description for product 26", "Product 26", 701m },
                    { 27, "Description for product 27", "Product 27", 880m },
                    { 28, "Description for product 28", "Product 28", 501m },
                    { 29, "Description for product 29", "Product 29", 941m },
                    { 30, "Description for product 30", "Product 30", 813m },
                    { 31, "Description for product 31", "Product 31", 551m },
                    { 32, "Description for product 32", "Product 32", 389m },
                    { 33, "Description for product 33", "Product 33", 895m },
                    { 34, "Description for product 34", "Product 34", 797m },
                    { 35, "Description for product 35", "Product 35", 677m },
                    { 36, "Description for product 36", "Product 36", 347m },
                    { 37, "Description for product 37", "Product 37", 580m },
                    { 38, "Description for product 38", "Product 38", 105m },
                    { 39, "Description for product 39", "Product 39", 372m },
                    { 40, "Description for product 40", "Product 40", 665m },
                    { 41, "Description for product 41", "Product 41", 257m },
                    { 42, "Description for product 42", "Product 42", 856m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 43, "Description for product 43", "Product 43", 221m },
                    { 44, "Description for product 44", "Product 44", 507m },
                    { 45, "Description for product 45", "Product 45", 400m },
                    { 46, "Description for product 46", "Product 46", 623m },
                    { 47, "Description for product 47", "Product 47", 785m },
                    { 48, "Description for product 48", "Product 48", 359m },
                    { 49, "Description for product 49", "Product 49", 976m },
                    { 50, "Description for product 50", "Product 50", 788m },
                    { 51, "Description for product 51", "Product 51", 170m },
                    { 52, "Description for product 52", "Product 52", 528m },
                    { 53, "Description for product 53", "Product 53", 409m },
                    { 54, "Description for product 54", "Product 54", 299m },
                    { 55, "Description for product 55", "Product 55", 536m },
                    { 56, "Description for product 56", "Product 56", 393m },
                    { 57, "Description for product 57", "Product 57", 400m },
                    { 58, "Description for product 58", "Product 58", 283m },
                    { 59, "Description for product 59", "Product 59", 375m },
                    { 60, "Description for product 60", "Product 60", 978m },
                    { 61, "Description for product 61", "Product 61", 623m },
                    { 62, "Description for product 62", "Product 62", 530m },
                    { 63, "Description for product 63", "Product 63", 875m },
                    { 64, "Description for product 64", "Product 64", 154m },
                    { 65, "Description for product 65", "Product 65", 658m },
                    { 66, "Description for product 66", "Product 66", 107m },
                    { 67, "Description for product 67", "Product 67", 278m },
                    { 68, "Description for product 68", "Product 68", 793m },
                    { 69, "Description for product 69", "Product 69", 734m },
                    { 70, "Description for product 70", "Product 70", 401m },
                    { 71, "Description for product 71", "Product 71", 711m },
                    { 72, "Description for product 72", "Product 72", 878m },
                    { 73, "Description for product 73", "Product 73", 156m },
                    { 74, "Description for product 74", "Product 74", 115m },
                    { 75, "Description for product 75", "Product 75", 591m },
                    { 76, "Description for product 76", "Product 76", 634m },
                    { 77, "Description for product 77", "Product 77", 754m },
                    { 78, "Description for product 78", "Product 78", 980m },
                    { 79, "Description for product 79", "Product 79", 487m },
                    { 80, "Description for product 80", "Product 80", 774m },
                    { 81, "Description for product 81", "Product 81", 547m },
                    { 82, "Description for product 82", "Product 82", 184m },
                    { 83, "Description for product 83", "Product 83", 400m },
                    { 84, "Description for product 84", "Product 84", 914m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 85, "Description for product 85", "Product 85", 755m },
                    { 86, "Description for product 86", "Product 86", 690m },
                    { 87, "Description for product 87", "Product 87", 340m },
                    { 88, "Description for product 88", "Product 88", 953m },
                    { 89, "Description for product 89", "Product 89", 208m },
                    { 90, "Description for product 90", "Product 90", 660m },
                    { 91, "Description for product 91", "Product 91", 614m },
                    { 92, "Description for product 92", "Product 92", 763m },
                    { 93, "Description for product 93", "Product 93", 951m },
                    { 94, "Description for product 94", "Product 94", 209m },
                    { 95, "Description for product 95", "Product 95", 478m },
                    { 96, "Description for product 96", "Product 96", 405m },
                    { 97, "Description for product 97", "Product 97", 835m },
                    { 98, "Description for product 98", "Product 98", 922m },
                    { 99, "Description for product 99", "Product 99", 286m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "ProductInStocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 11 },
                    { 3, 3, 5 },
                    { 4, 4, 10 },
                    { 5, 5, 13 },
                    { 6, 6, 6 },
                    { 7, 7, 14 },
                    { 8, 8, 5 },
                    { 9, 9, 7 },
                    { 10, 10, 1 },
                    { 11, 11, 6 },
                    { 12, 12, 19 },
                    { 13, 13, 1 },
                    { 14, 14, 19 },
                    { 15, 15, 7 },
                    { 16, 16, 11 },
                    { 17, 17, 5 },
                    { 18, 18, 8 },
                    { 19, 19, 15 },
                    { 20, 20, 14 },
                    { 21, 21, 3 },
                    { 22, 22, 9 },
                    { 23, 23, 3 },
                    { 24, 24, 1 },
                    { 25, 25, 2 },
                    { 26, 26, 17 },
                    { 27, 27, 9 },
                    { 28, 28, 6 },
                    { 29, 29, 8 },
                    { 30, 30, 9 },
                    { 31, 31, 8 },
                    { 32, 32, 10 },
                    { 33, 33, 0 },
                    { 34, 34, 16 },
                    { 35, 35, 15 },
                    { 36, 36, 9 },
                    { 37, 37, 19 },
                    { 38, 38, 15 },
                    { 39, 39, 14 },
                    { 40, 40, 10 },
                    { 41, 41, 17 },
                    { 42, 42, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "ProductInStocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 43, 43, 13 },
                    { 44, 44, 19 },
                    { 45, 45, 8 },
                    { 46, 46, 11 },
                    { 47, 47, 6 },
                    { 48, 48, 0 },
                    { 49, 49, 3 },
                    { 50, 50, 1 },
                    { 51, 51, 6 },
                    { 52, 52, 4 },
                    { 53, 53, 0 },
                    { 54, 54, 14 },
                    { 55, 55, 12 },
                    { 56, 56, 1 },
                    { 57, 57, 5 },
                    { 58, 58, 13 },
                    { 59, 59, 14 },
                    { 60, 60, 11 },
                    { 61, 61, 14 },
                    { 62, 62, 16 },
                    { 63, 63, 17 },
                    { 64, 64, 3 },
                    { 65, 65, 5 },
                    { 66, 66, 12 },
                    { 67, 67, 7 },
                    { 68, 68, 10 },
                    { 69, 69, 8 },
                    { 70, 70, 17 },
                    { 71, 71, 4 },
                    { 72, 72, 11 },
                    { 73, 73, 7 },
                    { 74, 74, 0 },
                    { 75, 75, 16 },
                    { 76, 76, 16 },
                    { 77, 77, 9 },
                    { 78, 78, 2 },
                    { 79, 79, 16 },
                    { 80, 80, 6 },
                    { 81, 81, 9 },
                    { 82, 82, 15 },
                    { 83, 83, 2 },
                    { 84, 84, 11 }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "ProductInStocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 85, 85, 19 },
                    { 86, 86, 9 },
                    { 87, 87, 18 },
                    { 88, 88, 11 },
                    { 89, 89, 15 },
                    { 90, 90, 4 },
                    { 91, 91, 14 },
                    { 92, 92, 18 },
                    { 93, 93, 11 },
                    { 94, 94, 12 },
                    { 95, 95, 0 },
                    { 96, 96, 8 },
                    { 97, 97, 3 },
                    { 98, 98, 17 },
                    { 99, 99, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInStocks_ProductId",
                schema: "Catalog",
                table: "ProductInStocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                schema: "Catalog",
                table: "Products",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInStocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
