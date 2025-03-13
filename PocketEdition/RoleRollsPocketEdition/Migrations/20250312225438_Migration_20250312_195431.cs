using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250312_195431 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vitalities_Creatures_CreatureId",
                table: "Vitalities");

            migrationBuilder.DropIndex(
                name: "IX_Vitalities_CreatureId",
                table: "Vitalities");

            migrationBuilder.DropColumn(
                name: "CreatureId",
                table: "Vitalities");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "Vitalities");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Vitalities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Vitalities_OwnerId",
                table: "Vitalities",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vitalities_Creatures_OwnerId",
                table: "Vitalities",
                column: "OwnerId",
                principalTable: "Creatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vitalities_Creatures_OwnerId",
                table: "Vitalities");

            migrationBuilder.DropIndex(
                name: "IX_Vitalities_OwnerId",
                table: "Vitalities");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Vitalities");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureId",
                table: "Vitalities",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "Vitalities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vitalities_CreatureId",
                table: "Vitalities",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vitalities_Creatures_CreatureId",
                table: "Vitalities",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id");
        }
    }
}
