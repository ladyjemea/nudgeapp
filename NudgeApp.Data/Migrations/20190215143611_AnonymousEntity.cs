using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class AnonymousEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnonymousNudgeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Result = table.Column<int>(nullable: false),
                    UserPreferedTransportationType = table.Column<int>(nullable: false),
                    ActualTransportationType = table.Column<int>(nullable: false),
                    SkyCoverage = table.Column<int>(nullable: false),
                    Road = table.Column<int>(nullable: false),
                    Temperature = table.Column<int>(nullable: false),
                    Wind = table.Column<int>(nullable: false),
                    Precipitation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousNudgeEntity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonymousNudgeEntity");
        }
    }
}
