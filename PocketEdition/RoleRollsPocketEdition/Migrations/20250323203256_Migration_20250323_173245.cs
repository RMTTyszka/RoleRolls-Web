using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250323_173245 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EnconterId",
                table: "Creatures",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Encounters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encounters_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_EnconterId",
                table: "Creatures",
                column: "EnconterId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_CampaignId",
                table: "Encounters",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Encounters_EnconterId",
                table: "Creatures",
                column: "EnconterId",
                principalTable: "Encounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Encounters_EnconterId",
                table: "Creatures");

            migrationBuilder.DropTable(
                name: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Creatures_EnconterId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "EnconterId",
                table: "Creatures");
        }
    }
}
