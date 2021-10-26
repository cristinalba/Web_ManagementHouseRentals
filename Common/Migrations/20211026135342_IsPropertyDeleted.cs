using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class IsPropertyDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraProperty_Extras_ExtraId",
                table: "ExtraProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraProperty_Properties_PropertiesId",
                table: "ExtraProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_EnergyCertificates_EnergyCertificateId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_TypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_SizeTypes_SizeTypeId",
                table: "Properties");

            migrationBuilder.AddColumn<bool>(
                name: "IsPropertyDeleted",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraProperty_Extras_ExtraId",
                table: "ExtraProperty",
                column: "ExtraId",
                principalTable: "Extras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraProperty_Properties_PropertiesId",
                table: "ExtraProperty",
                column: "PropertiesId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_EnergyCertificates_EnergyCertificateId",
                table: "Properties",
                column: "EnergyCertificateId",
                principalTable: "EnergyCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_TypeId",
                table: "Properties",
                column: "TypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_SizeTypes_SizeTypeId",
                table: "Properties",
                column: "SizeTypeId",
                principalTable: "SizeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraProperty_Extras_ExtraId",
                table: "ExtraProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraProperty_Properties_PropertiesId",
                table: "ExtraProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_EnergyCertificates_EnergyCertificateId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_TypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_SizeTypes_SizeTypeId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsPropertyDeleted",
                table: "Properties");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraProperty_Extras_ExtraId",
                table: "ExtraProperty",
                column: "ExtraId",
                principalTable: "Extras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraProperty_Properties_PropertiesId",
                table: "ExtraProperty",
                column: "PropertiesId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_EnergyCertificates_EnergyCertificateId",
                table: "Properties",
                column: "EnergyCertificateId",
                principalTable: "EnergyCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_TypeId",
                table: "Properties",
                column: "TypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_SizeTypes_SizeTypeId",
                table: "Properties",
                column: "SizeTypeId",
                principalTable: "SizeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
