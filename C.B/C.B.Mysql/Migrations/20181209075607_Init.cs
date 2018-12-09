using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C.B.MySql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Author = table.Column<string>(maxLength: 32, nullable: true),
                    IsShow = table.Column<int>(nullable: false),
                    IsHot = table.Column<int>(nullable: false),
                    ThumbPath = table.Column<string>(maxLength: 64, nullable: true),
                    ThumbPicId = table.Column<int>(nullable: false),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    IsShow = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(maxLength: 128, nullable: true),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpertInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Content = table.Column<string>(maxLength: 512, nullable: true),
                    PidFileId = table.Column<int>(nullable: false),
                    Author = table.Column<string>(maxLength: 32, nullable: true),
                    IsShow = table.Column<int>(nullable: false),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    filepath = table.Column<string>(maxLength: 128, nullable: true),
                    fileMd5 = table.Column<string>(maxLength: 64, nullable: true),
                    fileName = table.Column<string>(maxLength: 64, nullable: true),
                    fileType = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HisEventInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Content = table.Column<string>(maxLength: 2048, nullable: true),
                    Year = table.Column<int>(nullable: false),
                    PicId = table.Column<int>(nullable: false),
                    Link = table.Column<int>(maxLength: 128, nullable: false),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HisEventInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Content = table.Column<string>(maxLength: 512, nullable: true),
                    Region = table.Column<string>(maxLength: 64, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    replyContent = table.Column<string>(maxLength: 512, nullable: true),
                    replyName = table.Column<string>(maxLength: 64, nullable: true),
                    replyTime = table.Column<DateTime>(nullable: false),
                    IsShow = table.Column<int>(nullable: false),
                    IsHot = table.Column<int>(nullable: false),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Content = table.Column<string>(maxLength: 2048, nullable: true),
                    PubTime = table.Column<DateTime>(nullable: false),
                    PubOrg = table.Column<string>(maxLength: 64, nullable: true),
                    Author = table.Column<string>(maxLength: 32, nullable: true),
                    IsShow = table.Column<int>(nullable: false),
                    IsTop = table.Column<int>(nullable: false),
                    IsRoll = table.Column<int>(nullable: false),
                    ThumbId = table.Column<int>(nullable: false),
                    NewsType = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Content = table.Column<string>(maxLength: 2048, nullable: true),
                    PubTime = table.Column<DateTime>(nullable: false),
                    PubOrg = table.Column<string>(maxLength: 64, nullable: true),
                    Author = table.Column<string>(maxLength: 32, nullable: true),
                    IsShow = table.Column<int>(nullable: false),
                    IsTop = table.Column<int>(nullable: false),
                    IsRoll = table.Column<int>(nullable: false),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<short>(nullable: false),
                    UserName = table.Column<string>(maxLength: 32, nullable: true),
                    Password = table.Column<string>(maxLength: 32, nullable: true),
                    TrueName = table.Column<string>(maxLength: 32, nullable: true),
                    Department = table.Column<string>(maxLength: 32, nullable: true),
                    State = table.Column<string>(nullable: true),
                    AuthType = table.Column<int>(nullable: false),
                    MobileNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
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
                name: "FileInfo");

            migrationBuilder.DropTable(
                name: "HisEventInfo");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "NewsInfo");

            migrationBuilder.DropTable(
                name: "Notice");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
