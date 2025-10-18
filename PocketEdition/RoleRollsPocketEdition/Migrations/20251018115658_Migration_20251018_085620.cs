using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20251018_085620 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpellCircles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SpellId = table.Column<Guid>(type: "uuid", nullable: false),
                    Circle = table.Column<int>(type: "integer", nullable: false),
                    InGameDescription = table.Column<byte[]>(type: "bytea", nullable: true),
                    EffectDescription = table.Column<byte[]>(type: "bytea", nullable: true),
                    CastingTime = table.Column<byte[]>(type: "bytea", nullable: true),
                    Duration = table.Column<byte[]>(type: "bytea", nullable: true),
                    AreaOfEffect = table.Column<byte[]>(type: "bytea", nullable: true),
                    Requirements = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellCircles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellCircles_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpellCircles_SpellId",
                table: "SpellCircles",
                column: "SpellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpellCircles");

            migrationBuilder.DropTable(
                name: "Spells");
        }
    }
}
