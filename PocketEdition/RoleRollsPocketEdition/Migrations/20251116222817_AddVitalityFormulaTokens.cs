#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class AddVitalityFormulaTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormulaTokens",
                table: "VitalityTemplates",
                type: "jsonb",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormulaTokens",
                table: "VitalityTemplates");
        }
    }
}
