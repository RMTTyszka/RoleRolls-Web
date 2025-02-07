using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class AddedDamageType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatureType",
                table: "SceneCreatures",
                newName: "CreatureCategory");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Creatures",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "RoleTitle",
                table: "CampaignTemplates",
                newName: "CreatureTypeTitle");

            migrationBuilder.CreateTable(
                name: "DamageType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamageType_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamageType_CampaignTemplateId",
                table: "DamageType",
                column: "CampaignTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamageType");

            migrationBuilder.RenameColumn(
                name: "CreatureCategory",
                table: "SceneCreatures",
                newName: "CreatureType");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Creatures",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "CreatureTypeTitle",
                table: "CampaignTemplates",
                newName: "RoleTitle");
        }
    }
}
