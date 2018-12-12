using Microsoft.EntityFrameworkCore.Migrations;

namespace C.B.MySql.Migrations
{
    public partial class _0000002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "UserInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "UserInfo",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
