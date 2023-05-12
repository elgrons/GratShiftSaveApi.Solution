using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GratShiftSaveApi.Migrations
{
    public partial class RemoveUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GratShifts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GratShifts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "GratShifts",
                keyColumn: "GratShiftId",
                keyValue: 1,
                column: "UserId",
                value: "1");

            migrationBuilder.UpdateData(
                table: "GratShifts",
                keyColumn: "GratShiftId",
                keyValue: 2,
                column: "UserId",
                value: "2");
        }
    }
}
