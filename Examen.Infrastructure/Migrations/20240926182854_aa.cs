using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class aa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDetails_Items_ItemID",
                table: "ItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Items_ItemID",
                table: "ItemPrices");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "Items",
                newName: "itemID");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "ItemPrices",
                newName: "itemID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPrices_ItemID",
                table: "ItemPrices",
                newName: "IX_ItemPrices_itemID");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "ItemDetails",
                newName: "itemID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemDetails_ItemID",
                table: "ItemDetails",
                newName: "IX_ItemDetails_itemID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDetails_Items_itemID",
                table: "ItemDetails",
                column: "itemID",
                principalTable: "Items",
                principalColumn: "itemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Items_itemID",
                table: "ItemPrices",
                column: "itemID",
                principalTable: "Items",
                principalColumn: "itemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDetails_Items_itemID",
                table: "ItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Items_itemID",
                table: "ItemPrices");

            migrationBuilder.RenameColumn(
                name: "itemID",
                table: "Items",
                newName: "ItemID");

            migrationBuilder.RenameColumn(
                name: "itemID",
                table: "ItemPrices",
                newName: "ItemID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPrices_itemID",
                table: "ItemPrices",
                newName: "IX_ItemPrices_ItemID");

            migrationBuilder.RenameColumn(
                name: "itemID",
                table: "ItemDetails",
                newName: "ItemID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemDetails_itemID",
                table: "ItemDetails",
                newName: "IX_ItemDetails_ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDetails_Items_ItemID",
                table: "ItemDetails",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Items_ItemID",
                table: "ItemPrices",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID");
        }
    }
}
