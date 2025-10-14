using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20251013_140813 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSkillsPointsLimit",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "TotalSkillsPoints",
                table: "CampaignTemplates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalSkillsPointsLimit",
                table: "Creatures",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSkillsPoints",
                table: "CampaignTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
