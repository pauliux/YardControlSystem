using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class FixedOrderAndOperationModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_DropOffWarehouseId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_PickUpWarehouseId",
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

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OrderId",
                table: "Operations",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Orders_OrderId",
                table: "Operations",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderNr",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Orders_OrderId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_DropOffWarehouseId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_PickUpWarehouseId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Operations_OrderId",
                table: "Operations");

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

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
