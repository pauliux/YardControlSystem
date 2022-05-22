using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class RemoveDriverFKFromOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DriverId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_DriverId",
                table: "Orders",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
