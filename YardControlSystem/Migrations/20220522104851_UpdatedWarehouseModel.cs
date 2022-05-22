using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class UpdatedWarehouseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Ramps",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Ramps_WarehouseId",
                table: "Ramps",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ramps_Warehouses_WarehouseId",
                table: "Ramps",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ramps_Warehouses_WarehouseId",
                table: "Ramps");

            migrationBuilder.DropIndex(
                name: "IX_Ramps_WarehouseId",
                table: "Ramps");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Ramps",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
