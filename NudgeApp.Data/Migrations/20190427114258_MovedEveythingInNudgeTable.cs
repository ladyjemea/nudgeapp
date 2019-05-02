using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class MovedEveythingInNudgeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nudges_Trips_TripId",
                table: "Nudges");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "WeatherForecast");

            migrationBuilder.DropIndex(
                name: "IX_Nudges_TripId",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Nudges");

            migrationBuilder.AddColumn<int>(
                name: "CloudCoveragePercent",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistanceTraveled",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrecipitationProbability",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ReafFeelTemperature",
                table: "Nudges",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "RoadCondition",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Temperature",
                table: "Nudges",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Nudges",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Wind",
                table: "Nudges",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AnonymousNudges",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nudges_Users_UserId",
                table: "Nudges");

            migrationBuilder.DropIndex(
                name: "IX_Nudges_UserId",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "CloudCoveragePercent",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "DistanceTraveled",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "PrecipitationProbability",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "ReafFeelTemperature",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "RoadCondition",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "Wind",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AnonymousNudges");

            migrationBuilder.AddColumn<Guid>(
                name: "TripId",
                table: "Nudges",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CloudCoveragePercent = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    NudgeId = table.Column<Guid>(nullable: true),
                    PrecipitationProbability = table.Column<int>(nullable: false),
                    ReafFeelTemperature = table.Column<float>(nullable: false),
                    RoadCondition = table.Column<int>(nullable: false),
                    Temperature = table.Column<float>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Wind = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecast_Nudges_NudgeId",
                        column: x => x.NudgeId,
                        principalTable: "Nudges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    DistanceTraveled = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UsedTransportationType = table.Column<int>(nullable: false),
                    WeatherForecastId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_WeatherForecast_WeatherForecastId",
                        column: x => x.WeatherForecastId,
                        principalTable: "WeatherForecast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nudges_TripId",
                table: "Nudges",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_WeatherForecastId",
                table: "Trips",
                column: "WeatherForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecast_NudgeId",
                table: "WeatherForecast",
                column: "NudgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nudges_Trips_TripId",
                table: "Nudges",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
