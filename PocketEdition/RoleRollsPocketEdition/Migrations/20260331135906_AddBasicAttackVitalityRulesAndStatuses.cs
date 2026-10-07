#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicAttackVitalityRulesAndStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BasicAttackVitalityRules",
                table: "ItemConfigurations",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicAttackVitalityRules",
                table: "ItemConfigurations");
        }
    }
}
