using Microsoft.EntityFrameworkCore.Migrations;

namespace KrzysztofSochaAPI.Migrations
{
    public partial class AddNewPropertyToClothesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clothes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clothes");
        }
    }
}
