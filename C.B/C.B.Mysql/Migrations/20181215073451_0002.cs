using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C.B.Mysql.Migrations
{
    public partial class _0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ResourceInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    Filepath = table.Column<string>(maxLength: 128, nullable: true),
                    Url = table.Column<string>(maxLength: 128, nullable: true),
                    FileMd5 = table.Column<string>(maxLength: 64, nullable: true),
                    FileName = table.Column<string>(maxLength: 64, nullable: true),
                    FileType = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceInfo", x => x.Id);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventInfo");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "ExpertInfo");

            migrationBuilder.DropTable(
                name: "HisEventInfo");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "NewsInfo");

            migrationBuilder.DropTable(
                name: "Notice");

            migrationBuilder.DropTable(
                name: "ResourceInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
