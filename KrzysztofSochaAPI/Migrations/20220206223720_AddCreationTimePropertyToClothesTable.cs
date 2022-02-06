using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrzysztofSochaAPI.Migrations
{
    public partial class AddCreationTimePropertyToClothesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Clothes",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Clothes");
        }
    }
}
