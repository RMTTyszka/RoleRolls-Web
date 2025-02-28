using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class addedArchetypeCreatureTypeToCreature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ArchetypeId",
                table: "Creatures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureTypeId",
                table: "Creatures",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_ArchetypeId",
                table: "Creatures",
                column: "ArchetypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_CreatureTypeId",
                table: "Creatures",
                column: "CreatureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Archetypes_ArchetypeId",
                table: "Creatures",
                column: "ArchetypeId",
                principalTable: "Archetypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_CreatureTypes_CreatureTypeId",
                table: "Creatures",
                column: "CreatureTypeId",
                principalTable: "CreatureTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Archetypes_ArchetypeId",
                table: "Creatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_CreatureTypes_CreatureTypeId",
                table: "Creatures");

            migrationBuilder.DropIndex(
                name: "IX_Creatures_ArchetypeId",
                table: "Creatures");

            migrationBuilder.DropIndex(
                name: "IX_Creatures_CreatureTypeId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "ArchetypeId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "CreatureTypeId",
                table: "Creatures");
        }
    }
}
