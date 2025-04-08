using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250408_125809 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchertypePowerSchematic");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Bonus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PowerTemplateId",
                table: "Bonus",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArchertypePowerDescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArchetypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Description = table.Column<byte[]>(type: "bytea", nullable: false),
                    GameDescription = table.Column<byte[]>(type: "bytea", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchertypePowerDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchertypePowerDescription_Archetypes_ArchetypeId",
                        column: x => x.ArchetypeId,
                        principalTable: "Archetypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_PowerTemplateId",
                table: "Bonus",
                column: "PowerTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchertypePowerDescription_ArchetypeId",
                table: "ArchertypePowerDescription",
                column: "ArchetypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_PowerTemplates_PowerTemplateId",
                table: "Bonus",
                column: "PowerTemplateId",
                principalTable: "PowerTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_PowerTemplates_PowerTemplateId",
                table: "Bonus");

            migrationBuilder.DropTable(
                name: "ArchertypePowerDescription");

            migrationBuilder.DropIndex(
                name: "IX_Bonus_PowerTemplateId",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "PowerTemplateId",
                table: "Bonus");

            migrationBuilder.CreateTable(
                name: "ArchertypePowerSchematic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArchetypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchertypePowerSchematic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchertypePowerSchematic_Archetypes_ArchetypeId",
                        column: x => x.ArchetypeId,
                        principalTable: "Archetypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArchertypePowerSchematic_PowerTemplates_PowerId",
                        column: x => x.PowerId,
                        principalTable: "PowerTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchertypePowerSchematic_ArchetypeId",
                table: "ArchertypePowerSchematic",
                column: "ArchetypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchertypePowerSchematic_PowerId",
                table: "ArchertypePowerSchematic",
                column: "PowerId");
        }
    }
}
