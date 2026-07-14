using FluentAssertions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Templates;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Itens;

public class EquipmentGripTypeTests
{
    [Theory(DisplayName = "Equipar main hand usa grip explicito do template quando existe")]
    [InlineData(WeaponCategory.Medium, GripType.TwoHandedMediumWeapon)]
    [InlineData(WeaponCategory.Heavy, GripType.OneHandedHeavyWeapon)]
    public void EquipMainHandUsesTemplateGripTypeWhenAvailable(
        WeaponCategory category,
        GripType expectedGripType)
    {
        var equipment = new Equipment();
        var weapon = CreateWeapon(category, expectedGripType);

        equipment.Equip(weapon, EquipableSlot.MainHand);

        equipment.GripType.Should().Be(expectedGripType);
    }

    [Theory(DisplayName = "Equipar offhand recalcula grip combinado")]
    [InlineData(WeaponCategory.Light, GripType.OneLightWeapon, WeaponCategory.Light, GripType.OneLightWeapon, GripType.TwoWeaponsLight)]
    [InlineData(WeaponCategory.Light, GripType.OneLightWeapon, WeaponCategory.Medium, GripType.OneMediumWeapon, GripType.TwoWeaponsMedium)]
    [InlineData(WeaponCategory.Medium, GripType.OneMediumWeapon, WeaponCategory.Light, GripType.OneLightWeapon, GripType.TwoWeaponsMedium)]
    [InlineData(WeaponCategory.Medium, GripType.OneMediumWeapon, WeaponCategory.Medium, GripType.OneMediumWeapon, GripType.TwoWeaponsMedium)]
    public void EquipOffHandRecomputesCombinedGripType(
        WeaponCategory mainCategory,
        GripType mainGripType,
        WeaponCategory offCategory,
        GripType offGripType,
        GripType expectedGripType)
    {
        var equipment = new Equipment();
        var mainHand = CreateWeapon(mainCategory, mainGripType);
        var offHand = CreateWeapon(offCategory, offGripType);

        equipment.Equip(mainHand, EquipableSlot.MainHand);
        equipment.Equip(offHand, EquipableSlot.OffHand);

        equipment.GripType.Should().Be(expectedGripType);
    }

    [Fact(DisplayName = "Desequipar offhand restaura grip da main hand")]
    public void UnequipOffHandRestoresMainHandGripType()
    {
        var equipment = new Equipment();
        var creature = new Creature { Equipment = equipment };
        equipment.Creature = creature;
        var mainHand = CreateWeapon(WeaponCategory.Light, GripType.OneLightWeapon);
        var offHand = CreateWeapon(WeaponCategory.Light, GripType.OneLightWeapon);

        equipment.Equip(mainHand, EquipableSlot.MainHand);
        equipment.Equip(offHand, EquipableSlot.OffHand);
        equipment.Unequip(EquipableSlot.OffHand);

        equipment.GripType.Should().Be(GripType.OneLightWeapon);
    }

    private static ItemInstance CreateWeapon(WeaponCategory category, GripType gripType)
    {
        return new ItemInstance
        {
            Template = new WeaponTemplate
            {
                Category = category,
                DamageType = WeaponDamageType.Bludgeoning,
                GripType = gripType
            }
        };
    }
}
