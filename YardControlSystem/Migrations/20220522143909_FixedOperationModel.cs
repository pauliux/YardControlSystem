using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class FixedOperationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReservedDate",
                table: "Operations",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedDate",
                table: "Operations");
        }
    }
}
