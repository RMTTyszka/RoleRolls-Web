using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250507_121344 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaignId1",
                table: "PowerTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PowerTemplates_CampaignId1",
                table: "PowerTemplates",
                column: "CampaignId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignId",
                table: "PowerTemplates",
                column: "CampaignId",
                principalTable: "CampaignTemplates",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignId",
                table: "PowerTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId1",
                table: "PowerTemplates");

            migrationBuilder.DropIndex(
                name: "IX_PowerTemplates_CampaignId1",
                table: "PowerTemplates");

            migrationBuilder.DropColumn(
                name: "CampaignId1",
                table: "PowerTemplates");
        }
    }
}
