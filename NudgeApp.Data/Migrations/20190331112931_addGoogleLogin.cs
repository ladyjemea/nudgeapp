using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class addGoogleLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NudgeId",
                table: "WeatherForecast",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Google",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecast_NudgeId",
                table: "WeatherForecast",
                column: "NudgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherForecast_Nudges_NudgeId",
                table: "WeatherForecast",
                column: "NudgeId",
                principalTable: "Nudges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherForecast_Nudges_NudgeId",
                table: "WeatherForecast");

            migrationBuilder.DropIndex(
                name: "IX_WeatherForecast_NudgeId",
                table: "WeatherForecast");

            migrationBuilder.DropColumn(
                name: "NudgeId",
                table: "WeatherForecast");

            migrationBuilder.DropColumn(
                name: "Google",
                table: "Users");
        }
    }
}
