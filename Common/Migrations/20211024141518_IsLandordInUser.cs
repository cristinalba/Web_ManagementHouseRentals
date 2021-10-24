using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class IsLandordInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLandlord",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLandlord",
                table: "AspNetUsers");
        }
    }
}
