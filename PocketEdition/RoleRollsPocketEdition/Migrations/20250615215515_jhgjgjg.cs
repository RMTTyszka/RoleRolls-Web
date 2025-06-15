using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class jhgjgjg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignTemplateId1",
                table: "PowerTemplates");

            migrationBuilder.DropIndex(
                name: "IX_PowerTemplates_CampaignTemplateId1",
                table: "PowerTemplates");

            migrationBuilder.DropColumn(
                name: "CampaignTemplateId1",
                table: "PowerTemplates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaignTemplateId1",
                table: "PowerTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PowerTemplates_CampaignTemplateId1",
                table: "PowerTemplates",
                column: "CampaignTemplateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTemplates_CampaignTemplates_CampaignTemplateId1",
                table: "PowerTemplates",
                column: "CampaignTemplateId1",
                principalTable: "CampaignTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
