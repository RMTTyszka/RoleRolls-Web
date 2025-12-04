using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20251204_133614 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArmorProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.RenameColumn(
                name: "ArmorProperty_Id",
                table: "ItemConfigurations",
                newName: "ArmorDefense2");

            migrationBuilder.AddColumn<Guid>(
                name: "ArmorDefense1",
                table: "ItemConfigurations",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArmorDefense1",
                table: "ItemConfigurations");

            migrationBuilder.RenameColumn(
                name: "ArmorDefense2",
                table: "ItemConfigurations",
                newName: "ArmorProperty_Id");

            migrationBuilder.AddColumn<int>(
                name: "ArmorProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);
        }
    }
}
