using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class NotificationsFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nudges_Notifications_NotificationId",
                table: "Nudges");

            migrationBuilder.DropIndex(
                name: "IX_Nudges_NotificationId",
                table: "Nudges");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "Nudges");

            migrationBuilder.AddColumn<Guid>(
                name: "NudgeId",
                table: "Notifications",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NudgeId",
                table: "Notifications",
                column: "NudgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Nudges_NudgeId",
                table: "Notifications",
                column: "NudgeId",
                principalTable: "Nudges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Nudges_NudgeId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_NudgeId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NudgeId",
                table: "Notifications");

            migrationBuilder.AddColumn<Guid>(
                name: "NotificationId",
                table: "Nudges",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
