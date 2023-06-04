using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCWebshop.Migrations
{
    public partial class ShopItemDef3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountInStore",
                table: "ShopItem",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ShopItem",
                newName: "AmountInStore");
        }
    }
}
