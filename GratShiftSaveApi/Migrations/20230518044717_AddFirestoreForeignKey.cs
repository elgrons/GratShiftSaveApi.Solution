using Microsoft.EntityFrameworkCore.Migrations;

namespace GratShiftSaveApi.Migrations
{
    public partial class AddFirestoreForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.AlterColumn<string>(
        name: "FirestoreForeignKey",
        table: "GratShifts",
        type: "varchar(500)",
        nullable: false);

        migrationBuilder.CreateIndex(
        name: "IX_GratShifts_FirestoreForeignKey",
        table: "GratShifts",
        column: "FirestoreForeignKey");
    
        migrationBuilder.AddForeignKey(
        name: "FK_GratShifts_AspNetUsers_FirestoreForeignKey",
        table: "GratShifts",
        column: "FirestoreForeignKey",
        principalTable: "AspNetUsers",
        principalColumn: "Id",
        onDelete: ReferentialAction.Restrict);
}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
