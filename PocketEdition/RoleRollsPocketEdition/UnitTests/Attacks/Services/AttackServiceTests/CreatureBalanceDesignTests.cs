using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

// Replica os cenários de RpgBalanceDesignTests, mas exercitando o pipeline real de Creature.Attack.
public class CreatureBalanceDesignTests
{
    private const int SimulationSamples = 1500;
    private const int SearchSamples = 400;
    private const int Seed = 4242;
    private const int MaxLevel = 20;

    private static readonly WeaponCategory[] WeaponsUnderTest =
    {
        WeaponCategory.Light,
        WeaponCategory.Medium,
        WeaponCategory.Heavy
    };

    private static readonly ArmorCategory[] ArmorsUnderTest =
    {
        ArmorCategory.Light,
        ArmorCategory.Medium,
        ArmorCategory.Heavy
    };

    private readonly ITestOutputHelper _testOutputHelper;

    public CreatureBalanceDesignTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact(DisplayName = "Luck +1 aumenta dano medio por arma e nivel (Creature.Attack)")]
    public void LuckBoostsDamagePerWeaponPerLevel()
    {
        var perWeaponTotals = WeaponsUnderTest.ToDictionary(w => w, _ => 0.0);

        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var baseline = RunMatrix(SearchSamples, level);
            var withLuck = RunMatrix(SearchSamples, level, luckOverride: 1);

            foreach (var weapon in WeaponsUnderTest)
            {
                var baseAvg = ArmorsUnderTest.Average(armor => baseline[(weapon, armor)]);
                var luckAvg = ArmorsUnderTest.Average(armor => withLuck[(weapon, armor)]);

                perWeaponTotals[weapon] += luckAvg - baseAvg;
                _testOutputHelper.WriteLine(
                    $"Level {level:00} | Weapon {weapon,-6}: base {baseAvg:F2} -> luck {luckAvg:F2} (? {luckAvg - baseAvg:F2})");
            }
        }

        foreach (var weapon in WeaponsUnderTest)
        {
            var avgDelta = perWeaponTotals[weapon] / MaxLevel;
            avgDelta.Should().BeGreaterThan(0, $"sorte +1 deve aumentar dano em media para {weapon}");
            _testOutputHelper.WriteLine($"Luck +1 average delta for {weapon,-6}: {avgDelta:F2} dmg");
        }
    }

    [Fact(DisplayName = "Um dado extra aumenta dano medio (Creature.Attack)")]
    public void ExtraDiceBoostsDamagePerWeaponPerLevel()
    {
        var perWeaponTotals = WeaponsUnderTest.ToDictionary(w => w, _ => 0.0);

        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var baseline = RunMatrix(SearchSamples, level);
            var withExtraDie = RunMatrix(SearchSamples, level, extraAttackDice: 1);

            foreach (var weapon in WeaponsUnderTest)
            {
                var baseAvg = ArmorsUnderTest.Average(armor => baseline[(weapon, armor)]);
                var boostedAvg = ArmorsUnderTest.Average(armor => withExtraDie[(weapon, armor)]);

                perWeaponTotals[weapon] += boostedAvg - baseAvg;
                _testOutputHelper.WriteLine(
                    $"Level {level:00} | Weapon {weapon,-6}: base {baseAvg:F2} -> +die {boostedAvg:F2} (? {boostedAvg - baseAvg:F2})");
            }
        }

        foreach (var weapon in WeaponsUnderTest)
        {
            var avgDelta = perWeaponTotals[weapon] / MaxLevel;
            avgDelta.Should().BeGreaterThan(0, $"+1 dado deve aumentar dano em media para {weapon}");
            _testOutputHelper.WriteLine($"+1 die average delta for {weapon,-6}: {avgDelta:F2} dmg");
        }
    }

    [Fact(DisplayName = "Advantage (+1 dado fixo 15) aumenta dano medio (Creature.Attack)")]
    public void AdvantageBoostsDamagePerWeaponPerLevel()
    {
        var perWeaponTotals = WeaponsUnderTest.ToDictionary(w => w, _ => 0.0);

        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var baseline = RunMatrix(SearchSamples, level);
            var withAdvantage = RunMatrix(SearchSamples, level, advantage: 1);

            foreach (var weapon in WeaponsUnderTest)
            {
                var baseAvg = ArmorsUnderTest.Average(armor => baseline[(weapon, armor)]);
                var advAvg = ArmorsUnderTest.Average(armor => withAdvantage[(weapon, armor)]);

                perWeaponTotals[weapon] += advAvg - baseAvg;
                _testOutputHelper.WriteLine(
                    $"Level {level:00} | Weapon {weapon,-6}: base {baseAvg:F2} -> adv15 {advAvg:F2} (? {advAvg - baseAvg:F2})");
            }
        }

        foreach (var weapon in WeaponsUnderTest)
        {
            var avgDelta = perWeaponTotals[weapon] / MaxLevel;
            avgDelta.Should().BeGreaterThan(0, $"vantagem deve aumentar dano em media para {weapon}");
            _testOutputHelper.WriteLine($"Advantage (+15) average delta for {weapon,-6}: {avgDelta:F2} dmg");
        }
    }

    [Fact(DisplayName = "Peso da arma funciona melhor contra armadura equivalente (Creature.Attack)")]
    public void MatchingWeightsAreOptimal()
    {
        var matrix = RunMatrix(SimulationSamples, level: 5);
        foreach (var armor in ArmorsUnderTest)
        {
            var bestWeapon = matrix
                .Where(entry => entry.Key.Armor == armor)
                .OrderByDescending(entry => entry.Value)
                .First()
                .Key.Weapon;

            var expected = armor switch
            {
                ArmorCategory.Light => WeaponCategory.Light,
                ArmorCategory.Medium => WeaponCategory.Medium,
                ArmorCategory.Heavy => WeaponCategory.Heavy,
                _ => throw new ArgumentOutOfRangeException(nameof(armor), armor, null)
            };

            bestWeapon.Should().Be(expected);
        }
    }

    [Fact(DisplayName = "Envelope de balance e counters permanecem validos (Creature.Attack)")]
    public void BalanceEnvelopeAndCounters()
    {
        var aggregated = WeaponsUnderTest
            .SelectMany(w => ArmorsUnderTest.Select(a => (w, a)))
            .ToDictionary(pair => pair, _ => 0.0);

        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var results = RunMatrix(SearchSamples, level);
            foreach (var entry in results)
            {
                aggregated[entry.Key] += entry.Value;
            }
        }

        var min = aggregated.Min(entry => entry.Value);
        var max = aggregated.Max(entry => entry.Value);
        if (max > 0)
        {
            (min / max).Should().BeGreaterThan(0.1);
        }

        DominanceHolds(aggregated).Should().BeTrue();
    }

    [Fact(DisplayName = "Escalonamento por nivel mantem a dominancia (Creature.Attack)")]
    public void LevelScalingKeepsBalance()
    {
        var aggregated = WeaponsUnderTest
            .SelectMany(w => ArmorsUnderTest.Select(a => (w, a)))
            .ToDictionary(pair => pair, _ => 0.0);
        var perLevelLog = new List<string>();
        var weaponTotalsAllLevels = WeaponsUnderTest.ToDictionary(w => w, _ => 0.0);
        var armorTotalsAllLevels = ArmorsUnderTest.ToDictionary(a => a, _ => 0.0);

        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var results = RunMatrix(SearchSamples, level);

            foreach (var entry in results)
            {
                aggregated[entry.Key] += entry.Value;
            }

            perLevelLog.Add($"Level {level:00}");

            foreach (var weapon in WeaponsUnderTest)
            {
                foreach (var armor in ArmorsUnderTest)
                {
                    var dmg = results[(weapon, armor)];
                    perLevelLog.Add($"  Weapon {weapon,-6} vs Armor {armor,-6}: {dmg:F2}");
                }
            }

            foreach (var weapon in WeaponsUnderTest)
            {
                var weaponSum = ArmorsUnderTest.Sum(armor => results[(weapon, armor)]);
                perLevelLog.Add($"  Weapon {weapon,-6} total (lvl): {weaponSum:F2}");
                weaponTotalsAllLevels[weapon] += weaponSum;
            }

            foreach (var armor in ArmorsUnderTest)
            {
                var armorSum = WeaponsUnderTest.Sum(weapon => results[(weapon, armor)]);
                perLevelLog.Add($"  Armor  {armor,-6} total (lvl): {armorSum:F2}");
                armorTotalsAllLevels[armor] += armorSum;
            }
        }

        DominanceHolds(aggregated).Should().BeTrue("dominancia deve se manter na media dos niveis");

        var aggMin = aggregated.Min(e => e.Value);
        var aggMax = aggregated.Max(e => e.Value);
        if (aggMax > 0)
        {
            (aggMin / aggMax).Should().BeGreaterThan(0.06, "viabilidade media por nivel");
        }

        perLevelLog.Add("=== Totais agregados (todos os niveis) ===");
        foreach (var weapon in WeaponsUnderTest)
        {
            perLevelLog.Add($"Weapon {weapon,-6} total (all levels): {weaponTotalsAllLevels[weapon]:F2}");
        }

        foreach (var armor in ArmorsUnderTest)
        {
            perLevelLog.Add($"Armor  {armor,-6} total (all levels): {armorTotalsAllLevels[armor]:F2}");
        }

        foreach (var line in perLevelLog)
        {
            _testOutputHelper.WriteLine(line);
        }
    }

    [Fact(DisplayName = "HP medio necessario para 4 turnos (Creature.Attack)")]
    public void HitPointsNeededForFourRounds()
    {
        var report = new List<string>();

        var anyPositive = false;

        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var averageDamage = SimulatedAverageDamage(level, SearchSamples);
            var hpNeeded = Math.Ceiling(averageDamage * 4);
            if (hpNeeded > 0)
            {
                anyPositive = true;
            }
            report.Add($"Level {level:00}: needs ~{hpNeeded} HP (avg dmg {averageDamage:F2} across all weapon/armor pairs)");
        }

        anyPositive.Should().BeTrue("algum nivel deve gerar dano positivo para justificar o HP medio");

        foreach (var line in report)
        {
            _testOutputHelper.WriteLine(line);
        }
    }

    [Fact(DisplayName = "Estimativa de HP se aproxima da simulacao (Creature.Attack)")]
    public void ApproximateHpMatchesSimulation()
    {
        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var highSampleHp = EstimatedHpForLevel(level, samples: SearchSamples * 2, seedOffset: level * 97);
            if (highSampleHp == 0)
            {
                continue;
            }

            var lowSampleHp = EstimatedHpForLevel(level, samples: SearchSamples / 3, seedOffset: level * 131);

            (lowSampleHp / (double)highSampleHp).Should().BeInRange(0.65, 1.5,
                $"estimativa de HP (poucas amostras) deve manter ~4 turnos no nivel {level}");
        }
    }

    private Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> RunMatrix(
        int samples,
        int level,
        int? luckOverride = null,
        int extraAttackDice = 0,
        int advantage = 0)
    {
        var output = new Dictionary<(WeaponCategory, ArmorCategory), double>();

        foreach (var weapon in WeaponsUnderTest)
        {
            foreach (var armor in ArmorsUnderTest)
            {
                var seed = Seed + level * 31 + (int)weapon * 7 + (int)armor * 13 + extraAttackDice * 3 +
                           (luckOverride ?? 0) * 17 + advantage * 19;
                var roller = new RandomDiceRoller(seed);
                var dmg = SimulateAverageDamage(level, weapon, armor, samples, roller, luckOverride, extraAttackDice,
                    advantage);
                output[(weapon, armor)] = dmg;
            }
        }

        return output;
    }

    private double SimulateAverageDamage(
        int level,
        WeaponCategory weapon,
        ArmorCategory armor,
        int samples,
        IDiceRoller diceRoller,
        int? luckOverride,
        int extraAttackDice,
        int advantage)
    {
        var config = LandOfHeroesTemplate.Template.ItemConfiguration;
        var attacker = BuildAttacker(weapon, level, extraAttackDice);
        var defender = BuildDefender(armor, level);

        var command = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config,
            Luck = luckOverride ?? GetLuckType(weapon, armor),
            Advantage = advantage
        };

        double total = 0;
        for (var i = 0; i < samples; i++)
        {
            var result = attacker.Attack(defender, command, diceRoller);
            total += result.TotalDamage;
            defender.FullRestore();
        }

        return total / samples;
    }

    private static Creature BuildAttacker(WeaponCategory weapon, int level, int extraAttackDice)
    {
        var attacker = new BaseCreature(LandOfHeroesTemplate.Template, $"{weapon} attacker lvl {level}")
            .WithLevel(level)
            .WithWeapon(weapon, EquipableSlot.MainHand, level)
            .Creature;

        var attributeDice = GetAttributeDiceForLevel(level);
        var skillDice = GetSkillDiceForLevel(level) + extraAttackDice;
        SetWeaponDice(attacker, weapon, attributeDice, skillDice);
        attacker.FullRestore();
        return attacker;
    }

    private static Creature BuildDefender(ArmorCategory armor, int level)
    {
        var defender = new BaseCreature(LandOfHeroesTemplate.Template, $"{armor} defender lvl {level}")
            .WithLevel(level)
            .WithArmor(armor, level)
            .Creature;

        SetEvasion(defender, attributePoints: 0, skillPoints: 0);
        SetBlockAttribute(defender, 0);
        defender.FullRestore();
        return defender;
    }

    private static void SetWeaponDice(Creature creature, WeaponCategory weapon, int attributePoints, int skillPoints)
    {
        var minorSkillId = GetWeaponMinorSkill(weapon);
        var attributeTemplateId = LandOfHeroesTemplate.AttributelessMinorSkillsAttributeId[minorSkillId]
            ?? throw new InvalidOperationException("Weapon skill without attribute");

        SetAttribute(creature, attributeTemplateId, attributePoints);
        SetSpecificSkill(creature, LandOfHeroesTemplate.MinorSkillIds[minorSkillId], skillPoints);
    }

    private static void SetEvasion(Creature creature, int attributePoints, int skillPoints)
    {
        var agilityId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility];
        SetAttribute(creature, agilityId, attributePoints);
        SetSpecificSkill(creature, LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Evasion], skillPoints);
    }

    private static void SetBlockAttribute(Creature creature, int points)
    {
        var vigorId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor];
        SetAttribute(creature, vigorId, points);
    }

    private static void SetAttribute(Creature creature, Guid attributeTemplateId, int points)
    {
        var attribute = creature.Attributes.First(at => at.AttributeTemplateId == attributeTemplateId);
        attribute.Points = points;
    }

    private static void SetSpecificSkill(Creature creature, Guid minorSkillId, int points)
    {
        var minorSkill = creature.SpecificSkills.First(ms => ms.SpecificSkillTemplateId == minorSkillId);
        minorSkill.Points = points;
    }

    private static LandOfHeroesMinorSkill GetWeaponMinorSkill(WeaponCategory weapon) => weapon switch
    {
        WeaponCategory.Light => LandOfHeroesMinorSkill.MeleeLightWeapon,
        WeaponCategory.Medium => LandOfHeroesMinorSkill.MeleeMediumWeapon,
        WeaponCategory.Heavy => LandOfHeroesMinorSkill.MeleeHeavyWeapon,
        _ => throw new ArgumentOutOfRangeException(nameof(weapon), weapon, null)
    };

    private static bool DominanceHolds(Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> results)
    {
        return results[(WeaponCategory.Light, ArmorCategory.Light)] >
               results[(WeaponCategory.Heavy, ArmorCategory.Light)]
               && results[(WeaponCategory.Medium, ArmorCategory.Medium)] >
               results[(WeaponCategory.Light, ArmorCategory.Medium)]
               && results[(WeaponCategory.Medium, ArmorCategory.Medium)] >
               results[(WeaponCategory.Heavy, ArmorCategory.Medium)]
               && results[(WeaponCategory.Heavy, ArmorCategory.Heavy)] >=
               results[(WeaponCategory.Medium, ArmorCategory.Heavy)];
    }

    private static int GetLuckType(WeaponCategory weapon, ArmorCategory armor)
    {
        if ((weapon == WeaponCategory.Light && armor == ArmorCategory.Light) ||
            (weapon == WeaponCategory.Heavy && armor == ArmorCategory.Heavy))
            return 1;

        if ((weapon == WeaponCategory.Light && armor == ArmorCategory.Heavy) ||
            (weapon == WeaponCategory.Heavy && armor == ArmorCategory.Light))
            return -1;

        return 0;
    }

    private static int GetAttributeDiceForLevel(int level)
    {
        var bonus = 0;
        if (level >= 6) bonus++;
        if (level >= 11) bonus++;
        if (level >= 16) bonus++;
        return 3 + bonus;
    }

    private static int GetSkillDiceForLevel(int level)
    {
        var bonus = 0;
        if (level >= 4) bonus++;
        if (level >= 8) bonus++;
        if (level >= 12) bonus++;
        return 2 + bonus;
    }

    private static double SimulatedAverageDamage(int level, int samples)
    {
        var matrix = RunMatrixStatic(samples, level);
        return matrix.Average(entry => entry.Value);
    }

    private static Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> RunMatrixStatic(
        int samples,
        int level,
        int? luckOverride = null)
    {
        var helper = new CreatureBalanceDesignTests(SubstituteOutputHelper.Instance);
        return helper.RunMatrix(samples, level, luckOverride);
    }

    private static int EstimatedHpForLevel(int level, int samples, int seedOffset)
    {
        var matrix = RunMatrixStatic(samples, level, luckOverride: null);
        var averageDamage = matrix.Average(entry => entry.Value);
        return (int)Math.Ceiling(averageDamage * 4);
    }

    private class RandomDiceRoller : IDiceRoller
    {
        private readonly Random _random;

        public RandomDiceRoller(int seed)
        {
            _random = new Random(seed);
        }

        public int Roll(int size)
        {
            return _random.Next(1, size + 1);
        }

        public int[] RollMany(int sides, int times)
        {
            var rolls = new int[times];
            for (var i = 0; i < times; i++)
            {
                rolls[i] = Roll(sides);
            }

            return rolls;
        }
    }

    // Helper for static calls that need an ITestOutputHelper.
    private class SubstituteOutputHelper : ITestOutputHelper
    {
        public static readonly SubstituteOutputHelper Instance = new();
        public void WriteLine(string message) { }
        public void WriteLine(string format, params object[] args) { }
    }
}
