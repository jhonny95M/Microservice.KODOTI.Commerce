using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Persistence.DataBase.Migrations
{
    public partial class updataorderDetial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                schema: "Order",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "Order",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                schema: "Order",
                table: "OrderDetails",
                column: "OrderId",
                principalSchema: "Order",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                schema: "Order",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "Order",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Order",
                table: "OrderDetails");
        }
    }
}
