using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    public partial class AddedFormulaToLife : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Creatures_CreatureId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_MinorSkillTemplates_SkillTemplates_SkillTemplateId",
                table: "MinorSkillTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CreatureId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "LifeTemplates");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "LifeTemplates");

            migrationBuilder.DropColumn(
                name: "CreatureId",
                table: "Attributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "SkillTemplateId",
                table: "MinorSkillTemplates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Formula",
                table: "LifeTemplates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CampaignId",
                table: "Creatures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureTemplateId",
                table: "Creatures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Creatures",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Creatures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    AttributeTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_CreatureId",
                table: "Attribute",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinorSkillTemplates_SkillTemplates_SkillTemplateId",
                table: "MinorSkillTemplates",
                column: "SkillTemplateId",
                principalTable: "SkillTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorSkillTemplates_SkillTemplates_SkillTemplateId",
                table: "MinorSkillTemplates");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropColumn(
                name: "Formula",
                table: "LifeTemplates");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "CreatureTemplateId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Creatures");

            migrationBuilder.AlterColumn<Guid>(
                name: "SkillTemplateId",
                table: "MinorSkillTemplates",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "LifeTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "LifeTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureId",
                table: "Attributes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CreatureId",
                table: "Attributes",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Creatures_CreatureId",
                table: "Attributes",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinorSkillTemplates_SkillTemplates_SkillTemplateId",
                table: "MinorSkillTemplates",
                column: "SkillTemplateId",
                principalTable: "SkillTemplates",
                principalColumn: "Id");
        }
    }
}
