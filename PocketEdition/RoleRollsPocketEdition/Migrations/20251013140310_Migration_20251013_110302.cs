using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20251013_110302 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorSkillTemplates_AttributeTemplates_AttributeTemplateId",
                table: "MinorSkillTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillTemplates_AttributeTemplates_AttributeTemplateId",
                table: "SkillTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillTemplates_CampaignTemplates_CampaignTemplateId",
                table: "SkillTemplates");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignTemplateId",
                table: "SkillTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MinorSkillTemplates_AttributeTemplates_AttributeTemplateId",
                table: "MinorSkillTemplates",
                column: "AttributeTemplateId",
                principalTable: "AttributeTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillTemplates_AttributeTemplates_AttributeTemplateId",
                table: "SkillTemplates",
                column: "AttributeTemplateId",
                principalTable: "AttributeTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillTemplates_CampaignTemplates_CampaignTemplateId",
                table: "SkillTemplates",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorSkillTemplates_AttributeTemplates_AttributeTemplateId",
                table: "MinorSkillTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillTemplates_AttributeTemplates_AttributeTemplateId",
                table: "SkillTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillTemplates_CampaignTemplates_CampaignTemplateId",
                table: "SkillTemplates");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignTemplateId",
                table: "SkillTemplates",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_MinorSkillTemplates_AttributeTemplates_AttributeTemplateId",
                table: "MinorSkillTemplates",
                column: "AttributeTemplateId",
                principalTable: "AttributeTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillTemplates_AttributeTemplates_AttributeTemplateId",
                table: "SkillTemplates",
                column: "AttributeTemplateId",
                principalTable: "AttributeTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillTemplates_CampaignTemplates_CampaignTemplateId",
                table: "SkillTemplates",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id");
        }
    }
}
