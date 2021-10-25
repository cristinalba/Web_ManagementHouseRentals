using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class ProposalEntityModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_ProposalState_proposalStateId",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProposalState",
                table: "ProposalState");

            migrationBuilder.RenameTable(
                name: "ProposalState",
                newName: "ProposalStates");

            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "Proposals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Proposals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Proposals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProposalDate",
                table: "Proposals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProposalStates",
                table: "ProposalStates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ClientId",
                table: "Proposals",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_OwnerId",
                table: "Proposals",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_AspNetUsers_ClientId",
                table: "Proposals",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_AspNetUsers_OwnerId",
                table: "Proposals",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_ProposalStates_proposalStateId",
                table: "Proposals",
                column: "proposalStateId",
                principalTable: "ProposalStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_AspNetUsers_ClientId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_AspNetUsers_OwnerId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_ProposalStates_proposalStateId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ClientId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_OwnerId",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProposalStates",
                table: "ProposalStates");

            migrationBuilder.DropColumn(
                name: "Checked",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ProposalDate",
                table: "Proposals");

            migrationBuilder.RenameTable(
                name: "ProposalStates",
                newName: "ProposalState");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProposalState",
                table: "ProposalState",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_ProposalState_proposalStateId",
                table: "Proposals",
                column: "proposalStateId",
                principalTable: "ProposalState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
