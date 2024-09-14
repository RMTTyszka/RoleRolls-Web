using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class RenamedWeaponsCOlumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "ItemTemplates",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "ItemInstances",
                newName: "DamageType");

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "ItemTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "ItemInstances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ItemTemplates");

            migrationBuilder.RenameColumn(
                name: "DamageType",
                table: "ItemTemplates",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "DamageType",
                table: "ItemInstances",
                newName: "Size");

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateId",
                table: "ItemInstances",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
