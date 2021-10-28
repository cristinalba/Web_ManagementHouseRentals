using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class AddSoloPhotoPropertyEntite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoMobile",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoMobile",
                table: "Properties");
        }
    }
}
