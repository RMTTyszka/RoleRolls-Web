using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20251013_114357 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Attributes_AttributeId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_AttributeId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "Skills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttributeId",
                table: "Skills",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_AttributeId",
                table: "Skills",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Attributes_AttributeId",
                table: "Skills",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id");
        }
    }
}
