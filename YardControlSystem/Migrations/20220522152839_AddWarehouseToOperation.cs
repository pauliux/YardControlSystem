using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class AddWarehouseToOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorekeeperId",
                table: "Warehouses");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Operations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Operations");

            migrationBuilder.AddColumn<int>(
                name: "StorekeeperId",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
