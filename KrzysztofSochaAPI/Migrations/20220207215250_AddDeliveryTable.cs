using Microsoft.EntityFrameworkCore.Migrations;

namespace KrzysztofSochaAPI.Migrations
{
    public partial class AddDeliveryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClothesAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveryType",
                table: "Orders",
                newName: "DeliveryId");

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Deliveries_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Deliveries_DeliveryId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveryId",
                table: "Orders",
                newName: "DeliveryType");

            migrationBuilder.AddColumn<decimal>(
                name: "ClothesAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
