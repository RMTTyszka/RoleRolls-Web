using System.Text.Json;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Services;

namespace RoleRollsPocketEdition.Creatures.Entities;

public partial class Creature
{
    public EvadeResult Evade(Creature attacker, EvadeCommand input, IDiceRoller diceRoller)
    {
        var weapon = attacker.GetWeaponOrDefault(input.WeaponSlot);
        var weaponCategory = ((WeaponTemplate?)weapon.Template)?.Category ?? WeaponCategory.Light;
        var gripStats = GripTypeDefinition.Stats[attacker.Equipment.GripType];
        var hitValue = attacker.GetHitValue(input.ItemConfiguration, weaponCategory);
        var baseDice = Math.Max(0, hitValue.Total);
        var offensiveBonus = hitValue.Total + gripStats.Hit +
                             attacker.GetTotalBonus(BonusApplication.Hit, BonusType.Buff, null) +
                             attacker.GetLevelDifferenceBonusAgainst(this) + weapon.LevelBonus;
        var difficulty = 10 + offensiveBonus;

        var evadeProperty = input.ItemConfiguration.EvadeProperty ??
                            throw new InvalidOperationException("Evade property is not configured for this campaign.");
        var evadeValue = GetPropertyValue(new PropertyInput(evadeProperty));
        var chestArmor = Equipment.Chest;
        var armorCategory = Equipment.ArmorCategory;
        var armorEvasionBonus = chestArmor?.GetDefenseBonus1() ?? ArmorDefinition.DefenseBonus1(armorCategory);
        var armorLevelBonus = chestArmor?.LevelBonus ?? 0;
        var evadeBonus = evadeValue.Total + armorEvasionBonus + armorLevelBonus +
                          GetTotalBonus(BonusApplication.Evasion, BonusType.Buff, null);
        var advantage = Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Evasion, BonusType.Advantage, null));
        advantage = Math.Max(0, advantage);
        var luck = input.Luck + attacker.ResolveWeaponVsArmorLuck(weapon, armorCategory);

        var rawRolls = RollDefensiveDice(baseDice + advantage, luck, diceRoller);
        var allResults = rawRolls.Select(roll => roll + evadeBonus).ToList();
        var keptResults = allResults
            .OrderByDescending(result => result)
            .Take(baseDice)
            .ToList();
        var excesses = keptResults
            .Where(result => result < difficulty)
            .Select(result => difficulty - result)
            .OrderByDescending(excess => excess)
            .ToList();
        var hits = excesses.Count / gripStats.AttackDifficult;
        var blockProperty = input.ItemConfiguration.BlockProperty;
        var blockPropertyValue = blockProperty is null
            ? new PropertyValue()
            : GetPropertyValue(new PropertyInput(blockProperty));
        var block = ArmorDefinition.TotalBlock(armorCategory, Level) + blockPropertyValue.Total;
        var damageBonus = gripStats.BaseBonusDamage * attacker.Level + gripStats.Damage;
        var totalDamage = 0;
        var vitalityDamage = new List<CreatureTakeDamageResult>();

        for (var hitIndex = 0; hitIndex < hits; hitIndex++)
        {
            var chunkDamage = excesses
                .Skip(hitIndex * gripStats.AttackDifficult)
                .Take(gripStats.AttackDifficult)
                .Sum();
            var damage = Math.Max(chunkDamage + damageBonus - block, 1);

            totalDamage += damage;
            vitalityDamage.AddRange(ApplyBasicAttackDamage(this, damage, input.VitalityId));
        }

        return new EvadeResult
        {
            Attacker = attacker,
            Defender = this,
            Weapon = weapon,
            WeaponSlot = input.WeaponSlot,
            BaseDice = baseDice,
            Difficulty = difficulty,
            EvadeBonus = evadeBonus,
            Luck = luck,
            Advantage = advantage,
            RolledDices = JsonSerializer.Serialize(allResults),
            KeptResults = keptResults,
            Excesses = excesses,
            NumberOfHits = hits,
            Block = block,
            DamageBonus = damageBonus,
            TotalDamage = totalDamage,
            VitalityDamage = vitalityDamage,
            Success = hits == 0
        };
    }

    public int GetBasicBlock(PropertyValue blockProperty)
    {
        var armorCategory = Equipment.Chest?.ArmorTemplate?.Category ?? ArmorCategory.None;
        var total = ArmorDefinition.TotalBlock(armorCategory, Level) + blockProperty.Total;
        return Math.Max(total, 0);
    }

    private List<int> RollDefensiveDice(int numberOfDice, int luck, IDiceRoller diceRoller)
    {
        var rolls = Enumerable.Range(0, Math.Max(0, numberOfDice))
            .Select(_ => diceRoller.Roll(20))
            .ToList();

        if (luck == 0)
        {
            return rolls;
        }

        var indicesToReroll = rolls
            .Select((value, index) => new { value, index })
            .OrderBy(pair => luck > 0 ? pair.value : -pair.value)
            .Take(Math.Abs(luck))
            .Select(pair => pair.index)
            .ToList();

        foreach (var index in indicesToReroll)
        {
            var reroll = diceRoller.Roll(20);
            rolls[index] = luck > 0 ? Math.Max(rolls[index], reroll) : Math.Min(rolls[index], reroll);
        }

        return rolls;
    }

    private int GetEvasionLuck() => ArmorDefinition.BaseLuck(Equipment.ArmorCategory);
}
