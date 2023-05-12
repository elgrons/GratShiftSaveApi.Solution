using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GratShiftSaveApi.Migrations
{
    public partial class AddUserIdtoGratShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogins");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GratShifts");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Username = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Username);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
