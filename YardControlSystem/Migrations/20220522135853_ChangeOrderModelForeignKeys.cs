using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class ChangeOrderModelForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PickUpWarehouseId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DropOffWarehouseId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_PickUpWarehouseId",
                table: "Orders",
                column: "PickUpWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "PickUpWarehouseId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DropOffWarehouseId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
