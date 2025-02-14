using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class archetypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaignTemplateId",
                table: "Archetypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Archetypes_CampaignTemplateId",
                table: "Archetypes",
                column: "CampaignTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archetypes_CampaignTemplates_CampaignTemplateId",
                table: "Archetypes",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archetypes_CampaignTemplates_CampaignTemplateId",
                table: "Archetypes");

            migrationBuilder.DropIndex(
                name: "IX_Archetypes_CampaignTemplateId",
                table: "Archetypes");

            migrationBuilder.DropColumn(
                name: "CampaignTemplateId",
                table: "Archetypes");
        }
    }
}
