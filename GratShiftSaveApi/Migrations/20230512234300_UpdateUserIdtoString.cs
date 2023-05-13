using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GratShiftSaveApi.Migrations
{
    public partial class UpdateUserIdtoString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GratShifts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "GratShifts",
                keyColumn: "GratShiftId",
                keyValue: 1,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "GratShifts",
                keyColumn: "GratShiftId",
                keyValue: 2,
                column: "UserId",
                value: "1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "GratShifts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "GratShifts",
                keyColumn: "GratShiftId",
                keyValue: 1,
                column: "UserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GratShifts",
                keyColumn: "GratShiftId",
                keyValue: 2,
                column: "UserId",
                value: 0);
        }
    }
}
