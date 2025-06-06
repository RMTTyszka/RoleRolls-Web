using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250606_172129 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Attributes_AttributeId",
                table: "Skills");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttributeId",
                table: "Skills",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureId",
                table: "Skills",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CreatureId",
                table: "Skills",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Attributes_AttributeId",
                table: "Skills",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Creatures_CreatureId",
                table: "Skills",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Attributes_AttributeId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Creatures_CreatureId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_CreatureId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CreatureId",
                table: "Skills");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttributeId",
                table: "Skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Attributes_AttributeId",
                table: "Skills",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
