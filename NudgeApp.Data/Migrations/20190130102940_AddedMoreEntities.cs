using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class AddedMoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualTravelType",
                table: "PreferencesEntity");

            migrationBuilder.DropColumn(
                name: "PreferedTravelType",
                table: "PreferencesEntity");

            migrationBuilder.AddColumn<int>(
                name: "ActualTransportationType",
                table: "PreferencesEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferedTransportationType",
                table: "PreferencesEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnvironmentalInfoEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<int>(nullable: false),
                    CloudCoveragePercent = table.Column<int>(nullable: false),
                    Wind = table.Column<int>(nullable: false),
                    RoadCondition = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalInfoEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NudgeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    EnvironmentalInfoId = table.Column<Guid>(nullable: false),
                    NudgeResult = table.Column<int>(nullable: false),
                    TransportationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NudgeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NudgeEntity_EnvironmentalInfoEntity_EnvironmentalInfoId",
                        column: x => x.EnvironmentalInfoId,
                        principalTable: "EnvironmentalInfoEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NudgeEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UsedTransportationType = table.Column<int>(nullable: false),
                    DistanceTraveled = table.Column<int>(nullable: false),
                    EnvironmentalInfoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripEntity_EnvironmentalInfoEntity_EnvironmentalInfoId",
                        column: x => x.EnvironmentalInfoId,
                        principalTable: "EnvironmentalInfoEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NudgeEntity_EnvironmentalInfoId",
                table: "NudgeEntity",
                column: "EnvironmentalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_NudgeEntity_UserId",
                table: "NudgeEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TripEntity_EnvironmentalInfoId",
                table: "TripEntity",
                column: "EnvironmentalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TripEntity_UserId",
                table: "TripEntity",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NudgeEntity");

            migrationBuilder.DropTable(
                name: "TripEntity");

            migrationBuilder.DropTable(
                name: "EnvironmentalInfoEntity");

            migrationBuilder.DropColumn(
                name: "ActualTransportationType",
                table: "PreferencesEntity");

            migrationBuilder.DropColumn(
                name: "PreferedTransportationType",
                table: "PreferencesEntity");

            migrationBuilder.AddColumn<int>(
                name: "ActualTravelType",
                table: "PreferencesEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferedTravelType",
                table: "PreferencesEntity",
                nullable: false,
                defaultValue: 0);
        }
    }
}
