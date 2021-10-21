using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class deletePhotoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Photos_Cod_PhotoId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Photos_Photos_PhotoId",
                table: "Property_Photos");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Property_Photos_PhotoId",
                table: "Property_Photos");

            migrationBuilder.DropIndex(
                name: "IX_Properties_Cod_PhotoId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Property_Photos");

            migrationBuilder.DropColumn(
                name: "Cod_PhotoId",
                table: "Properties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Property_Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cod_PhotoId",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_Photos_PhotoId",
                table: "Property_Photos",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_Cod_PhotoId",
                table: "Properties",
                column: "Cod_PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Photos_Cod_PhotoId",
                table: "Properties",
                column: "Cod_PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Photos_Photos_PhotoId",
                table: "Property_Photos",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
