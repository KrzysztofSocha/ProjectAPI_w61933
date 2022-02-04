using Microsoft.EntityFrameworkCore.Migrations;

namespace KrzysztofSochaAPI.Migrations
{
    public partial class ChangeRelationOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedClothes_Orders_OrderId",
                table: "OrderedClothes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedClothes",
                table: "OrderedClothes");

            migrationBuilder.DropIndex(
                name: "IX_OrderedClothes_OrderedClothesId",
                table: "OrderedClothes");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderedClothes",
                newName: "OrdersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedClothes",
                table: "OrderedClothes",
                columns: new[] { "OrderedClothesId", "OrdersId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedClothes_OrdersId",
                table: "OrderedClothes",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedClothes_Orders_OrdersId",
                table: "OrderedClothes",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedClothes_Orders_OrdersId",
                table: "OrderedClothes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedClothes",
                table: "OrderedClothes");

            migrationBuilder.DropIndex(
                name: "IX_OrderedClothes_OrdersId",
                table: "OrderedClothes");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "OrderedClothes",
                newName: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedClothes",
                table: "OrderedClothes",
                columns: new[] { "OrderId", "OrderedClothesId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedClothes_OrderedClothesId",
                table: "OrderedClothes",
                column: "OrderedClothesId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedClothes_Orders_OrderId",
                table: "OrderedClothes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
