using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    public partial class addedSceneEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorSkills_Skills_SkillId",
                table: "MinorSkills");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.AlterColumn<Guid>(
                name: "SkillId",
                table: "MinorSkills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureId",
                table: "Attributes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CampaignScenes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignScenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    SceneId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorId = table.Column<Guid>(type: "uuid", nullable: true),
                    RolledDices = table.Column<string>(type: "text", nullable: false),
                    NumberOfDices = table.Column<int>(type: "integer", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyType = table.Column<int>(type: "integer", nullable: false),
                    NumberOfSuccesses = table.Column<int>(type: "integer", nullable: false),
                    NumberOfCriticalSuccesses = table.Column<int>(type: "integer", nullable: false),
                    NumberOfCriticalFailures = table.Column<int>(type: "integer", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    Complexity = table.Column<int>(type: "integer", nullable: false),
                    Success = table.Column<bool>(type: "boolean", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolls", x => x.Id);
                });

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
                name: "FK_MinorSkills_Skills_SkillId",
                table: "MinorSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Creatures_CreatureId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_MinorSkills_Skills_SkillId",
                table: "MinorSkills");

            migrationBuilder.DropTable(
                name: "CampaignScenes");

            migrationBuilder.DropTable(
                name: "Rolls");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CreatureId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "CreatureId",
                table: "Attributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "SkillId",
                table: "MinorSkills",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
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
                name: "FK_MinorSkills_Skills_SkillId",
                table: "MinorSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id");
        }
    }
}
