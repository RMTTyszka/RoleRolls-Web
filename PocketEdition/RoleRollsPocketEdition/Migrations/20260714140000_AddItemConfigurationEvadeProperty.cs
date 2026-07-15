using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using RoleRollsPocketEdition.Infrastructure;

#nullable disable

namespace RoleRollsPocketEdition.Migrations;

[DbContext(typeof(RoleRollsDbContext))]
[Migration("20260714140000_AddItemConfigurationEvadeProperty")]
public partial class AddItemConfigurationEvadeProperty : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "EvadeProperty_Id",
            table: "ItemConfigurations",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "EvadeProperty_Type",
            table: "ItemConfigurations",
            type: "integer",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "EvadeProperty_Id",
            table: "ItemConfigurations");

        migrationBuilder.DropColumn(
            name: "EvadeProperty_Type",
            table: "ItemConfigurations");
    }
}
