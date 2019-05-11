using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class GeneralColumnRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Users",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Users",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "PushNotifications",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "PushNotifications",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Preferences",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Preferences",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Nudges",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Nudges",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Notifications",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Notifications",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "AnonymousNudges",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "AnonymousNudges",
                newName: "CreatedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Users",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Users",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "PushNotifications",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "PushNotifications",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Preferences",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Preferences",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Nudges",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Nudges",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Notifications",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Notifications",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "AnonymousNudges",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "AnonymousNudges",
                newName: "Created");
        }
    }
}
