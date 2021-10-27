using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class ModifyContractEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "Observations",
                table: "Contracts",
                newName: "GuidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Contracts",
                newName: "Observations");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Contracts",
                type: "datetime2",
                nullable: true);
        }
    }
}
