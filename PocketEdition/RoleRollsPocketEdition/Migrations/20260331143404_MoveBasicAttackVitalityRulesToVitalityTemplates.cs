using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class MoveBasicAttackVitalityRulesToVitalityTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasicAttackOrder",
                table: "VitalityTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusAtThirtyPercent",
                table: "VitalityTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusAtZero",
                table: "VitalityTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.DropColumn(
                name: "BasicAttackVitalityRules",
                table: "ItemConfigurations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicAttackOrder",
                table: "VitalityTemplates");

            migrationBuilder.DropColumn(
                name: "StatusAtThirtyPercent",
                table: "VitalityTemplates");

            migrationBuilder.DropColumn(
                name: "StatusAtZero",
                table: "VitalityTemplates");

            migrationBuilder.AddColumn<string>(
                name: "BasicAttackVitalityRules",
                table: "ItemConfigurations",
                type: "jsonb",
                nullable: true);
        }
    }
}
