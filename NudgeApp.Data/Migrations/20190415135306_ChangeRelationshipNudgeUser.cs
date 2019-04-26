using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class ChangeRelationshipNudgeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nudges_Users_UserId",
                table: "Nudges");

            migrationBuilder.DropIndex(
                name: "IX_Nudges_UserId",
                table: "Nudges");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Nudges_UserId",
                table: "Nudges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nudges_Users_UserId",
                table: "Nudges",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
