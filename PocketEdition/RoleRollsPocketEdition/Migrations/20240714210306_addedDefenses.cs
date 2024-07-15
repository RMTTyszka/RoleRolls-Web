using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class addedDefenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PowerTemplateId",
                table: "Powers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureId",
                table: "Defenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DefenseTemplateId",
                table: "Defenses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DefenseTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Formula = table.Column<string>(type: "text", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefenseTemplates", x => x.Id);
                    table.ForeignKey(
                        
                        name: "FK_DefenseTemplates_CreatureTemplates_CreatureTemplateId",
                        column: x => x.CreatureTemplateId,
                        principalTable: "CreatureTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Defenses_CreatureId",
                table: "Defenses",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseTemplates_CreatureTemplateId",
                table: "DefenseTemplates",
                column: "CreatureTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defenses_Creatures_CreatureId",
                table: "Defenses",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defenses_Creatures_CreatureId",
                table: "Defenses");

            migrationBuilder.DropTable(
                name: "DefenseTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Defenses_CreatureId",
                table: "Defenses");

            migrationBuilder.DropColumn(
                name: "PowerTemplateId",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "CreatureId",
                table: "Defenses");

            migrationBuilder.DropColumn(
                name: "DefenseTemplateId",
                table: "Defenses");
        }
    }
}
