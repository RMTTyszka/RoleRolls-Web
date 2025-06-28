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
        var gripStats = GripTypeExtensions.Stats[Equipment.GripType];

        var hitValue = GetHitValue(input, weaponCategory, gripStats);
        var defenseValue = GetDefenseValue(target, input.GetDefenseId);
        var roll = RollToHit(hitValue, defenseValue, weaponCategory, input);

        return roll.Success
            ? ResolveSuccessfulAttack(target, weapon, roll.NumberOfRollSuccesses, input, gripStats)
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
        return 10 + defenseValue.Bonus + ArmorDefinition.DefenseBonus(armorCategory);
    }

    private Roll RollToHit(PropertyValue hitValue, int defenseValue, WeaponCategory category, AttackCommand input)
    {
        var advantage = Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Hit, BonusType.Advantage, null));
        var command = new RollDiceCommand(
            hitValue.Value,
            advantage,
            hitValue.Bonus,
            WeaponDefinition.HitDifficulty(category),
            defenseValue,
            [],
            input.Luck
        );
        var roll = new Roll();
        roll.Process(command);
        return roll;
    }

    private AttackResult ResolveSuccessfulAttack(Creature target, ItemInstance weapon, int times, AttackCommand input,
        GripTypeStats gripStats)
    {
        var damages = new List<DamageRollResult>();

        for (int i = 0; i < times; i++)
        {
            var damage = RollDamage(weapon, input, gripStats);
            damage.ReducedDamage = Math.Max(1, damage.ReducedDamage - target.GetBasicBlock());
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

    private DamageRollResult RollDamage(ItemInstance weapon, AttackCommand input, GripTypeStats gripStats)
    {
        var result = new DamageRollResult();
        var rng = new Random();
        var max = gripStats.Damage;

        var damage = rng.Next(1, max + 1);
        damage += gripStats.BaseBonusDamage;
        damage += (weapon.Level / 2) * gripStats.MagicBonusModifier;

        var damageProperty = GetPropertyValue(new PropertyInput(
            input.ItemConfiguration.GetWeaponDamageProperty(((WeaponTemplate?)weapon.Template)?.Category ??
                                                            WeaponCategory.Light),
            input.DamageAttribute
        ));

        damage += damageProperty.Bonus * gripStats.AttributeModifier;

        result.DiceValue = damage;
        result.BonusModifier = gripStats.AttributeModifier;
        result.FlatBonus = gripStats.BaseBonusDamage;
        result.TotalDamage = damage;
        result.ReducedDamage = damage;

        return result;
    }
}