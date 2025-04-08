using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250408_170214 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchertypePowerDescription_Archetypes_ArchetypeId",
                table: "ArchertypePowerDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchertypePowerDescription",
                table: "ArchertypePowerDescription");

            migrationBuilder.RenameTable(
                name: "ArchertypePowerDescription",
                newName: "ArchertypePowerDescriptions");

            migrationBuilder.RenameIndex(
                name: "IX_ArchertypePowerDescription_ArchetypeId",
                table: "ArchertypePowerDescriptions",
                newName: "IX_ArchertypePowerDescriptions_ArchetypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchertypePowerDescriptions",
                table: "ArchertypePowerDescriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchertypePowerDescriptions_Archetypes_ArchetypeId",
                table: "ArchertypePowerDescriptions",
                column: "ArchetypeId",
                principalTable: "Archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchertypePowerDescriptions_Archetypes_ArchetypeId",
                table: "ArchertypePowerDescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchertypePowerDescriptions",
                table: "ArchertypePowerDescriptions");

            migrationBuilder.RenameTable(
                name: "ArchertypePowerDescriptions",
                newName: "ArchertypePowerDescription");

            migrationBuilder.RenameIndex(
                name: "IX_ArchertypePowerDescriptions_ArchetypeId",
                table: "ArchertypePowerDescription",
                newName: "IX_ArchertypePowerDescription_ArchetypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchertypePowerDescription",
                table: "ArchertypePowerDescription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchertypePowerDescription_Archetypes_ArchetypeId",
                table: "ArchertypePowerDescription",
                column: "ArchetypeId",
                principalTable: "Archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
