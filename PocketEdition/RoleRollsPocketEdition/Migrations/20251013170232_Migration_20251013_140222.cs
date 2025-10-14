using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20251013_140222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPointsPerSpecificSkill",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "MinPointsPerSpecificSkill",
                table: "Creatures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPointsPerSpecificSkill",
                table: "Creatures",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinPointsPerSpecificSkill",
                table: "Creatures",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
