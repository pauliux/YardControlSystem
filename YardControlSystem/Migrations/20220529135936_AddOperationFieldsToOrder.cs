using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class AddOperationFieldsToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_DropOffWarehouseId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_PickUpWarehouseId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DropOffWarehouseId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PickUpWarehouseId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "DropOffOperationId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PickUpOperationId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOffOperationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickUpOperationId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DropOffWarehouseId",
                table: "Orders",
                column: "DropOffWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickUpWarehouseId",
                table: "Orders",
                column: "PickUpWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_DropOffWarehouseId",
                table: "Orders",
                column: "DropOffWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_PickUpWarehouseId",
                table: "Orders",
                column: "PickUpWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
