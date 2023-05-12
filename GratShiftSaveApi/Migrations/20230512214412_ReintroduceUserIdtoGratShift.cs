using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GratShiftSaveApi.Migrations
{
    public partial class ReintroduceUserIdtoGratShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GratShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GratShifts");
        }
    }
}
