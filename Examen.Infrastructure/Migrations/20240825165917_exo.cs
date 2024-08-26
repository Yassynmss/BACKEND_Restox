using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class exo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_BizAccounts_BizAccountID",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Menus");

            migrationBuilder.AlterColumn<int>(
                name: "BizAccountID",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_BizAccounts_BizAccountID",
                table: "Menus",
                column: "BizAccountID",
                principalTable: "BizAccounts",
                principalColumn: "BizAccountID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_BizAccounts_BizAccountID",
                table: "Menus");

            migrationBuilder.AlterColumn<int>(
                name: "BizAccountID",
                table: "Menus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccountID",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_BizAccounts_BizAccountID",
                table: "Menus",
                column: "BizAccountID",
                principalTable: "BizAccounts",
                principalColumn: "BizAccountID");
        }
    }
}
