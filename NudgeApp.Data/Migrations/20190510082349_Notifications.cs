using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Nudges",
                newName: "DateTime");

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

            migrationBuilder.AddColumn<Guid>(
                name: "NotificationId",
                table: "Nudges",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "PrecipitationProbability",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WindCondition",
                table: "Nudges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nudges_NotificationId",
                table: "Nudges",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nudges_Notifications_NotificationId",
                table: "Nudges",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nudges_Notifications_NotificationId",
                table: "Nudges");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Nudges_NotificationId",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "CloudCoveragePercent",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "PrecipitationProbability",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "WindCondition",
                table: "Nudges");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Nudges",
                newName: "Time");

            migrationBuilder.AlterColumn<int>(
                name: "Wind",
                table: "Nudges",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
