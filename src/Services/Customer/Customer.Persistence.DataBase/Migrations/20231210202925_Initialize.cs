﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer.Persistence.DataBase.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Customer",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                schema: "Customer",
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 1, "Client 1" },
                    { 2, "Client 2" },
                    { 3, "Client 3" },
                    { 4, "Client 4" },
                    { 5, "Client 5" },
                    { 6, "Client 6" },
                    { 7, "Client 7" },
                    { 8, "Client 8" },
                    { 9, "Client 9" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Customer");
        }
    }
}
