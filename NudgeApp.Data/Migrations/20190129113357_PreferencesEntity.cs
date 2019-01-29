using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class PreferencesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualTravelType",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "AimedTransportationType",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "PreferedTravelType",
                table: "UserEntity");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserEntity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PreferencesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    PreferedTravelType = table.Column<int>(nullable: false),
                    ActualTravelType = table.Column<int>(nullable: false),
                    AimedTransportationType = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferencesEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreferencesEntity_UserId",
                table: "PreferencesEntity",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreferencesEntity");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserEntity");

            migrationBuilder.AddColumn<int>(
                name: "ActualTravelType",
                table: "UserEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AimedTransportationType",
                table: "UserEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferedTravelType",
                table: "UserEntity",
                nullable: false,
                defaultValue: 0);
        }
    }
}
