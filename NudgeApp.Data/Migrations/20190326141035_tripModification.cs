using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class tripModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WeatherForecastId",
                table: "Trips",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Trips_WeatherForecastId",
                table: "Trips",
                column: "WeatherForecastId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_WeatherForecast_WeatherForecastId",
                table: "Trips",
                column: "WeatherForecastId",
                principalTable: "WeatherForecast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_WeatherForecast_WeatherForecastId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_WeatherForecastId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "WeatherForecastId",
                table: "Trips");
        }
    }
}
