using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class DeleteZipCodeEntity : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_ZipCodes_ZipCodeId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "ZipCodes");

            migrationBuilder.DropIndex(
                name: "IX_Properties_ZipCodeId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ZipCodeId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Properties",
                newName: "Long");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Properties",
                newName: "Lat");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "ZipCode",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "Long",
                table: "Properties",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "Properties",
                newName: "Latitude");

            migrationBuilder.AddColumn<int>(
                name: "ZipCodeId",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ZipCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ZipCodeId",
                table: "Properties",
                column: "ZipCodeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_ZipCodes_ZipCodeId",
                table: "Properties",
                column: "ZipCodeId",
                principalTable: "ZipCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
