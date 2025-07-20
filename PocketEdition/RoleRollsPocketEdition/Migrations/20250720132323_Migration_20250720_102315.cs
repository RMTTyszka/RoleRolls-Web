using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250720_102315 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Skills",
                newName: "Points");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Attributes",
                newName: "Points");

            migrationBuilder.AlterColumn<Guid>(
                name: "SkillId",
                table: "MinorSkills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BlockProperty_Id",
                table: "ItemConfigurations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlockProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockProperty_Id",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "BlockProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "Skills",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "Attributes",
                newName: "Value");

            migrationBuilder.AlterColumn<Guid>(
                name: "SkillId",
                table: "MinorSkills",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
