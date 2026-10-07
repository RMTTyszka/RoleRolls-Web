#nullable disable

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Migrations;

[DbContext(typeof(RoleRollsDbContext))]
[Migration("20261007120000_AddSceneInitiativeAndTemplateProperty")]
public partial class AddSceneInitiativeAndTemplateProperty : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "IniciativePropertyId",
            table: "CampaignTemplates",
            type: "uuid",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "SceneInitiativeEntries",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                SceneId = table.Column<Guid>(type: "uuid", nullable: false),
                CreatureId = table.Column<Guid>(type: "uuid", nullable: false),
                Position = table.Column<int>(type: "integer", nullable: false),
                Score = table.Column<int>(type: "integer", nullable: false),
                PropertyValue = table.Column<int>(type: "integer", nullable: false),
                DiceResults = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false,
                    defaultValue: "[]")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SceneInitiativeEntries", x => x.Id);
                table.ForeignKey(
                    name: "FK_SceneInitiativeEntries_CampaignScenes_SceneId",
                    column: x => x.SceneId,
                    principalTable: "CampaignScenes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_SceneInitiativeEntries_SceneId_CreatureId",
            table: "SceneInitiativeEntries",
            columns: new[] { "SceneId", "CreatureId" },
            unique: true);

    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "SceneInitiativeEntries");

        migrationBuilder.DropColumn(
            name: "IniciativePropertyId",
            table: "CampaignTemplates");
    }
}
