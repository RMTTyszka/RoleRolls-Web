using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class addedItemConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ItemInstances");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "ItemInstances");

            migrationBuilder.DropColumn(
                name: "WeaponInstance_Category",
                table: "ItemInstances");

            migrationBuilder.CreateTable(
                name: "ItemConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArmorDefenseId = table.Column<Guid>(type: "uuid", nullable: true),
                    LightWeaponHitAttributeId = table.Column<Guid>(type: "uuid", nullable: true),
                    MediumWeaponHitAttributeId = table.Column<Guid>(type: "uuid", nullable: true),
                    HeavyWeaponHitAttributeId = table.Column<Guid>(type: "uuid", nullable: true),
                    LightWeaponDamageAttributeId = table.Column<Guid>(type: "uuid", nullable: true),
                    MediumWeaponDamageAttributeId = table.Column<Guid>(type: "uuid", nullable: true),
                    HeavyWeaponDamageAttributeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemConfigurations_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemConfigurations_CampaignId",
                table: "ItemConfigurations",
                column: "CampaignId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemConfigurations");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "ItemInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "ItemInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeaponInstance_Category",
                table: "ItemInstances",
                type: "integer",
                nullable: true);
        }
    }
}
