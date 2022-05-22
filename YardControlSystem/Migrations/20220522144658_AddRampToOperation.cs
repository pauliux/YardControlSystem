using Microsoft.EntityFrameworkCore.Migrations;

namespace YardControlSystem.Migrations
{
    public partial class AddRampToOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RampId",
                table: "Operations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_RampId",
                table: "Operations",
                column: "RampId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Ramps_RampId",
                table: "Operations",
                column: "RampId",
                principalTable: "Ramps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Ramps_RampId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_RampId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "RampId",
                table: "Operations");
        }
    }
}
