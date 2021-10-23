using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class SizePropertyAndEnergyEntitiesModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extras_Properties_PropertyId",
                table: "Extras");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_EnergyCertificates_EnergyCertificateId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_typeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_SizeTypes_SizeTypeId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Extras_PropertyId",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Extras");

            migrationBuilder.AlterColumn<int>(
                name: "typeId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeTypeId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameProperty",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnergyCertificateId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ExtraProperty",
                columns: table => new
                {
                    ExtraId = table.Column<int>(type: "int", nullable: false),
                    PropertiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraProperty", x => new { x.ExtraId, x.PropertiesId });
                    table.ForeignKey(
                        name: "FK_ExtraProperty_Extras_ExtraId",
                        column: x => x.ExtraId,
                        principalTable: "Extras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtraProperty_Properties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraProperty_PropertiesId",
                table: "ExtraProperty",
                column: "PropertiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_EnergyCertificates_EnergyCertificateId",
                table: "Properties",
                column: "EnergyCertificateId",
                principalTable: "EnergyCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_typeId",
                table: "Properties",
                column: "typeId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_EnergyCertificates_EnergyCertificateId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_typeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_SizeTypes_SizeTypeId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "ExtraProperty");

            migrationBuilder.AlterColumn<int>(
                name: "typeId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SizeTypeId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameProperty",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "EnergyCertificateId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Extras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Extras_PropertyId",
                table: "Extras",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extras_Properties_PropertyId",
                table: "Extras",
                column: "PropertyId",
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
                name: "FK_Properties_PropertyTypes_typeId",
                table: "Properties",
                column: "typeId",
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
    }
}
