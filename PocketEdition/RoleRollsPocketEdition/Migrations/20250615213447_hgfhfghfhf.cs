using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class hgfhfghfhf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignId",
                table: "PowerTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId",
                table: "PowerTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId1",
                table: "PowerTemplates");

            migrationBuilder.RenameColumn(
                name: "CampaignId1",
                table: "PowerTemplates",
                newName: "CampaignTemplateId1");

            migrationBuilder.RenameIndex(
                name: "IX_PowerTemplates_CampaignId1",
                table: "PowerTemplates",
                newName: "IX_PowerTemplates_CampaignTemplateId1");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignId",
                table: "PowerTemplates",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CampaignTemplateId",
                table: "PowerTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PowerTemplates_CampaignTemplateId",
                table: "PowerTemplates",
                column: "CampaignTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignTemplateId",
                table: "PowerTemplates",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignTemplateId1",
                table: "PowerTemplates",
                column: "CampaignTemplateId1",
                principalTable: "CampaignTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId",
                table: "PowerTemplates",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignTemplateId",
                table: "PowerTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignTemplateId1",
                table: "PowerTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId",
                table: "PowerTemplates");

            migrationBuilder.DropIndex(
                name: "IX_PowerTemplates_CampaignTemplateId",
                table: "PowerTemplates");

            migrationBuilder.DropColumn(
                name: "CampaignTemplateId",
                table: "PowerTemplates");

            migrationBuilder.RenameColumn(
                name: "CampaignTemplateId1",
                table: "PowerTemplates",
                newName: "CampaignId1");

            migrationBuilder.RenameIndex(
                name: "IX_PowerTemplates_CampaignTemplateId1",
                table: "PowerTemplates",
                newName: "IX_PowerTemplates_CampaignId1");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignId",
                table: "PowerTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignId",
                table: "PowerTemplates",
                column: "CampaignId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId",
                table: "PowerTemplates",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId1",
                table: "PowerTemplates",
                column: "CampaignId1",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
