using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class dsads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId",
                table: "PowerTemplates");

            migrationBuilder.DropIndex(
                name: "IX_PowerTemplates_CampaignId",
                table: "PowerTemplates");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "PowerTemplates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaignId",
                table: "PowerTemplates",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerTemplates_CampaignId",
                table: "PowerTemplates",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_Campaigns_CampaignId",
                table: "PowerTemplates",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id");
        }
    }
}
