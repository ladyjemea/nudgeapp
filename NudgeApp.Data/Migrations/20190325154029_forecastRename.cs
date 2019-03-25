using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class forecastRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NudgeEntity_EnvironmentalInfoEntity_EnvironmentalInfoId",
                table: "NudgeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TripEntity_EnvironmentalInfoEntity_EnvironmentalInfoId",
                table: "TripEntity");

            migrationBuilder.RenameColumn(
                name: "EnvironmentalInfoId",
                table: "TripEntity",
                newName: "WeatherForecastId");

            migrationBuilder.RenameIndex(
                name: "IX_TripEntity_EnvironmentalInfoId",
                table: "TripEntity",
                newName: "IX_TripEntity_WeatherForecastId");

            migrationBuilder.RenameColumn(
                name: "EnvironmentalInfoId",
                table: "NudgeEntity",
                newName: "WeatherForecastId");

            migrationBuilder.RenameIndex(
                name: "IX_NudgeEntity_EnvironmentalInfoId",
                table: "NudgeEntity",
                newName: "IX_NudgeEntity_WeatherForecastId");

            migrationBuilder.AlterColumn<float>(
                name: "Wind",
                table: "EnvironmentalInfoEntity",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Temperature",
                table: "EnvironmentalInfoEntity",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PrecipitationProbability",
                table: "EnvironmentalInfoEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ReafFeelTemperature",
                table: "EnvironmentalInfoEntity",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_NudgeEntity_EnvironmentalInfoEntity_WeatherForecastId",
                table: "NudgeEntity",
                column: "WeatherForecastId",
                principalTable: "EnvironmentalInfoEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripEntity_EnvironmentalInfoEntity_WeatherForecastId",
                table: "TripEntity",
                column: "WeatherForecastId",
                principalTable: "EnvironmentalInfoEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NudgeEntity_EnvironmentalInfoEntity_WeatherForecastId",
                table: "NudgeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TripEntity_EnvironmentalInfoEntity_WeatherForecastId",
                table: "TripEntity");

            migrationBuilder.DropColumn(
                name: "PrecipitationProbability",
                table: "EnvironmentalInfoEntity");

            migrationBuilder.DropColumn(
                name: "ReafFeelTemperature",
                table: "EnvironmentalInfoEntity");

            migrationBuilder.RenameColumn(
                name: "WeatherForecastId",
                table: "TripEntity",
                newName: "EnvironmentalInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_TripEntity_WeatherForecastId",
                table: "TripEntity",
                newName: "IX_TripEntity_EnvironmentalInfoId");

            migrationBuilder.RenameColumn(
                name: "WeatherForecastId",
                table: "NudgeEntity",
                newName: "EnvironmentalInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_NudgeEntity_WeatherForecastId",
                table: "NudgeEntity",
                newName: "IX_NudgeEntity_EnvironmentalInfoId");

            migrationBuilder.AlterColumn<int>(
                name: "Wind",
                table: "EnvironmentalInfoEntity",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Temperature",
                table: "EnvironmentalInfoEntity",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddForeignKey(
                name: "FK_NudgeEntity_EnvironmentalInfoEntity_EnvironmentalInfoId",
                table: "NudgeEntity",
                column: "EnvironmentalInfoId",
                principalTable: "EnvironmentalInfoEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripEntity_EnvironmentalInfoEntity_EnvironmentalInfoId",
                table: "TripEntity",
                column: "EnvironmentalInfoId",
                principalTable: "EnvironmentalInfoEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
