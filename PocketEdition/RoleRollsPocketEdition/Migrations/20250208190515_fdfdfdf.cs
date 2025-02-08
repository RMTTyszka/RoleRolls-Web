using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class fdfdfdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_Archetypes_ArchetypeId",
                table: "Bonus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_CreatureTypes_CreatureTypeId",
                table: "Bonus");

            migrationBuilder.DropIndex(
                name: "IX_Bonus_ArchetypeId",
                table: "Bonus");

            migrationBuilder.DropIndex(
                name: "IX_Bonus_CreatureTypeId",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "ArchetypeId",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "CreatureTypeId",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "Bonus");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_EntityId",
                table: "Bonus",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_Archetypes_EntityId",
                table: "Bonus",
                column: "EntityId",
                principalTable: "Archetypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_CreatureTypes_EntityId",
                table: "Bonus",
                column: "EntityId",
                principalTable: "CreatureTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_Archetypes_EntityId",
                table: "Bonus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_CreatureTypes_EntityId",
                table: "Bonus");

            migrationBuilder.DropIndex(
                name: "IX_Bonus_EntityId",
                table: "Bonus");

            migrationBuilder.AddColumn<Guid>(
                name: "ArchetypeId",
                table: "Bonus",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureTypeId",
                table: "Bonus",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntityType",
                table: "Bonus",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_ArchetypeId",
                table: "Bonus",
                column: "ArchetypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_CreatureTypeId",
                table: "Bonus",
                column: "CreatureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_Archetypes_ArchetypeId",
                table: "Bonus",
                column: "ArchetypeId",
                principalTable: "Archetypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_CreatureTypes_CreatureTypeId",
                table: "Bonus",
                column: "CreatureTypeId",
                principalTable: "CreatureTypes",
                principalColumn: "Id");
        }
    }
}
