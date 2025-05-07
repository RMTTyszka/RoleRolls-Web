using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250507_122220 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RollBonus",
                table: "Rolls",
                newName: "Property_Type");

            migrationBuilder.RenameColumn(
                name: "PropertyType",
                table: "Rolls",
                newName: "Bonus");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Rolls",
                newName: "Property_Id");

            migrationBuilder.RenameColumn(
                name: "RangedMediumWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedMediumWeaponHitProperty_Id");

            migrationBuilder.RenameColumn(
                name: "RangedMediumWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedMediumWeaponDamageProperty_Id");

            migrationBuilder.RenameColumn(
                name: "RangedLightWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedLightWeaponHitProperty_Id");

            migrationBuilder.RenameColumn(
                name: "RangedLightWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedLightWeaponDamageProperty_Id");

            migrationBuilder.RenameColumn(
                name: "RangedHeavyWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedHeavyWeaponHitProperty_Id");

            migrationBuilder.RenameColumn(
                name: "RangedHeavyWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "RangedHeavyWeaponDamageProperty_Id");

            migrationBuilder.RenameColumn(
                name: "MeleeMediumWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeMediumWeaponHitProperty_Id");

            migrationBuilder.RenameColumn(
                name: "MeleeMediumWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeMediumWeaponDamageProperty_Id");

            migrationBuilder.RenameColumn(
                name: "MeleeLightWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeLightWeaponHitProperty_Id");

            migrationBuilder.RenameColumn(
                name: "MeleeLightWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeLightWeaponDamageProperty_Id");

            migrationBuilder.RenameColumn(
                name: "MeleeHeavyWeaponHitProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeHeavyWeaponHitProperty_Id");

            migrationBuilder.RenameColumn(
                name: "MeleeHeavyWeaponDamageProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "MeleeHeavyWeaponDamageProperty_Id");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetSecondVitality_PropertyId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetSecondVitality_Id");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetFirstVitality_PropertyId",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetFirstVitality_Id");

            migrationBuilder.RenameColumn(
                name: "ArmorProperty_PropertyId",
                table: "ItemConfigurations",
                newName: "ArmorProperty_Id");

            migrationBuilder.RenameColumn(
                name: "Property_PropertyId",
                table: "Bonus",
                newName: "Property_Id");

            migrationBuilder.AddColumn<int>(
                name: "Advantage",
                table: "Rolls",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Advantage",
                table: "Rolls");

            migrationBuilder.RenameColumn(
                name: "Property_Type",
                table: "Rolls",
                newName: "RollBonus");

            migrationBuilder.RenameColumn(
                name: "Property_Id",
                table: "Rolls",
                newName: "PropertyId");

            migrationBuilder.RenameColumn(
                name: "Bonus",
                table: "Rolls",
                newName: "PropertyType");

            migrationBuilder.RenameColumn(
                name: "RangedMediumWeaponHitProperty_Id",
                table: "ItemConfigurations",
                newName: "RangedMediumWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedMediumWeaponDamageProperty_Id",
                table: "ItemConfigurations",
                newName: "RangedMediumWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedLightWeaponHitProperty_Id",
                table: "ItemConfigurations",
                newName: "RangedLightWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedLightWeaponDamageProperty_Id",
                table: "ItemConfigurations",
                newName: "RangedLightWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedHeavyWeaponHitProperty_Id",
                table: "ItemConfigurations",
                newName: "RangedHeavyWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "RangedHeavyWeaponDamageProperty_Id",
                table: "ItemConfigurations",
                newName: "RangedHeavyWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeMediumWeaponHitProperty_Id",
                table: "ItemConfigurations",
                newName: "MeleeMediumWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeMediumWeaponDamageProperty_Id",
                table: "ItemConfigurations",
                newName: "MeleeMediumWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeLightWeaponHitProperty_Id",
                table: "ItemConfigurations",
                newName: "MeleeLightWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeLightWeaponDamageProperty_Id",
                table: "ItemConfigurations",
                newName: "MeleeLightWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeHeavyWeaponHitProperty_Id",
                table: "ItemConfigurations",
                newName: "MeleeHeavyWeaponHitProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "MeleeHeavyWeaponDamageProperty_Id",
                table: "ItemConfigurations",
                newName: "MeleeHeavyWeaponDamageProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetSecondVitality_Id",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetSecondVitality_PropertyId");

            migrationBuilder.RenameColumn(
                name: "BasicAttackTargetFirstVitality_Id",
                table: "ItemConfigurations",
                newName: "BasicAttackTargetFirstVitality_PropertyId");

            migrationBuilder.RenameColumn(
                name: "ArmorProperty_Id",
                table: "ItemConfigurations",
                newName: "ArmorProperty_PropertyId");

            migrationBuilder.RenameColumn(
                name: "Property_Id",
                table: "Bonus",
                newName: "Property_PropertyId");
        }
    }
}
