using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class Buys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Buys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pagado = table.Column<bool>(type: "bit", nullable: false),
                    Entregado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BuyId",
                table: "Products",
                column: "BuyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Buys_BuyId",
                table: "Products",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Buys_BuyId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Buys");

            migrationBuilder.DropIndex(
                name: "IX_Products_BuyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BuyId",
                table: "Products");
        }
    }
}
