using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class FixOrderDateOfCreationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_DriverId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DriverId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DriverId1",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateOfCreation",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "DriverId1",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DriverId1",
                table: "Orders",
                column: "DriverId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_DriverId1",
                table: "Orders",
                column: "DriverId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
