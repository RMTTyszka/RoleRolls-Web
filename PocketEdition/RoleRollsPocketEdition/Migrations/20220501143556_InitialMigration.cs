using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Creatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreatureTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAttributePoints = table.Column<int>(type: "integer", nullable: false),
                    TotalSkillsPoints = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
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
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lifes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LifeTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lifes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lifes_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeTemplates_CreatureTemplates_CreatureTemplateId",
                        column: x => x.CreatureTemplateId,
                        principalTable: "CreatureTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LifeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeTemplates_CreatureTemplates_CreatureTemplateId",
                        column: x => x.CreatureTemplateId,
                        principalTable: "CreatureTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillTemplates_CreatureTemplates_CreatureTemplateId",
                        column: x => x.CreatureTemplateId,
                        principalTable: "CreatureTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MinorSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MinorSkillTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SkillProficience = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinorSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MinorSkillTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SkillProficience = table.Column<int>(type: "integer", nullable: false),
                    SkillTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorSkillTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinorSkillTemplates_SkillTemplates_SkillTemplateId",
                        column: x => x.SkillTemplateId,
                        principalTable: "SkillTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CreatureId",
                table: "Attributes",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeTemplates_CreatureTemplateId",
                table: "AttributeTemplates",
                column: "CreatureTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_CreatureId",
                table: "Lifes",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeTemplates_CreatureTemplateId",
                table: "LifeTemplates",
                column: "CreatureTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MinorSkills_SkillId",
                table: "MinorSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_MinorSkillTemplates_SkillTemplateId",
                table: "MinorSkillTemplates",
                column: "SkillTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CreatureId",
                table: "Skills",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillTemplates_CreatureTemplateId",
                table: "SkillTemplates",
                column: "CreatureTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "AttributeTemplates");

            migrationBuilder.DropTable(
                name: "Lifes");

            migrationBuilder.DropTable(
                name: "LifeTemplates");

            migrationBuilder.DropTable(
                name: "MinorSkills");

            migrationBuilder.DropTable(
                name: "MinorSkillTemplates");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SkillTemplates");

            migrationBuilder.DropTable(
                name: "Creatures");

            migrationBuilder.DropTable(
                name: "CreatureTemplates");
        }
    }
}
