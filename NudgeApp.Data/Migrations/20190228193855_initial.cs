using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnonymousNudgeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Result = table.Column<int>(nullable: false),
                    UserPreferedTransportationType = table.Column<int>(nullable: false),
                    ActualTransportationType = table.Column<int>(nullable: false),
                    SkyCoverage = table.Column<int>(nullable: false),
                    RoadCondition = table.Column<int>(nullable: false),
                    Temperature = table.Column<float>(nullable: false),
                    Wind = table.Column<float>(nullable: false),
                    PrecipitationProbability = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousNudgeEntity", x => x.Id);
                });

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
                name: "PushNotificationEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Endpoint = table.Column<string>(nullable: true),
                    P256DH = table.Column<string>(nullable: true),
                    Auth = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
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
                name: "PreferencesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    PreferedTransportationType = table.Column<int>(nullable: false),
                    ActualTransportationType = table.Column<int>(nullable: false),
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
                name: "IX_PreferencesEntity_UserId",
                table: "PreferencesEntity",
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
                name: "AnonymousNudgeEntity");

            migrationBuilder.DropTable(
                name: "NudgeEntity");

            migrationBuilder.DropTable(
                name: "PreferencesEntity");

            migrationBuilder.DropTable(
                name: "PushNotificationEntity");

            migrationBuilder.DropTable(
                name: "TripEntity");

            migrationBuilder.DropTable(
                name: "EnvironmentalInfoEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
