using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class holaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethodID",
                table: "Orderss");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orderss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orderss");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodID",
                table: "Orderss",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
