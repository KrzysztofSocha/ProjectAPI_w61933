using Microsoft.EntityFrameworkCore.Migrations;

namespace KrzysztofSochaAPI.Migrations
{
    public partial class AddSizeAndQuantityPropertiesToOrdersAndCartTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderClothes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "OrderClothes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderClothes");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderClothes");
        }
    }
}
