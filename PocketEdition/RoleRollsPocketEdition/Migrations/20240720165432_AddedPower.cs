using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class AddedPower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Powers");

            migrationBuilder.CreateTable(
                name: "PowerTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PowerDurationType = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    ActionType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CastFormula = table.Column<string>(type: "text", nullable: false),
                    CastDescription = table.Column<string>(type: "text", nullable: false),
                    UseAttributeId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetDefenseId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsagesFormula = table.Column<string>(type: "text", nullable: false),
                    UsageType = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerTemplates_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreaturePowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    PowerTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsumedUsages = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreaturePowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreaturePowers_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreaturePowers_PowerTemplates_PowerTemplateId",
                        column: x => x.PowerTemplateId,
                        principalTable: "PowerTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreaturePowers_CreatureId",
                table: "CreaturePowers",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CreaturePowers_PowerTemplateId",
                table: "CreaturePowers",
                column: "PowerTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerTemplates_CampaignId",
                table: "PowerTemplates",
                column: "CampaignId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreaturePowers");

            migrationBuilder.DropTable(
                name: "PowerTemplates");

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PowerTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetDefense = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UseAttribute = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.Id);
                });
        }
    }
}
