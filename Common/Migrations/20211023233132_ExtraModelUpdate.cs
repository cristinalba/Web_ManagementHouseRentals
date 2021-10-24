using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class ExtraModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_typeId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "typeId",
                table: "Properties",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_typeId",
                table: "Properties",
                newName: "IX_Properties_TypeId");

            migrationBuilder.RenameColumn(
                name: "NameExtra",
                table: "Extras",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "Assigned",
                table: "Extras",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_TypeId",
                table: "Properties",
                column: "TypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_TypeId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Assigned",
                table: "Extras");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Properties",
                newName: "typeId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_TypeId",
                table: "Properties",
                newName: "IX_Properties_typeId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Extras",
                newName: "NameExtra");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_typeId",
                table: "Properties",
                column: "typeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
