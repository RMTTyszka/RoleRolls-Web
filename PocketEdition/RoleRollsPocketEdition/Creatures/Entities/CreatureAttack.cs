using System.Diagnostics;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Rolls.Services;

namespace RoleRollsPocketEdition.Creatures.Entities;

public partial class Creature
{
    public AttackResult Attack(Creature target, AttackCommand input, IDiceRoller diceRoller)
    {
        var weapon = GetWeaponOrDefault(input.WeaponSlot);
        var weaponTemplate = (WeaponTemplate?)weapon.Template;
        var weaponCategory = weaponTemplate?.Category ?? WeaponCategory.Light;
        var gripStats = GripTypeDefinition.Stats[Equipment.GripType];

        var hitValue = GetHitValue(input, weaponCategory, gripStats);
        var defenseValue = GetDefenseValue(target, input.GetDefenseId);
        var unluck = target.GetEvasionLuck();
        input.Luck -= unluck;
        var armorCategory = target.Equipment.ArmorCategory;
        var dicesByLevel = GetRollDices();
        var roll = RollToHit(weapon, dicesByLevel, hitValue, defenseValue, gripStats, input, diceRoller, armorCategory);

        return roll.Success
            ? ResolveSuccessfulAttack(target, weapon, roll.NumberOfRollSuccesses, input, gripStats, diceRoller)
            : CreateFailedResult(target, weapon);
    }

    private ItemInstance GetWeaponOrDefault(EquipableSlot slot)
    {
        var weapon = Equipment.GetItem(slot);
        return weapon ?? new ItemInstance
        {
            Template = new WeaponTemplate
            {
                Category = WeaponCategory.Medium,
                DamageType = WeaponDamageType.Bludgeoning
            }
        };
    }

    private PropertyValue GetHitValue(AttackCommand input, WeaponCategory category, GripTypeStats gripStats)
    {
        var property = input.ItemConfiguration.GetWeaponHitProperty(category);
        var value = GetPropertyValue(new PropertyInput(property, input.HitAttribute));
        value.Bonus += gripStats.Hit + GetTotalBonus(BonusApplication.Hit, BonusType.Buff, null);
        return value;
    }

    private int GetDefenseValue(Creature target, Guid defenseId)
    {
        var defenseInput = new PropertyInput(new Property(defenseId));
        var armorCategory = (target.Equipment.Chest?.Template as ArmorTemplate)?.Category ?? ArmorCategory.None;
        var defenseValue = target.GetPropertyValue(defenseInput);
        return 10 + defenseValue.Value + defenseValue.Bonus + ArmorDefinition.EvasionBonus(armorCategory);
    }

    private Roll RollToHit(ItemInstance weapon, int dices, PropertyValue hitValue, int defenseValue, GripTypeStats gripType, AttackCommand input, IDiceRoller diceRoller, ArmorCategory armorCategory)
    {
        var advantage = Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Hit, BonusType.Advantage, null));
        advantage += ResolveWeaponVsArmorAdvantage(weapon, armorCategory);
        var weaponBonus = weapon.GetBonus;
        var weaponInnateHitBonus = gripType.Hit;
        var command = new RollDiceCommand(
            hitValue.Value,
            advantage,
            hitValue.Bonus + hitValue.Value + weaponBonus + weaponInnateHitBonus,
            gripType.AttackDifficult,
            defenseValue,
            [],
            input.Luck
        );
        var roll = new Roll();
        roll.Process(command, diceRoller, 20);
        return roll;
    }

    private int ResolveWeaponVsArmorAdvantage(ItemInstance weapon, ArmorCategory armorCategory)
    {
        switch (weapon.WeaponTemplate.Category)
        {
            case WeaponCategory.None:
                break;
            case WeaponCategory.Light:
                if (armorCategory is ArmorCategory.Light)
                {
                    return 2;
                }
                if (armorCategory is ArmorCategory.Heavy)
                {
                    return -2;
                }
                break;
            case WeaponCategory.Heavy:
                if (armorCategory is ArmorCategory.Heavy)
                {
                    return 2;
                }
                if (armorCategory is ArmorCategory.Light)
                {
                    return -1;
                }
                break;
            case WeaponCategory.Medium:
            case WeaponCategory.LightShield:
            case WeaponCategory.MediumShield:
            case WeaponCategory.HeavyShield:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return 0;
    }

    private AttackResult ResolveSuccessfulAttack(Creature target, ItemInstance weapon, int times, AttackCommand input,
        GripTypeStats gripStats, IDiceRoller diceRoller)
    {
        var damages = new List<DamageRollResult>();
        Debug.Assert(weapon.WeaponTemplate != null, "weapon.WeaponTemplate != null");
        var damageProperty = GetPropertyValue(new PropertyInput(
            input.ItemConfiguration.GetWeaponDamageProperty(weapon.WeaponTemplate.Category),
            input.DamageAttribute
        ));
        var property = input.ItemConfiguration.BlockProperty;
        var propertyValue = GetPropertyValue(new PropertyInput(property, input.BlockProperty));
        for (int i = 0; i < times; i++)
        {
            var damage = RollDamage(weapon, damageProperty, gripStats, diceRoller);
            damage.ReducedDamage = Math.Max(1, damage.ReducedDamage - target.GetBasicBlock(propertyValue));
            damages.Add(damage);

            var result = target.TakeDamage(input.GetVitalityId, damage.TotalDamage);
            if (result.ExcessDamage > 0)
                target.TakeDamage(input.GetSecondVitalityId, result.ExcessDamage);
        }

        return new AttackResult
        {
            Attacker = this,
            Target = target,
            TotalDamage = damages.Sum(d => d.ReducedDamage),
            Weapon = weapon,
            Success = true
        };
    }

    private AttackResult CreateFailedResult(Creature target, ItemInstance weapon)
    {
        return new AttackResult
        {
            Attacker = this,
            Target = target,
            Weapon = weapon,
            Success = false
        };
    }
}