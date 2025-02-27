using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class upgradedItemConfigurationToProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RangedMediumWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "RangedMediumWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedMediumWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "RangedMediumWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedLightWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "RangedLightWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedLightWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "RangedLightWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedHeavyWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "RangedHeavyWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedHeavyWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "RangedHeavyWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeMediumWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "MeleeMediumWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeMediumWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "MeleeMediumWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeLightWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "MeleeLightWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeLightWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "MeleeLightWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeHeavyWeaponHitPropertyId",
                table: "ItemConfigurations",
                newName: "MeleeHeavyWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeHeavyWeaponDamagePropertyId",
                table: "ItemConfigurations",
                newName: "MeleeHeavyWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetSecondLifeId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetSecondLife_PropertyId");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetFirstLifeId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetFirstLife_PropertyId");

            migrationBuilder.RenameColumn(
                name: "ArmorPropertyId",
                table: "ItemConfigurations",
                newName: "ArmorProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "Property_IdProperty",
                table: "Bonus",
                newName: "Property_PropertyId");

            migrationBuilder.AddColumn<int>(
                name: "ArmorProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BasicAttackTargetFirstLife_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BasicAttackTargetSecondLife_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeleeHeavyWeaponDamageProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeleeHeavyWeaponHitProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeleeLightWeaponDamageProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeleeLightWeaponHitProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeleeMediumWeaponDamageProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeleeMediumWeaponHitProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangedHeavyWeaponDamageProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangedHeavyWeaponHitProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangedLightWeaponDamageProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangedLightWeaponHitProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangedMediumWeaponDamageProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangedMediumWeaponHitProperty_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArmorProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "BasicAttackTargetFirstLife_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "BasicAttackTargetSecondLife_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "MeleeHeavyWeaponDamageProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "MeleeHeavyWeaponHitProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "MeleeLightWeaponDamageProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "MeleeLightWeaponHitProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "MeleeMediumWeaponDamageProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "MeleeMediumWeaponHitProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "RangedHeavyWeaponDamageProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "RangedHeavyWeaponHitProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "RangedLightWeaponDamageProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "RangedLightWeaponHitProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "RangedMediumWeaponDamageProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "RangedMediumWeaponHitProperty_Type",
                table: "ItemConfigurations");

            migrationBuilder.RenameColumn(
                name: "RangedMediumWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedMediumWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedMediumWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedMediumWeaponDamagePropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedLightWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedLightWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedLightWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedLightWeaponDamagePropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedHeavyWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedHeavyWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedHeavyWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedHeavyWeaponDamagePropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeMediumWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeMediumWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeMediumWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeMediumWeaponDamagePropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeLightWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeLightWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeLightWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeLightWeaponDamagePropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeHeavyWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeHeavyWeaponHitPropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeHeavyWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeHeavyWeaponDamagePropertyId");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetSecondLife_PropertyId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetSecondLifeId");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetFirstLife_PropertyId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetFirstLifeId");

            migrationBuilder.RenameColumn(
                name: "ArmorProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "ArmorPropertyId");

            migrationBuilder.RenameColumn(
                name: "Property_PropertyId",
                table: "Bonus",
                newName: "Property_IdProperty");
        }
    }
}
