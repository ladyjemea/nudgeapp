using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudgeApp.Data.Migrations
{
    public partial class AddPushNotificationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushNotificationEntity");
        }
    }
}
