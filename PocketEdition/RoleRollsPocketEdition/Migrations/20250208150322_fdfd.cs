using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class fdfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lifes_LifeTemplate_LifeTemplateId",
                table: "Lifes");

            migrationBuilder.DropForeignKey(
                name: "FK_LifeTemplate_CampaignTemplates_CampaignTemplateId",
                table: "LifeTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LifeTemplate",
                table: "LifeTemplate");

            migrationBuilder.RenameTable(
                name: "LifeTemplate",
                newName: "LifeTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_LifeTemplate_CampaignTemplateId",
                table: "LifeTemplates",
                newName: "IX_LifeTemplates_CampaignTemplateId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignTemplateId",
                table: "LifeTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureTemplateId",
                table: "LifeTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LifeTemplates",
                table: "LifeTemplates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lifes_LifeTemplates_LifeTemplateId",
                table: "Lifes",
                column: "LifeTemplateId",
                principalTable: "LifeTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeTemplates_CampaignTemplates_CampaignTemplateId",
                table: "LifeTemplates",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lifes_LifeTemplates_LifeTemplateId",
                table: "Lifes");

            migrationBuilder.DropForeignKey(
                name: "FK_LifeTemplates_CampaignTemplates_CampaignTemplateId",
                table: "LifeTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LifeTemplates",
                table: "LifeTemplates");

            migrationBuilder.DropColumn(
                name: "CreatureTemplateId",
                table: "LifeTemplates");

            migrationBuilder.RenameTable(
                name: "LifeTemplates",
                newName: "LifeTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_LifeTemplates_CampaignTemplateId",
                table: "LifeTemplate",
                newName: "IX_LifeTemplate_CampaignTemplateId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignTemplateId",
                table: "LifeTemplate",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LifeTemplate",
                table: "LifeTemplate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lifes_LifeTemplate_LifeTemplateId",
                table: "Lifes",
                column: "LifeTemplateId",
                principalTable: "LifeTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeTemplate_CampaignTemplates_CampaignTemplateId",
                table: "LifeTemplate",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id");
        }
    }
}
