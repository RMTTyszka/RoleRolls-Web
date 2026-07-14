using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using RoleRollsPocketEdition.Infrastructure;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(RoleRollsDbContext))]
    [Migration("20260714130000_AddWeaponTemplateGripType")]
    public partial class AddWeaponTemplateGripType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GripType",
                table: "ItemTemplates",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GripType",
                table: "ItemTemplates");
        }
    }
}
