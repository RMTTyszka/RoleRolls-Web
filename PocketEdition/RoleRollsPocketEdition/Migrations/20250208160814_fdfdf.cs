using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class fdfdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageType_CampaignTemplates_CampaignTemplateId",
                table: "DamageType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DamageType",
                table: "DamageType");

            migrationBuilder.RenameTable(
                name: "DamageType",
                newName: "DamageTypes");

            migrationBuilder.RenameIndex(
                name: "IX_DamageType_CampaignTemplateId",
                table: "DamageTypes",
                newName: "IX_DamageTypes_CampaignTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamageTypes",
                table: "DamageTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageTypes_CampaignTemplates_CampaignTemplateId",
                table: "DamageTypes",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageTypes_CampaignTemplates_CampaignTemplateId",
                table: "DamageTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DamageTypes",
                table: "DamageTypes");

            migrationBuilder.RenameTable(
                name: "DamageTypes",
                newName: "DamageType");

            migrationBuilder.RenameIndex(
                name: "IX_DamageTypes_CampaignTemplateId",
                table: "DamageType",
                newName: "IX_DamageType_CampaignTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamageType",
                table: "DamageType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageType_CampaignTemplates_CampaignTemplateId",
                table: "DamageType",
                column: "CampaignTemplateId",
                principalTable: "CampaignTemplates",
                principalColumn: "Id");
        }
    }
}
