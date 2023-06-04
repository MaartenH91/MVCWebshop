using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCWebshop.Migrations
{
    public partial class SC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "ShoppingCart",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "ShoppingCart");
        }
    }
}
