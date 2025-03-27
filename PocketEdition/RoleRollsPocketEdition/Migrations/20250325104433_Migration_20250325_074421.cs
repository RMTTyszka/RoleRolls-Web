using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250325_074421 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Encounters_EnconterId",
                table: "Creatures");

            migrationBuilder.RenameColumn(
                name: "EnconterId",
                table: "Creatures",
                newName: "EncounterId");

            migrationBuilder.RenameIndex(
                name: "IX_Creatures_EnconterId",
                table: "Creatures",
                newName: "IX_Creatures_EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Encounters_EncounterId",
                table: "Creatures",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Encounters_EncounterId",
                table: "Creatures");

            migrationBuilder.RenameColumn(
                name: "EncounterId",
                table: "Creatures",
                newName: "EnconterId");

            migrationBuilder.RenameIndex(
                name: "IX_Creatures_EncounterId",
                table: "Creatures",
                newName: "IX_Creatures_EnconterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Encounters_EnconterId",
                table: "Creatures",
                column: "EnconterId",
                principalTable: "Encounters",
                principalColumn: "Id");
        }
    }
}
