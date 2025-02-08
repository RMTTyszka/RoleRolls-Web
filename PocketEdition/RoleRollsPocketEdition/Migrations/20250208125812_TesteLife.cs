using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class TesteLife : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LifeTemplates_CampaignTemplates_CampaignTemplateId",
                table: "LifeTemplates");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignTemplateId",
                table: "LifeTemplates",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_LifeTemplates_CampaignTemplates_CampaignTemplateId",
                table: "LifeTemplates",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LifeTemplates_CampaignTemplates_CampaignTemplateId",
                table: "LifeTemplates");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignTemplateId",
                table: "LifeTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeTemplates_CampaignTemplates_CampaignTemplateId",
                table: "LifeTemplates",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
