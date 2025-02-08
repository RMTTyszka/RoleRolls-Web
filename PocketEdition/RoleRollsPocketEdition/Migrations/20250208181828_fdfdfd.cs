using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class fdfdfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatureTypeId",
                table: "Bonus",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreatureTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreatureTypes_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_CreatureTypeId",
                table: "Bonus",
                column: "CreatureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureTypes_CampaignTemplateId",
                table: "CreatureTypes",
                column: "CampaignTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_CreatureTypes_CreatureTypeId",
                table: "Bonus",
                column: "CreatureTypeId",
                principalTable: "CreatureTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_CreatureTypes_CreatureTypeId",
                table: "Bonus");

            migrationBuilder.DropTable(
                name: "CreatureTypes");

            migrationBuilder.DropIndex(
                name: "IX_Bonus_CreatureTypeId",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "CreatureTypeId",
                table: "Bonus");
        }
    }
}
