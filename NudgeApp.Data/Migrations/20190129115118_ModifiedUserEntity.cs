using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class ModifiedUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserEntity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserEntity");
        }
    }
}
