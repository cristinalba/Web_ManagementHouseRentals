using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class EntityPropertyAnProperty_PhotoModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photography",
                table: "Property_Photos");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Property_Photos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Property_Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Property_Photos_PropertyId",
                table: "Property_Photos",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Photos_Properties_PropertyId",
                table: "Property_Photos",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Photos_Properties_PropertyId",
                table: "Property_Photos");

            migrationBuilder.DropIndex(
                name: "IX_Property_Photos_PropertyId",
                table: "Property_Photos");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Property_Photos");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Property_Photos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photography",
                table: "Property_Photos",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
