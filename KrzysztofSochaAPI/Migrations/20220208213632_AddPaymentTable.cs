using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrzysztofSochaAPI.Migrations
{
    public partial class AddPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FreeDelivery",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payment_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payment_PaymentId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FreeDelivery",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");
        }
    }
}
