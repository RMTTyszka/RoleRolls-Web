using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_Archetypes_ArchetypeId",
                table: "Bonus",
                column: "ArchetypeId",
                principalTable: "Archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_CreatureTypes_CreatureTypeId",
                table: "Bonus",
                column: "CreatureTypeId",
                principalTable: "CreatureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_Archetypes_ArchetypeId",
                table: "Bonus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_CreatureTypes_CreatureTypeId",
                table: "Bonus");

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
