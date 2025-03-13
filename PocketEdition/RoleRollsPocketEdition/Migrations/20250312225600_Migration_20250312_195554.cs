using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250312_195554 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vitalities_Creatures_OwnerId",
                table: "Vitalities");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Vitalities",
                newName: "CreatureId");

            migrationBuilder.RenameIndex(
                name: "IX_Vitalities_OwnerId",
                table: "Vitalities",
                newName: "IX_Vitalities_CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vitalities_Creatures_CreatureId",
                table: "Vitalities",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vitalities_Creatures_CreatureId",
                table: "Vitalities");

            migrationBuilder.RenameColumn(
                name: "CreatureId",
                table: "Vitalities",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Vitalities_CreatureId",
                table: "Vitalities",
                newName: "IX_Vitalities_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vitalities_Creatures_OwnerId",
                table: "Vitalities",
                column: "OwnerId",
                principalTable: "Creatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
