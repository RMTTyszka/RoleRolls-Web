using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250603_121425 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Level",
                table: "ArchertypePowerDescriptions",
                newName: "RequiredLevel");

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "Bonus",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Target",
                table: "Bonus");

            migrationBuilder.RenameColumn(
                name: "RequiredLevel",
                table: "ArchertypePowerDescriptions",
                newName: "Level");
        }
    }
}
