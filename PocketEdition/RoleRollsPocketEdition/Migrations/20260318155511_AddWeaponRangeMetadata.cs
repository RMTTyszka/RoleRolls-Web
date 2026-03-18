using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class AddWeaponRangeMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRanged",
                table: "ItemTemplates",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Range",
                table: "ItemTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRanged",
                table: "ItemTemplates");

            migrationBuilder.DropColumn(
                name: "Range",
                table: "ItemTemplates");
        }
    }
}
