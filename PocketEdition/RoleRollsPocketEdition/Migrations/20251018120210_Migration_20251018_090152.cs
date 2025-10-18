using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20251018_090152 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
