using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class ProposalEntityMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checked",
                table: "Proposals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "Proposals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
