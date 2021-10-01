using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_ManagementHouseRentals.Migrations
{
    public partial class addProposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Photo_Photos_PhotoId",
                table: "Property_Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property_Photo",
                table: "Property_Photo");

            migrationBuilder.RenameTable(
                name: "Property_Photo",
                newName: "Property_Photos");

            migrationBuilder.RenameIndex(
                name: "IX_Property_Photo_PhotoId",
                table: "Property_Photos",
                newName: "IX_Property_Photos_PhotoId");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property_Photos",
                table: "Property_Photos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProposalState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    propertyId = table.Column<int>(type: "int", nullable: true),
                    proposalStateId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_Properties_propertyId",
                        column: x => x.propertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposals_ProposalState_proposalStateId",
                        column: x => x.proposalStateId,
                        principalTable: "ProposalState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PropertyId",
                table: "Contracts",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_propertyId",
                table: "Proposals",
                column: "propertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_proposalStateId",
                table: "Proposals",
                column: "proposalStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Properties_PropertyId",
                table: "Contracts",
                column: "PropertyId",
                principalTable: "Properties",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Properties_PropertyId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Photos_Photos_PhotoId",
                table: "Property_Photos");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "ProposalState");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_PropertyId",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property_Photos",
                table: "Property_Photos");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Property_Photos",
                newName: "Property_Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Property_Photos_PhotoId",
                table: "Property_Photo",
                newName: "IX_Property_Photo_PhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property_Photo",
                table: "Property_Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Photo_Photos_PhotoId",
                table: "Property_Photo",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
