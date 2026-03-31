using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatureConditionsAndVitalityConditionReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusAtThirtyPercent",
                table: "VitalityTemplates");

            migrationBuilder.DropColumn(
                name: "StatusAtZero",
                table: "VitalityTemplates");

            migrationBuilder.AddColumn<Guid>(
                name: "ConditionAtThirtyPercent_Id",
                table: "VitalityTemplates",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConditionAtThirtyPercent_Type",
                table: "VitalityTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConditionAtZero_Id",
                table: "VitalityTemplates",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConditionAtZero_Type",
                table: "VitalityTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureConditionId",
                table: "Bonus",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreatureConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    Description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreatureConditions_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_CreatureConditionId",
                table: "Bonus",
                column: "CreatureConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureConditions_CampaignTemplateId",
                table: "CreatureConditions",
                column: "CampaignTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_CreatureConditions_CreatureConditionId",
                table: "Bonus",
                column: "CreatureConditionId",
                principalTable: "CreatureConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_CreatureConditions_CreatureConditionId",
                table: "Bonus");

            migrationBuilder.DropTable(
                name: "CreatureConditions");

            migrationBuilder.DropIndex(
                name: "IX_Bonus_CreatureConditionId",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "ConditionAtThirtyPercent_Id",
                table: "VitalityTemplates");

            migrationBuilder.DropColumn(
                name: "ConditionAtThirtyPercent_Type",
                table: "VitalityTemplates");

            migrationBuilder.DropColumn(
                name: "ConditionAtZero_Id",
                table: "VitalityTemplates");

            migrationBuilder.DropColumn(
                name: "ConditionAtZero_Type",
                table: "VitalityTemplates");

            migrationBuilder.DropColumn(
                name: "CreatureConditionId",
                table: "Bonus");

            migrationBuilder.AddColumn<string>(
                name: "StatusAtThirtyPercent",
                table: "VitalityTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusAtZero",
                table: "VitalityTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);
        }
    }
}
