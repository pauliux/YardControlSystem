using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class UpdateOrderAndOperationModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Operations_OperationId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Ramps_RampId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OperationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RampId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "RampId",
                table: "Orders",
                newName: "PickUpWarehouseId");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "DropOffWarehouseId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Operations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_DriverId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DropOffWarehouseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Operations");

            migrationBuilder.RenameColumn(
                name: "PickUpWarehouseId",
                table: "Orders",
                newName: "RampId");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                table: "Orders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OperationId",
                table: "Orders",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RampId",
                table: "Orders",
                column: "RampId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Operations_OperationId",
                table: "Orders",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Ramps_RampId",
                table: "Orders",
                column: "RampId",
                principalTable: "Ramps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
