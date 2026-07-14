using System.Text.Json;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Rolls.Services;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.Creatures.Entities;

public partial class Creature
{
    public BasicAttackResult BasicAttack(Creature target, BasicAttackCommand input, IDiceRoller diceRoller,
        ITestOutputHelper? testOutputHelper = null)
    {
        var weapon = GetWeaponOrDefault(input.WeaponSlot);
        var weaponTemplate = (WeaponTemplate?)weapon.Template;
        var weaponCategory = weaponTemplate?.Category ?? WeaponCategory.Light;
        var gripStats = GripTypeDefinition.Stats[Equipment.GripType];
        var weaponLevelBonus = weapon.LevelBonus;

        var hitValue = GetHitValue(input, weaponCategory);
        var defenseId = input.ResolvedDefenseId;
        var defenseValue = GetDefenseValue(target, defenseId);
        var diceCount = Math.Max(0, hitValue.Total);
        var levelDifferenceBonus = GetLevelDifferenceBonusAgainst(target);
        var hitBonus = hitValue.Total + gripStats.Hit +
                       GetTotalBonus(BonusApplication.Hit, BonusType.Buff, null) + levelDifferenceBonus +
                       weaponLevelBonus;

        var armorCategory = target.Equipment.ArmorCategory;
        var advantage = Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Hit, BonusType.Advantage, null));
        advantage = Math.Max(0, advantage);
        var luck = input.Luck;
        luck -= target.GetEvasionLuck();
        luck += ResolveWeaponVsArmorLuck(weapon, armorCategory);
        var difficulty = gripStats.AttackDifficult;
        var complexity = defenseValue;

        var rollCommand = new RollDiceCommand(
            propertyValue: diceCount,
            advantage: advantage,
            bonus: hitBonus,
            difficulty: difficulty,
            complexity: complexity,
            predefinedRolls: [],
            luck: luck);

        var roll = new Roll();
        roll.Process(rollCommand, diceRoller, 20);

        var rolledValues = JsonSerializer.Deserialize<List<int>>(roll.RolledDices) ?? new List<int>();

        var successes = rolledValues
            .Select(total => total - complexity)
            .Where(over => over >= 0)
            .OrderByDescending(over => over)
            .ToList();

        var tier = Level;
        var damageBonusPerHit = gripStats.BaseBonusDamage * tier + gripStats.Damage;

        var result = ResolveSuccessfulAttack(
            target,
            weapon,
            input,
            successes,
            armorCategory,
            damageBonusPerHit,
            difficulty);

        result.WeaponSlot = input.WeaponSlot;
        result.DefenseId = defenseId;
        result.Complexity = complexity;
        result.Difficulty = difficulty;
        result.NumberOfSuccesses = roll.NumberOfSuccesses;
        result.NumberOfRollSuccesses = roll.NumberOfRollSuccesses;
        result.Bonus = hitBonus;
        result.Luck = luck;
        result.Advantage = advantage;
        result.RolledDices = roll.RolledDices;
        return result;
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

    private PropertyValue GetHitValue(BasicAttackCommand input, WeaponCategory category)
    {
        var property = input.ItemConfiguration.GetWeaponHitProperty(category);
        return GetPropertyValue(new PropertyInput(property));
    }

    private int GetLevelDifferenceBonusAgainst(Creature target) => Level - target.Level;

    private int GetDefenseValue(Creature target, Guid defenseId) => target.DefenseValue(defenseId);

    private int ResolveWeaponVsArmorLuck(ItemInstance weapon, ArmorCategory armorCategory)
    {
        switch (weapon.WeaponTemplate.Category)
        {
            case WeaponCategory.None:
                break;
            case WeaponCategory.Light:
                if (armorCategory is ArmorCategory.Light)
                {
                    return 1;
                }

                if (armorCategory is ArmorCategory.Heavy)
                {
                    return -1;
                }

                break;
            case WeaponCategory.Heavy:
                if (armorCategory is ArmorCategory.Heavy)
                {
                    return 1;
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

    private BasicAttackResult ResolveSuccessfulAttack(
        Creature target,
        ItemInstance weapon,
        BasicAttackCommand input,
        List<int> successes,
        ArmorCategory targetArmorCategory,
        int damageBonusPerHit,
        int difficulty)
    {
        var hits = successes.Count / difficulty;
        var blockProperty = input.ItemConfiguration.BlockProperty;
        var blockPropertyValue = blockProperty != null
            ? target.GetPropertyValue(new PropertyInput(blockProperty))
            : new PropertyValue();
        var block = ArmorDefinition.TotalBlock(targetArmorCategory, target.Level) +
                    blockPropertyValue.Total;

        var totalDamage = 0;
        for (var i = 0; i < hits; i++)
        {
            var chunkDamage = successes.Skip(i * difficulty)
                .Take(difficulty)
                .Sum();

            var damage = Math.Max(chunkDamage + damageBonusPerHit - block, 1);

            totalDamage += damage;
            ApplyBasicAttackDamage(target, damage, input.VitalityId);
        }

        var success = hits > 0;
        return new BasicAttackResult
        {
            Attacker = this,
            Target = target,
            TotalDamage = totalDamage,
            Weapon = weapon,
            Success = success,
            Block = block,
            DamageBonus = damageBonusPerHit
        };
    }
}
