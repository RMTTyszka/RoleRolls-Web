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
    private const int MinLevelUnderTest = 1;
    private const int MaxLevelUnderTest = 20;

    private static readonly int[] LevelsUnderTest =
        Enumerable.Range(MinLevelUnderTest, MaxLevelUnderTest - MinLevelUnderTest + 1).ToArray();

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

    private static readonly GripType[] StandardOffensiveGripTypesUnderTest =
    {
        GripType.OneLightWeapon,
        GripType.OneMediumWeapon,
        GripType.TwoHandedHeavyWeapon
    };

    private static readonly GripType[] OffensiveGripTypesUnderTest =
    {
        GripType.OneLightWeapon,
        GripType.OneMediumWeapon,
        GripType.TwoHandedHeavyWeapon,
        GripType.TwoWeaponsLight,
        GripType.TwoWeaponsMedium,
        GripType.OneHandedHeavyWeapon,
        GripType.TwoHandedMediumWeapon
    };

    private static readonly GripType[] OffensiveBalanceGripTypesUnderTest =
    {
        GripType.OneLightWeapon,
        GripType.OneMediumWeapon,
        GripType.TwoHandedHeavyWeapon,
        GripType.OneHandedHeavyWeapon,
        GripType.TwoHandedMediumWeapon
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

        foreach (var level in LevelsUnderTest)
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
            var avgDelta = perWeaponTotals[weapon] / LevelsUnderTest.Length;
            avgDelta.Should().BeGreaterThan(0, $"sorte +1 deve aumentar dano em media para {weapon}");
            _testOutputHelper.WriteLine($"Luck +1 average delta for {weapon,-6}: {avgDelta:F2} dmg");
        }
    }

    [Fact(DisplayName = "Um dado extra aumenta dano medio (Creature.Attack)")]
    public void ExtraDiceBoostsDamagePerWeaponPerLevel()
    {
        var perWeaponTotals = WeaponsUnderTest.ToDictionary(w => w, _ => 0.0);

        foreach (var level in LevelsUnderTest)
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
            var avgDelta = perWeaponTotals[weapon] / LevelsUnderTest.Length;
            avgDelta.Should().BeGreaterThan(0, $"+1 dado deve aumentar dano em media para {weapon}");
            _testOutputHelper.WriteLine($"+1 die average delta for {weapon,-6}: {avgDelta:F2} dmg");
        }
    }

    [Fact(DisplayName = "Advantage (+1 dado fixo 15) aumenta dano medio (Creature.Attack)")]
    public void AdvantageBoostsDamagePerWeaponPerLevel()
    {
        var perWeaponTotals = WeaponsUnderTest.ToDictionary(w => w, _ => 0.0);

        foreach (var level in LevelsUnderTest)
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
            var avgDelta = perWeaponTotals[weapon] / LevelsUnderTest.Length;
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

        foreach (var level in LevelsUnderTest)
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
            (min / max).Should().BeGreaterThan(0.05);
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

        foreach (var level in LevelsUnderTest)
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
        var aggMin = aggregated.Min(e => e.Value);
        var aggMax = aggregated.Max(e => e.Value);
        if (aggMax > 0)
        {
            (aggMin / aggMax).Should().BeGreaterThan(0.05, "viabilidade media por nivel");
        }
        DominanceHolds(aggregated).Should().BeTrue("dominancia deve se manter na media dos niveis");
        
    }

    [Fact(DisplayName = "Escalonamento por nivel mantem balance por grip type ofensivo (Creature.Attack)")]
    public void GripTypeLevelScalingKeepsBalance()
    {
        var aggregated = OffensiveGripTypesUnderTest
            .SelectMany(g => ArmorsUnderTest.Select(a => (g, a)))
            .ToDictionary(pair => pair, _ => 0.0);
        var balanceAggregated = OffensiveBalanceGripTypesUnderTest
            .SelectMany(g => ArmorsUnderTest.Select(a => (g, a)))
            .ToDictionary(pair => pair, _ => 0.0);
        var perLevelLog = new List<string>();
        var gripTotalsAllLevels = OffensiveGripTypesUnderTest.ToDictionary(g => g, _ => 0.0);
        var armorTotalsAllLevels = ArmorsUnderTest.ToDictionary(a => a, _ => 0.0);

        foreach (var level in LevelsUnderTest)
        {
            var results = RunGripMatrix(SearchSamples, level);

            foreach (var entry in results)
            {
                aggregated[entry.Key] += entry.Value;
                if (balanceAggregated.ContainsKey(entry.Key))
                {
                    balanceAggregated[entry.Key] += entry.Value;
                }
            }

            perLevelLog.Add($"Level {level:00}");

            foreach (var gripType in OffensiveGripTypesUnderTest)
            {
                foreach (var armor in ArmorsUnderTest)
                {
                    var dmg = results[(gripType, armor)];
                    perLevelLog.Add($"  Grip {gripType,-22} vs Armor {armor,-6}: {dmg:F2}");
                }
            }

            foreach (var gripType in OffensiveGripTypesUnderTest)
            {
                var gripSum = ArmorsUnderTest.Sum(armor => results[(gripType, armor)]);
                perLevelLog.Add($"  Grip {gripType,-22} total (lvl): {gripSum:F2}");
                gripTotalsAllLevels[gripType] += gripSum;
            }

            foreach (var armor in ArmorsUnderTest)
            {
                var armorSum = OffensiveGripTypesUnderTest.Sum(gripType => results[(gripType, armor)]);
                perLevelLog.Add($"  Armor {armor,-6} total (lvl): {armorSum:F2}");
                armorTotalsAllLevels[armor] += armorSum;
            }

            var oneHandedHeavyTotal = TotalDamage(results, GripType.OneHandedHeavyWeapon);
            var twoHandedHeavyTotal = TotalDamage(results, GripType.TwoHandedHeavyWeapon);
            oneHandedHeavyTotal.Should().BeLessThan(
                twoHandedHeavyTotal,
                $"heavy de uma mao deve causar menos dano que heavy de duas maos no level {level} ({oneHandedHeavyTotal:F2} vs {twoHandedHeavyTotal:F2})");
        }

        perLevelLog.Add("=== Totais agregados por grip type (todos os niveis) ===");
        foreach (var gripType in OffensiveGripTypesUnderTest)
        {
            perLevelLog.Add($"Grip {gripType,-22} total (all levels): {gripTotalsAllLevels[gripType]:F2}");
        }

        foreach (var armor in ArmorsUnderTest)
        {
            perLevelLog.Add($"Armor {armor,-6} total (all levels): {armorTotalsAllLevels[armor]:F2}");
        }

        foreach (var line in perLevelLog)
        {
            _testOutputHelper.WriteLine(line);
        }

        var aggMin = balanceAggregated.Min(e => e.Value);
        var aggMax = balanceAggregated.Max(e => e.Value);
        if (aggMax > 0)
        {
            (aggMin / aggMax).Should().BeGreaterThan(0.05, "viabilidade media por nivel por grip type sem combinacao");
        }

        OffensiveGripDominanceHolds(balanceAggregated).Should().BeTrue("dominancia ofensiva sem combinacao deve se manter na media dos niveis");
        OffensiveGripDamageOrderHolds(balanceAggregated).Should().BeTrue("heavy de uma mao e medium de duas maos devem ficar entre medium e heavy em dano agregado");
        DualWieldOutdamagesSingleWeapon(aggregated).Should().BeTrue("dual wield deve causar mais dano agregado que arma equivalente sozinha");
    }

    [Theory(DisplayName = "Loga diferenca de dano por grip type e nivel para perfil inicial (Creature.Attack)")]
    [InlineData(2, 1)]
    public void GripTypeDamageDifferenceByLevelIsReported(int initialAttributePoints, int initialSpecificSkillPoints)
    {
        foreach (var level in LevelsUnderTest)
        {
            var referenceResults = RunGripMatrix(SearchSamples, level, StandardOffensiveGripTypesUnderTest,
                initialAttributePoints: 3,
                initialSpecificSkillPoints: 1);
            var scenarioResults = RunGripMatrix(SearchSamples, level, StandardOffensiveGripTypesUnderTest,
                initialAttributePoints,
                initialSpecificSkillPoints);

            _testOutputHelper.WriteLine($"Level {level:00} | Profile {initialAttributePoints}/{initialSpecificSkillPoints} vs 3/1");
            foreach (var gripType in StandardOffensiveGripTypesUnderTest)
            {
                foreach (var armor in ArmorsUnderTest)
                {
                    var referenceDamage = referenceResults[(gripType, armor)];
                    var scenarioDamage = scenarioResults[(gripType, armor)];
                    var difference = scenarioDamage - referenceDamage;
                    var differencePercentage = referenceDamage == 0
                        ? 0
                        : difference / referenceDamage * 100;

                    _testOutputHelper.WriteLine(
                        $"  Grip {gripType,-22} | Armor {armor,-6} | Reference {referenceDamage:F2} | Scenario {scenarioDamage:F2} | Delta {difference:F2} ({differencePercentage:F2}%)");
                }
            }
        }
    }

    [Fact(DisplayName = "Dual wield usa dois ataques menores que arma solo (Creature.Attack)")]
    public void DualWieldUsesTwoWeakerAttacks()
    {
        var level = 10;
        var armor = ArmorCategory.Medium;

        var singleLight = SimulateAverageDamageForSlot(level, GripType.OneLightWeapon, armor, SearchSamples, EquipableSlot.MainHand);
        var dualLightMain = SimulateAverageDamageForSlot(level, GripType.TwoWeaponsLight, armor, SearchSamples, EquipableSlot.MainHand);
        var dualLightOff = SimulateAverageDamageForSlot(level, GripType.TwoWeaponsLight, armor, SearchSamples, EquipableSlot.OffHand);

        dualLightMain.Should().BeLessThan(singleLight, "cada arma leve em dual wield deve bater menos que arma leve sozinha");
        dualLightOff.Should().BeLessThan(singleLight, "offhand leve em dual wield deve bater menos que arma leve sozinha");
        (dualLightMain + dualLightOff).Should().BeGreaterThan(singleLight, "a soma das duas armas leves deve superar arma leve sozinha");

        var singleMedium = SimulateAverageDamageForSlot(level, GripType.OneMediumWeapon, armor, SearchSamples, EquipableSlot.MainHand);
        var dualMediumMain = SimulateAverageDamageForSlot(level, GripType.TwoWeaponsMedium, armor, SearchSamples, EquipableSlot.MainHand);
        var dualMediumOff = SimulateAverageDamageForSlot(level, GripType.TwoWeaponsMedium, armor, SearchSamples, EquipableSlot.OffHand);

        dualMediumMain.Should().BeLessThan(singleMedium, "cada arma media em dual wield deve bater menos que arma media sozinha");
        dualMediumOff.Should().BeLessThan(singleMedium, "offhand media em dual wield deve bater menos que arma media sozinha");
        (dualMediumMain + dualMediumOff).Should().BeGreaterThan(singleMedium, "a soma das duas armas medias deve superar arma media sozinha");
    }

    [Fact(DisplayName = "HP medio necessario para 4 turnos (Creature.Attack)")]
    public void HitPointsNeededForFourRounds()
    {
        var report = new List<string>();

        var anyPositive = false;

        foreach (var level in LevelsUnderTest)
        {
            var averageDamage = SimulatedAverageDamage(level, SearchSamples);
            var hpNeeded = Math.Ceiling(averageDamage * 4);
            if (hpNeeded > 0)
            {
                anyPositive = true;
            }

            var creature = new BaseCreature(LandOfHeroesTemplate.Template, $"hp lvl {level}")
                .WithLevel(level)
                .Creature;
            creature.FullRestore();

            var basicAttackVitalities = creature.Vitalities
                .Where(vitality => vitality.VitalityTemplate?.BasicAttackOrder is > 0)
                .OrderBy(vitality => vitality.VitalityTemplate!.BasicAttackOrder)
                .ThenBy(vitality => vitality.Name)
                .ToList();

            var basicAttackVitalitiesLog = string.Join(
                " | ",
                basicAttackVitalities.Select(vitality => $"{vitality.Name}: {vitality.MaxValue}"));
            var basicAttackVitalitiesTotal = basicAttackVitalities.Sum(vitality => vitality.MaxValue);

            report.Add(
                $"Level {level:00}: needs ~{hpNeeded} HP (avg dmg {averageDamage:F2} |[{basicAttackVitalitiesLog}] | total {basicAttackVitalitiesTotal}");
        }

        anyPositive.Should().BeTrue("algum nivel deve gerar dano positivo para justificar o HP medio");

        foreach (var line in report)
        {
            _testOutputHelper.WriteLine(line);
        }
    }

    [Fact(DisplayName = "Loga todas as vitalities da criatura por nivel (Creature.Attack)")]
    public void LogCreatureVitalitiesPerLevel()
    {
        foreach (var level in LevelsUnderTest)
        {
            var creature = new BaseCreature(LandOfHeroesTemplate.Template, $"hp lvl {level}")
                .WithLevel(level)
                .Creature;

            creature.FullRestore();

            var vitalitySummary = string.Join(
                " | ",
                creature.Vitalities
                    .OrderBy(vitality => vitality.Name)
                    .Select(vitality => $"{vitality.Name}: {vitality.MaxValue}"));

            _testOutputHelper.WriteLine($"Level {level:00}: {vitalitySummary}");
        }
    }

    [Fact(DisplayName = "Estimativa de HP se aproxima da simulacao (Creature.Attack)")]
    public void ApproximateHpMatchesSimulation()
    {
        foreach (var level in LevelsUnderTest)
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
                var seed = Seed + level * 31 + (int)weapon * 7 + (int)armor * 13;
                var roller = new RandomDiceRoller(seed);
                var dmg = SimulateAverageDamage(level, weapon, armor, samples, roller, luckOverride, extraAttackDice,
                    advantage);
                output[(weapon, armor)] = dmg;
            }
        }

        return output;
    }

    private Dictionary<(GripType GripType, ArmorCategory Armor), double> RunGripMatrix(
        int samples,
        int level,
        IReadOnlyCollection<GripType>? gripTypes = null,
        int initialAttributePoints = 3,
        int initialSpecificSkillPoints = 1)
    {
        var output = new Dictionary<(GripType, ArmorCategory), double>();

        foreach (var gripType in gripTypes ?? OffensiveGripTypesUnderTest)
        {
            foreach (var armor in ArmorsUnderTest)
            {
                var seed = Seed + level * 31 + (int)armor * 13;
                var roller = new RandomDiceRoller(seed);
                var dmg = SimulateAverageDamage(level, gripType, armor, samples, roller, initialAttributePoints,
                    initialSpecificSkillPoints);
                output[(gripType, armor)] = dmg;
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

        var command = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config,
            Luck = luckOverride ?? 0,
            Advantage = advantage
        };

        double total = 0;
        for (var i = 0; i < samples; i++)
        {
            var result = attacker.BasicAttack(defender, command, diceRoller);
            total += result.TotalDamage;
            defender.FullRestore();
        }

        return total / samples;
    }

    private double SimulateAverageDamage(
        int level,
        GripType gripType,
        ArmorCategory armor,
        int samples,
        IDiceRoller diceRoller,
        int initialAttributePoints = 3,
        int initialSpecificSkillPoints = 1)
    {
        var config = LandOfHeroesTemplate.Template.ItemConfiguration;
        var attacker = BuildAttacker(gripType, level, initialAttributePoints, initialSpecificSkillPoints);
        var defender = BuildDefender(armor, level);

        var command = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config
        };

        double total = 0;
        for (var i = 0; i < samples; i++)
        {
            var mainHandResult = attacker.BasicAttack(defender, command, diceRoller);
            var sampleDamage = mainHandResult.TotalDamage;

            if (GetLoadout(gripType).OffHand is not null)
            {
                command.WeaponSlot = EquipableSlot.OffHand;
                var offHandResult = attacker.BasicAttack(defender, command, diceRoller);
                sampleDamage += offHandResult.TotalDamage;
                command.WeaponSlot = EquipableSlot.MainHand;
            }

            total += sampleDamage;
            defender.FullRestore();
        }

        return total / samples;
    }

    private double SimulateAverageDamageForSlot(
        int level,
        GripType gripType,
        ArmorCategory armor,
        int samples,
        EquipableSlot weaponSlot)
    {
        var config = LandOfHeroesTemplate.Template.ItemConfiguration;
        var attacker = BuildAttacker(gripType, level);
        var defender = BuildDefender(armor, level);
        var diceRoller = new RandomDiceRoller(Seed + level * 31 + (int)armor * 13 + (int)weaponSlot * 17);

        var command = new BasicAttackCommand
        {
            WeaponSlot = weaponSlot,
            ItemConfiguration = config
        };

        double total = 0;
        for (var i = 0; i < samples; i++)
        {
            var result = attacker.BasicAttack(defender, command, diceRoller);
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

    private static Creature BuildAttacker(GripType gripType, int level, int initialAttributePoints = 3,
        int initialSpecificSkillPoints = 1)
    {
        var loadout = GetLoadout(gripType);
        var builder = new BaseCreature(LandOfHeroesTemplate.Template, $"{gripType} attacker lvl {level}")
            .WithLevel(level)
            .WithWeapon(loadout.MainHand.Category, EquipableSlot.MainHand, level, loadout.MainHand.GripType);

        if (loadout.OffHand is not null)
        {
            builder.WithWeapon(loadout.OffHand.Value.Category, EquipableSlot.OffHand, level, loadout.OffHand.Value.GripType);
        }

        var attacker = builder.Creature;
        attacker.Equipment.GripType.Should().Be(gripType, $"{gripType} deve ser atingido por Equip real");

        var attributeDice = GetAttributeDiceForLevel(level, initialAttributePoints);
        var skillDice = GetSkillDiceForLevel(level, initialSpecificSkillPoints);
        SetWeaponDice(attacker, loadout.MainHand.Category, attributeDice, skillDice);
        attacker.FullRestore();
        return attacker;
    }

    private static Creature BuildDefender(ArmorCategory armor, int level)
    {
        var defender = new BaseCreature(LandOfHeroesTemplate.Template, $"{armor} defender lvl {level}")
            .WithLevel(level)
            .WithArmor(armor, level)
            .Creature;

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

    private static (WeaponGrip MainHand, WeaponGrip? OffHand) GetLoadout(GripType gripType) => gripType switch
    {
        GripType.OneLightWeapon => (new WeaponGrip(WeaponCategory.Light, GripType.OneLightWeapon), null),
        GripType.OneMediumWeapon => (new WeaponGrip(WeaponCategory.Medium, GripType.OneMediumWeapon), null),
        GripType.TwoHandedHeavyWeapon => (new WeaponGrip(WeaponCategory.Heavy, GripType.TwoHandedHeavyWeapon), null),
        GripType.TwoWeaponsLight => (
            new WeaponGrip(WeaponCategory.Light, GripType.OneLightWeapon),
            new WeaponGrip(WeaponCategory.Light, GripType.OneLightWeapon)),
        GripType.TwoWeaponsMedium => (
            new WeaponGrip(WeaponCategory.Medium, GripType.OneMediumWeapon),
            new WeaponGrip(WeaponCategory.Medium, GripType.OneMediumWeapon)),
        GripType.OneHandedHeavyWeapon => (new WeaponGrip(WeaponCategory.Heavy, GripType.OneHandedHeavyWeapon), null),
        GripType.TwoHandedMediumWeapon => (new WeaponGrip(WeaponCategory.Medium, GripType.TwoHandedMediumWeapon), null),
        _ => throw new ArgumentOutOfRangeException(nameof(gripType), gripType, null)
    };

    private static bool DominanceHolds(Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> results)
    {
        return CompareIfPresent(
                   results,
                   (WeaponCategory.Light, ArmorCategory.Light),
                   (WeaponCategory.Heavy, ArmorCategory.Light),
                   (left, right) => left > right)
               && CompareIfPresent(
                   results,
                   (WeaponCategory.Medium, ArmorCategory.Medium),
                   (WeaponCategory.Light, ArmorCategory.Medium),
                   (left, right) => left > right)
               && CompareIfPresent(
                   results,
                   (WeaponCategory.Heavy, ArmorCategory.Heavy),
                   (WeaponCategory.Medium, ArmorCategory.Heavy),
                   (left, right) => left >= right);
    }

    private static bool OffensiveGripDominanceHolds(Dictionary<(GripType GripType, ArmorCategory Armor), double> results)
    {
        return CompareIfPresent(
                   results,
                   (GripType.OneLightWeapon, ArmorCategory.Light),
                   (GripType.TwoHandedHeavyWeapon, ArmorCategory.Light),
                   (left, right) => left > right)
               && CompareIfPresent(
                   results,
                   (GripType.OneMediumWeapon, ArmorCategory.Medium),
                   (GripType.OneLightWeapon, ArmorCategory.Medium),
                   (left, right) => left > right)
               && CompareIfPresent(
                   results,
                   (GripType.TwoHandedHeavyWeapon, ArmorCategory.Heavy),
                   (GripType.OneMediumWeapon, ArmorCategory.Heavy),
                   (left, right) => left >= right);
    }

    private static bool OffensiveGripDamageOrderHolds(Dictionary<(GripType GripType, ArmorCategory Armor), double> results)
    {
        var medium = TotalDamage(results, GripType.OneMediumWeapon);
        var oneHandedHeavy = TotalDamage(results, GripType.OneHandedHeavyWeapon);
        var twoHandedMedium = TotalDamage(results, GripType.TwoHandedMediumWeapon);
        var heavy = TotalDamage(results, GripType.TwoHandedHeavyWeapon);

        return oneHandedHeavy > medium
               && oneHandedHeavy < heavy
               && twoHandedMedium > medium
               && twoHandedMedium < heavy;
    }

    private static bool DualWieldOutdamagesSingleWeapon(Dictionary<(GripType GripType, ArmorCategory Armor), double> results)
    {
        return TotalDamage(results, GripType.TwoWeaponsLight) > TotalDamage(results, GripType.OneLightWeapon)
               && TotalDamage(results, GripType.TwoWeaponsMedium) > TotalDamage(results, GripType.OneMediumWeapon);
    }

    private static double TotalDamage(
        Dictionary<(GripType GripType, ArmorCategory Armor), double> results,
        GripType gripType)
    {
        return ArmorsUnderTest.Sum(armor => results.TryGetValue((gripType, armor), out var damage) ? damage : 0);
    }

    private static bool CompareIfPresent(
        Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> results,
        (WeaponCategory Weapon, ArmorCategory Armor) leftKey,
        (WeaponCategory Weapon, ArmorCategory Armor) rightKey,
        Func<double, double, bool> comparison)
    {
        if (!results.TryGetValue(leftKey, out var left) || !results.TryGetValue(rightKey, out var right))
        {
            return true;
        }

        return comparison(left, right);
    }

    private static bool CompareIfPresent(
        Dictionary<(GripType GripType, ArmorCategory Armor), double> results,
        (GripType GripType, ArmorCategory Armor) leftKey,
        (GripType GripType, ArmorCategory Armor) rightKey,
        Func<double, double, bool> comparison)
    {
        if (!results.TryGetValue(leftKey, out var left) || !results.TryGetValue(rightKey, out var right))
        {
            return true;
        }

        return comparison(left, right);
    }

    private static int GetAttributeDiceForLevel(int level, int initialAttributePoints = 3)
    {
        var bonus = 0;
        if (level >= 6) bonus++;
        if (level >= 11) bonus++;
        if (level >= 16) bonus++;
        return initialAttributePoints + bonus;
    }

    private static int GetSkillDiceForLevel(int level, int initialSpecificSkillPoints = 1)
    {
        var bonus = 0;
        if (level >= 4) bonus++;
        if (level >= 8) bonus++;
        if (level >= 12) bonus++;
        return initialSpecificSkillPoints + bonus;
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

    private readonly record struct WeaponGrip(WeaponCategory Category, GripType GripType);

    // Helper for static calls that need an ITestOutputHelper.
    private class SubstituteOutputHelper : ITestOutputHelper
    {
        public static readonly SubstituteOutputHelper Instance = new();

        public void WriteLine(string message)
        {
        }

        public void WriteLine(string format, params object[] args)
        {
        }
    }
}
