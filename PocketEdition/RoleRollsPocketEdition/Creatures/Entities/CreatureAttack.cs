using System.Text.Json;
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
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.Creatures.Entities;

public partial class Creature
{
    public AttackResult Attack(Creature target, AttackCommand input, IDiceRoller diceRoller,
        ITestOutputHelper? testOutputHelper = null)
    {
        var weapon = GetWeaponOrDefault(input.WeaponSlot);
        var weaponTemplate = (WeaponTemplate?)weapon.Template;
        var weaponCategory = weaponTemplate?.Category ?? WeaponCategory.Light;
        var gripStats = GripTypeDefinition.Stats[Equipment.GripType];
        var weaponLevelBonus = weapon.LevelBonus;
        var predefinedRolls = new List<int>();

        var hitValue = GetHitValue(input, weaponCategory);
        var defenseValue = GetDefenseValue(target, input.GetDefenseId1);
        var diceCount = Math.Max(0, hitValue.Value + hitValue.Bonus);
        var attackerLevelBonus = Math.Max(Level - 1, 0);
        var hitBonus = hitValue.Value + hitValue.Bonus + gripStats.Hit +
                       GetTotalBonus(BonusApplication.Hit, BonusType.Buff, null) + attackerLevelBonus +
                       weaponLevelBonus;

        var armorCategory = target.Equipment.ArmorCategory;
        var complexity = defenseValue;

        var advantage = Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Hit, BonusType.Advantage, null));
        advantage = Math.Max(0, advantage);

        var luck = input.Luck;
        luck -= target.GetEvasionLuck();
        luck += ResolveWeaponVsArmorLuck(weapon, armorCategory);

        var rollCommand = new RollDiceCommand(
            propertyValue: diceCount,
            advantage: advantage,
            bonus: hitBonus,
            difficulty: gripStats.AttackDifficult,
            complexity: complexity,
            predefinedRolls: predefinedRolls,
            luck: luck);

        var roll = new Roll();
        roll.Process(rollCommand, diceRoller, 20);

        var rolledValues = JsonSerializer.Deserialize<List<int>>(roll.RolledDices) ?? new List<int>();

        var successes = rolledValues
            .Select(total => total - complexity)
            .Where(over => over >= 0)
            .OrderByDescending(over => over)
            .ToList();

        var difficulty = gripStats.AttackDifficult;
        var tier = 1 + Math.Max(Level - 1, 0) / 2;
        var damageBonusPerHit = gripStats.BaseBonusDamage * tier + gripStats.Damage;

        var result = ResolveSuccessfulAttack(target, weapon, difficulty, damageBonusPerHit, successes, armorCategory, input);
        result.Attacker = null;
        result.Target = null;
        result.Weapon = null;
        result.Difficulty = difficulty;
        result.NumberOfRollSuccesses = roll.NumberOfRollSuccesses;
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

    private PropertyValue GetHitValue(AttackCommand input, WeaponCategory category)
    {
        var property = input.ItemConfiguration.GetWeaponHitProperty(category);
        return GetPropertyValue(new PropertyInput(property, input.HitAttribute));
    }

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

    private AttackResult ResolveSuccessfulAttack(
        Creature target,
        ItemInstance weapon,
        int difficulty,
        int damageBonusPerHit,
        List<int> successes,
        ArmorCategory targetArmorCategory,
        AttackCommand input)
    {
        var hits = successes.Count / difficulty;
        var blockProperty = input.ItemConfiguration.BlockProperty;
        var blockPropertyValue = blockProperty != null
            ? target.GetPropertyValue(new PropertyInput(blockProperty, input.BlockProperty))
            : new PropertyValue();
        var block = ArmorDefinition.TotalBlock(targetArmorCategory, target.Level) +
                    blockPropertyValue.Value + blockPropertyValue.Bonus;

        var totalDamage = 0;
        for (var i = 0; i < hits; i++)
        {
            var chunkDamage = successes.Skip(i * difficulty)
                .Take(difficulty)
                .Sum();

            var damage = 1 + Math.Max(chunkDamage + damageBonusPerHit - block, 0);

            totalDamage += damage;

            var result = target.TakeDamage(input.GetVitalityId, damage);
            if (result.ExcessDamage > 0)
                target.TakeDamage(input.GetSecondVitalityId, result.ExcessDamage);
        }

        var success = hits > 0;
        return new AttackResult
        {
            Attacker = this,
            Target = target,
            TotalDamage = totalDamage,
            Weapon = weapon,
            Success = success,
            Block = block,
            DamageBonus = damageBonusPerHit,
            NumberOfRollSuccesses = hits
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

    private List<int> RollAttackDice(
        int diceCount,
        int advantage,
        int luck,
        int hitBonus,
        int complexity,
        IDiceRoller diceRoller)
    {
        var rolls = new List<int>(diceCount + Math.Max(advantage, 0));

        for (var i = 0; i < diceCount; i++)
        {
            rolls.Add(diceRoller.Roll(20));
        }

        for (var i = 0; i < advantage; i++)
        {
            rolls.Add(15);
        }

        ApplyLuck(rolls, luck, hitBonus, complexity, diceRoller);
        return rolls;
    }

    private static void ApplyLuck(
        IList<int> rolls,
        int luck,
        int hitBonus,
        int complexity,
        IDiceRoller diceRoller)
    {
        if (luck == 0 || rolls.Count == 0) return;

        var threshold = complexity - hitBonus;
        var iterations = Math.Abs(luck);

        for (var i = 0; i < iterations; i++)
        {
            if (luck > 0)
            {
                var candidateIndex = -1;
                var lowest = int.MaxValue;
                for (var j = 0; j < rolls.Count; j++)
                {
                    var roll = rolls[j];
                    if (roll >= threshold) continue;
                    if (roll == 1 || roll == 20) continue;
                    if (roll < lowest)
                    {
                        lowest = roll;
                        candidateIndex = j;
                    }
                }

                if (candidateIndex != -1)
                {
                    var reroll = diceRoller.Roll(20);
                    rolls[candidateIndex] = Math.Max(rolls[candidateIndex], reroll);
                }
            }
            else
            {
                var candidateIndex = -1;
                var highest = int.MinValue;
                for (var j = 0; j < rolls.Count; j++)
                {
                    var roll = rolls[j];
                    if (roll < threshold) continue;
                    if (roll == 1 || roll == 20) continue;
                    if (roll > highest)
                    {
                        highest = roll;
                        candidateIndex = j;
                    }
                }

                if (candidateIndex != -1)
                {
                    var reroll = diceRoller.Roll(20);
                    rolls[candidateIndex] = Math.Min(rolls[candidateIndex], reroll);
                }
            }
        }
    }
}
