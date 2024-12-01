using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class addedTargetLife : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediumWeaponHitAttributeId",
                table: "ItemConfigurations",
                newName: "MediumWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "MediumWeaponDamageAttributeId",
                table: "ItemConfigurations",
                newName: "MediumWeaponDamagePropertyId");

            migrationBuilder.RenameColumn(
                name: "LightWeaponHitAttributeId",
                table: "ItemConfigurations",
                newName: "LightWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "LightWeaponDamageAttributeId",
                table: "ItemConfigurations",
                newName: "LightWeaponDamagePropertyId");

            migrationBuilder.RenameColumn(
                name: "HeavyWeaponHitAttributeId",
                table: "ItemConfigurations",
                newName: "HeavyWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "HeavyWeaponDamageAttributeId",
                table: "ItemConfigurations",
                newName: "HeavyWeaponDamagePropertyId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureId",
                table: "MinorSkills",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BasicAttackTargetLifeId",
                table: "ItemConfigurations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinorSkills_CreatureId",
                table: "MinorSkills",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinorSkills_Creatures_CreatureId",
                table: "MinorSkills",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorSkills_Creatures_CreatureId",
                table: "MinorSkills");

            migrationBuilder.DropIndex(
                name: "IX_MinorSkills_CreatureId",
                table: "MinorSkills");

            migrationBuilder.DropColumn(
                name: "CreatureId",
                table: "MinorSkills");

            migrationBuilder.DropColumn(
                name: "BasicAttackTargetLifeId",
                table: "ItemConfigurations");

            migrationBuilder.RenameColumn(
                name: "MediumWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "MediumWeaponHitAttributeId");

            migrationBuilder.RenameColumn(
                name: "MediumWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "MediumWeaponDamageAttributeId");

            migrationBuilder.RenameColumn(
                name: "LightWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "LightWeaponHitAttributeId");

            migrationBuilder.RenameColumn(
                name: "LightWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "LightWeaponDamageAttributeId");

            migrationBuilder.RenameColumn(
                name: "HeavyWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "HeavyWeaponHitAttributeId");

            migrationBuilder.RenameColumn(
                name: "HeavyWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "HeavyWeaponDamageAttributeId");
        }
    }
}
