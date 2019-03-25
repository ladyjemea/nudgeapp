using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class tableRenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NudgeEntity_UserEntity_UserId",
                table: "NudgeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_NudgeEntity_EnvironmentalInfoEntity_WeatherForecastId",
                table: "NudgeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferencesEntity_UserEntity_UserId",
                table: "PreferencesEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TripEntity_UserEntity_UserId",
                table: "TripEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TripEntity_EnvironmentalInfoEntity_WeatherForecastId",
                table: "TripEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripEntity",
                table: "TripEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PushNotificationEntity",
                table: "PushNotificationEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferencesEntity",
                table: "PreferencesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NudgeEntity",
                table: "NudgeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnvironmentalInfoEntity",
                table: "EnvironmentalInfoEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnonymousNudgeEntity",
                table: "AnonymousNudgeEntity");

            migrationBuilder.RenameTable(
                name: "UserEntity",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "TripEntity",
                newName: "Trips");

            migrationBuilder.RenameTable(
                name: "PushNotificationEntity",
                newName: "PushNotifications");

            migrationBuilder.RenameTable(
                name: "PreferencesEntity",
                newName: "Preferences");

            migrationBuilder.RenameTable(
                name: "NudgeEntity",
                newName: "Nudges");

            migrationBuilder.RenameTable(
                name: "EnvironmentalInfoEntity",
                newName: "WeatherForecast");

            migrationBuilder.RenameTable(
                name: "AnonymousNudgeEntity",
                newName: "AnonymousNudges");

            migrationBuilder.RenameIndex(
                name: "IX_TripEntity_WeatherForecastId",
                table: "Trips",
                newName: "IX_Trips_WeatherForecastId");

            migrationBuilder.RenameIndex(
                name: "IX_TripEntity_UserId",
                table: "Trips",
                newName: "IX_Trips_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PreferencesEntity_UserId",
                table: "Preferences",
                newName: "IX_Preferences_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NudgeEntity_WeatherForecastId",
                table: "Nudges",
                newName: "IX_Nudges_WeatherForecastId");

            migrationBuilder.RenameIndex(
                name: "IX_NudgeEntity_UserId",
                table: "Nudges",
                newName: "IX_Nudges_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PushNotifications",
                table: "PushNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Preferences",
                table: "Preferences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nudges",
                table: "Nudges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherForecast",
                table: "WeatherForecast",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnonymousNudges",
                table: "AnonymousNudges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nudges_Users_UserId",
                table: "Nudges",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nudges_WeatherForecast_WeatherForecastId",
                table: "Nudges",
                column: "WeatherForecastId",
                principalTable: "WeatherForecast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_Users_UserId",
                table: "Preferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Nudges_Users_UserId",
                table: "Nudges");

            migrationBuilder.DropForeignKey(
                name: "FK_Nudges_WeatherForecast_WeatherForecastId",
                table: "Nudges");

            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_Users_UserId",
                table: "Preferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_WeatherForecast_WeatherForecastId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherForecast",
                table: "WeatherForecast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PushNotifications",
                table: "PushNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Preferences",
                table: "Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nudges",
                table: "Nudges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnonymousNudges",
                table: "AnonymousNudges");

            migrationBuilder.RenameTable(
                name: "WeatherForecast",
                newName: "EnvironmentalInfoEntity");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserEntity");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "TripEntity");

            migrationBuilder.RenameTable(
                name: "PushNotifications",
                newName: "PushNotificationEntity");

            migrationBuilder.RenameTable(
                name: "Preferences",
                newName: "PreferencesEntity");

            migrationBuilder.RenameTable(
                name: "Nudges",
                newName: "NudgeEntity");

            migrationBuilder.RenameTable(
                name: "AnonymousNudges",
                newName: "AnonymousNudgeEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_WeatherForecastId",
                table: "TripEntity",
                newName: "IX_TripEntity_WeatherForecastId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_UserId",
                table: "TripEntity",
                newName: "IX_TripEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Preferences_UserId",
                table: "PreferencesEntity",
                newName: "IX_PreferencesEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Nudges_WeatherForecastId",
                table: "NudgeEntity",
                newName: "IX_NudgeEntity_WeatherForecastId");

            migrationBuilder.RenameIndex(
                name: "IX_Nudges_UserId",
                table: "NudgeEntity",
                newName: "IX_NudgeEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnvironmentalInfoEntity",
                table: "EnvironmentalInfoEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripEntity",
                table: "TripEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PushNotificationEntity",
                table: "PushNotificationEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferencesEntity",
                table: "PreferencesEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NudgeEntity",
                table: "NudgeEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnonymousNudgeEntity",
                table: "AnonymousNudgeEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NudgeEntity_UserEntity_UserId",
                table: "NudgeEntity",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NudgeEntity_EnvironmentalInfoEntity_WeatherForecastId",
                table: "NudgeEntity",
                column: "WeatherForecastId",
                principalTable: "EnvironmentalInfoEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferencesEntity_UserEntity_UserId",
                table: "PreferencesEntity",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripEntity_UserEntity_UserId",
                table: "TripEntity",
                column: "UserId",
                principalTable: "UserEntity",
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
    }
}
