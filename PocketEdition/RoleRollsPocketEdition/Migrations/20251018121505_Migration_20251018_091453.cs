using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20251018_091453 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Archetypes_ArchetypeId",
                table: "Spells");

            migrationBuilder.DropIndex(
                name: "IX_Spells_ArchetypeId",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "ArchetypeId",
                table: "Spells");

            migrationBuilder.CreateTable(
                name: "ArchetypeSpells",
                columns: table => new
                {
                    ArchetypesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpellsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchetypeSpells", x => new { x.ArchetypesId, x.SpellsId });
                    table.ForeignKey(
                        name: "FK_ArchetypeSpells_Archetypes_ArchetypesId",
                        column: x => x.ArchetypesId,
                        principalTable: "Archetypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArchetypeSpells_Spells_SpellsId",
                        column: x => x.SpellsId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchetypeSpells_SpellsId",
                table: "ArchetypeSpells",
                column: "SpellsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchetypeSpells");

            migrationBuilder.AddColumn<Guid>(
                name: "ArchetypeId",
                table: "Spells",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Spells_ArchetypeId",
                table: "Spells",
                column: "ArchetypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spells_Archetypes_ArchetypeId",
                table: "Spells",
                column: "ArchetypeId",
                principalTable: "Archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
