using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class hola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderss_Customers_CustomerID",
                table: "Orderss");

            migrationBuilder.DropColumn(
                name: "DatCrea",
                table: "Orderss");

            migrationBuilder.DropColumn(
                name: "DatUpt",
                table: "Orderss");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Orderss",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderss_Customers_CustomerID",
                table: "Orderss",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderss_Customers_CustomerID",
                table: "Orderss");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Orderss",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatCrea",
                table: "Orderss",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DatUpt",
                table: "Orderss",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Orderss_Customers_CustomerID",
                table: "Orderss",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
