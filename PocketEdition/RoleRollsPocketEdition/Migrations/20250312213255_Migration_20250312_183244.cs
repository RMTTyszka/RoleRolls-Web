using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250312_183244 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lifes");

            migrationBuilder.DropTable(
                name: "LifeTemplates");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetSecondLife_Type",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetSecondVitality_Type");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetSecondLife_PropertyId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetSecondVitality_PropertyId");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetFirstLife_Type",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetFirstVitality_Type");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetFirstLife_PropertyId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetFirstVitality_PropertyId");

            migrationBuilder.AddColumn<bool>(
                name: "CanBeAlly",
                table: "CreatureTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanBeEnemy",
                table: "CreatureTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "VitalityTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Formula = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalityTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VitalityTemplates_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vitalities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Formula = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    VitalityTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vitalities_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vitalities_VitalityTemplates_VitalityTemplateId",
                        column: x => x.VitalityTemplateId,
                        principalTable: "VitalityTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vitalities_CreatureId",
                table: "Vitalities",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Vitalities_VitalityTemplateId",
                table: "Vitalities",
                column: "VitalityTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalityTemplates_CampaignTemplateId",
                table: "VitalityTemplates",
                column: "CampaignTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vitalities");

            migrationBuilder.DropTable(
                name: "VitalityTemplates");

            migrationBuilder.DropColumn(
                name: "CanBeAlly",
                table: "CreatureTypes");

            migrationBuilder.DropColumn(
                name: "CanBeEnemy",
                table: "CreatureTypes");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetSecondVitality_Type",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetSecondLife_Type");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetSecondVitality_PropertyId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetSecondLife_PropertyId");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetFirstVitality_Type",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetFirstLife_Type");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetFirstVitality_PropertyId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetFirstLife_PropertyId");

            migrationBuilder.CreateTable(
                name: "LifeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Formula = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeTemplates_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lifes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LifeTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true),
                    Formula = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lifes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lifes_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lifes_LifeTemplates_LifeTemplateId",
                        column: x => x.LifeTemplateId,
                        principalTable: "LifeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_CreatureId",
                table: "Lifes",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_LifeTemplateId",
                table: "Lifes",
                column: "LifeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeTemplates_CampaignTemplateId",
                table: "LifeTemplates",
                column: "CampaignTemplateId");
        }
    }
}
