using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class RenamedColumnsNudge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudCoveragePercent",
                table: "Nudges");

            migrationBuilder.RenameColumn(
                name: "PrecipitationProbability",
                table: "Nudges",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "DistanceTraveled",
                table: "Nudges",
                newName: "Distance");

            migrationBuilder.AlterColumn<int>(
                name: "Wind",
                table: "Nudges",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<int>(
                name: "Probability",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkyCoverage",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Probability",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "SkyCoverage",
                table: "Nudges");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Nudges",
                newName: "PrecipitationProbability");

            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "Nudges",
                newName: "DistanceTraveled");

            migrationBuilder.AlterColumn<float>(
                name: "Wind",
                table: "Nudges",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CloudCoveragePercent",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);
        }
    }
}
