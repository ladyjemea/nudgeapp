using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class ActualPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushNotifications");

            migrationBuilder.DropColumn(
                name: "ActualTransportationType",
                table: "Preferences");

            migrationBuilder.RenameColumn(
                name: "PreferedTransportationType",
                table: "Preferences",
                newName: "MinTemperature");

            migrationBuilder.RenameColumn(
                name: "AimedTransportationType",
                table: "Preferences",
                newName: "MaxTemperature");

            migrationBuilder.AddColumn<bool>(
                name: "RainyTrip",
                table: "Preferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SnowyTrip",
                table: "Preferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TransportationType",
                table: "Preferences",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Nudges",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ActualPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    TransportationType = table.Column<int>(nullable: false),
                    MinTemperature = table.Column<int>(nullable: false),
                    MaxTemperature = table.Column<int>(nullable: false),
                    RainyTrip = table.Column<bool>(nullable: false),
                    SnowyTrip = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActualPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActualPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PushNotificationSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Endpoint = table.Column<string>(nullable: true),
                    P256DH = table.Column<string>(nullable: true),
                    Auth = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActualPreferences_UserId",
                table: "ActualPreferences",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActualPreferences");

            migrationBuilder.DropTable(
                name: "PushNotificationSubscriptions");

            migrationBuilder.DropColumn(
                name: "RainyTrip",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "SnowyTrip",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "TransportationType",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Nudges");

            migrationBuilder.RenameColumn(
                name: "MinTemperature",
                table: "Preferences",
                newName: "PreferedTransportationType");

            migrationBuilder.RenameColumn(
                name: "MaxTemperature",
                table: "Preferences",
                newName: "AimedTransportationType");

            migrationBuilder.AddColumn<int>(
                name: "ActualTransportationType",
                table: "Preferences",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PushNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Auth = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Endpoint = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    P256DH = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotifications", x => x.Id);
                });
        }
    }
}
