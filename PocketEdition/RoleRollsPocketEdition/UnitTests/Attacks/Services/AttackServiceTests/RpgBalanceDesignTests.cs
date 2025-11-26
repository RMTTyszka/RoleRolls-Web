using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RoleRollsPocketEdition.Itens;
using Xunit;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class RpgBalanceDesignTests
{
    private const int BaseComplexity = 10;
    private const int AttributeDice = 3;
    private const int SkillDice = 2;
    private const int DicePerAttack = AttributeDice + SkillDice;
    private const int SimulationSamples = 20000;
    private const int SearchSamples = 2000;
    private const int Seed = 42;
    private const int MaxLevel = 20;
    private readonly ITestOutputHelper _testOutputHelper;

    private static readonly IReadOnlyDictionary<WeaponCategory, WeaponProfile> WeaponProfiles =
        new Dictionary<WeaponCategory, WeaponProfile>
        {
            // Baseline escolhido pela busca: Hit(L/M/H) = (1,0,0)
            [WeaponCategory.Light] = new WeaponProfile(Difficulty: 1, HitBonus: 1, DamageBonusPerHit: 0),
            [WeaponCategory.Medium] = new WeaponProfile(Difficulty: 2, HitBonus: 0, DamageBonusPerHit: 0),
            [WeaponCategory.Heavy] = new WeaponProfile(Difficulty: 3, HitBonus: 0, DamageBonusPerHit: 0)
        };

    // Block 0 representa alvo sem armadura; mesmo a armadura leve deve ter pelo menos 1.
    private static readonly IReadOnlyDictionary<ArmorCategory, ArmorProfile> ArmorProfiles =
        new Dictionary<ArmorCategory, ArmorProfile>
        {
            // Baseline escolhido pela busca: Dodge/Block(L/M/H) = (2,2)/(1,4)/(0,6)
            [ArmorCategory.Light] = new ArmorProfile(DodgeBonus: 2, Block: 2),
            [ArmorCategory.Medium] = new ArmorProfile(DodgeBonus: 1, Block: 4),
            [ArmorCategory.Heavy] = new ArmorProfile(DodgeBonus: 0, Block: 6)
        };

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

    private static readonly Lazy<Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double>> Matrix =
        new(() => RunMatrix(SimulationSamples));

    public RpgBalanceDesignTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact(DisplayName = "Fixed roll example from the prompt is respected")]
    public void FixedExampleMatchesPrompt()
    {
        var rolls = new[] { 12, 8, 14, 15, 9 };
        var noArmor = new ArmorProfile(0, 0);
        // Usa perfis neutros (HitBonus 0) para reproduzir exatamente o exemplo do enunciado.
        var lightProfile = new WeaponProfile(Difficulty: 1, HitBonus: 0, DamageBonusPerHit: 0);
        var heavyProfile = new WeaponProfile(Difficulty: 3, HitBonus: 0, DamageBonusPerHit: 0);

        var lightOutcome = ResolveAttack(rolls, lightProfile, noArmor);
        lightOutcome.Hits.Should().Be(3);
        lightOutcome.DamagePerHit.Should().BeEquivalentTo(new[] { 2, 4, 5 });
        lightOutcome.TotalDamage.Should().Be(11);

        var heavyOutcome = ResolveAttack(rolls, heavyProfile, noArmor);
        heavyOutcome.Hits.Should().Be(1);
        heavyOutcome.TotalDamage.Should().Be(11);
    }

    [Fact(DisplayName = "Matching weapon weight is best against armor of the same weight")]
    public void MatchingWeightsAreOptimal()
    {
        var results = Matrix.Value;
        foreach (var armor in ArmorsUnderTest)
        {
            var bestWeapon = results
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

    [Fact(DisplayName = "All combinations stay viable and respect counters")]
    public void BalanceEnvelopeAndCounters()
    {
        var results = Matrix.Value;

        var min = results.Min(entry => entry.Value);
        var max = results.Max(entry => entry.Value);
        (min / max).Should().BeGreaterThan(0.25);

        results[(WeaponCategory.Light, ArmorCategory.Light)]
            .Should().BeGreaterThan(results[(WeaponCategory.Heavy, ArmorCategory.Light)]);

        results[(WeaponCategory.Heavy, ArmorCategory.Heavy)]
            .Should().BeGreaterThan(results[(WeaponCategory.Light, ArmorCategory.Heavy)]);

        results[(WeaponCategory.Medium, ArmorCategory.Medium)]
            .Should().BeGreaterThan(results[(WeaponCategory.Light, ArmorCategory.Medium)]);

        results[(WeaponCategory.Medium, ArmorCategory.Medium)]
            .Should().BeGreaterThan(results[(WeaponCategory.Heavy, ArmorCategory.Medium)]);

        // Heavy armor should be punished most by heavy weapons, not by medium.
        results[(WeaponCategory.Heavy, ArmorCategory.Heavy)]
            .Should().BeGreaterThanOrEqualTo(results[(WeaponCategory.Medium, ArmorCategory.Heavy)]);
    }

    [Fact(DisplayName = "Find balanced profiles among candidates")]
    public void AutoSearchBalancedProfiles()
    {
        var hitOptions = new[] { 0, 1 };
        var lightDodgeOptions = new[] { 1, 2 };
        var lightBlockOptions = new[] { 1, 2 };
        var mediumDodgeOptions = new[] { 1, 2 };
        var mediumBlockOptions = new[] { 2, 3, 4 };
        var heavyBlockOptions = new[] { 6, 7 };

        var rng = new Random(Seed);
        var best = new List<(double Score, string Summary,
            Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> Matrix)>();

        foreach (var lightHit in hitOptions)
        foreach (var mediumHit in hitOptions)
        foreach (var heavyHit in hitOptions)
        foreach (var lightDodge in lightDodgeOptions)
        foreach (var lightBlock in lightBlockOptions)
        foreach (var mediumDodge in mediumDodgeOptions)
        foreach (var mediumBlock in mediumBlockOptions)
        foreach (var heavyBlock in heavyBlockOptions)
        {
            var weaponProfiles = new Dictionary<WeaponCategory, WeaponProfile>
            {
                [WeaponCategory.Light] = new WeaponProfile(1, lightHit, 0),
                [WeaponCategory.Medium] = new WeaponProfile(2, mediumHit, 0),
                [WeaponCategory.Heavy] = new WeaponProfile(3, heavyHit, 0),
            };

            var armorProfiles = new Dictionary<ArmorCategory, ArmorProfile>
            {
                [ArmorCategory.Light] = new ArmorProfile(lightDodge, lightBlock),
                [ArmorCategory.Medium] = new ArmorProfile(mediumDodge, mediumBlock),
                [ArmorCategory.Heavy] = new ArmorProfile(0, heavyBlock),
            };

            var results = RunMatrix(SearchSamples, rng, weaponProfiles, armorProfiles);
            var ratio = results.Min(e => e.Value) / results.Max(e => e.Value);
            if (ratio < 0.25) continue;

            if (!DominanceHolds(results)) continue;

            var summary =
                $"Hit(L/M/H)=({lightHit},{mediumHit},{heavyHit}) " +
                $"Dodge/Block(L/M/H)=({lightDodge},{lightBlock})/({mediumDodge},{mediumBlock})/(0,{heavyBlock}) " +
                $"Ratio={ratio:F3}";
            best.Add((ratio, summary, results));
        }

        best.Should().NotBeEmpty("deveria existir pelo menos um perfil equilibrado nos candidatos");
        var top = best.MaxBy(entry => entry.Score);
        _testOutputHelper.WriteLine(top.Summary);
    }

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

    [Fact(DisplayName = "Level scaling keeps balance per tier")]
    public void LevelScalingKeepsBalance()
    {
        var perLevelLog = new List<string>();
        var weaponTotalsAllLevels = WeaponsUnderTest.ToDictionary(w => w, _ => 0.0);
        var armorTotalsAllLevels = ArmorsUnderTest.ToDictionary(a => a, _ => 0.0);

        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var (weaponProfiles, armorProfiles) = BuildLevelProfiles(level);
            var dicePerAttack = GetDicePerAttackForLevel(level);
            var rng = new Random(Seed + level); // pequena variacao para reduzir vi sesgo de amostra
            var results = RunMatrix(SearchSamples, rng, weaponProfiles, armorProfiles, dicePerAttack);

            DominanceHolds(results).Should()
                .BeTrue($"dominancia deve se manter no nivel {level}");

            var ratio = results.Min(e => e.Value) / results.Max(e => e.Value);
            ratio.Should().BeGreaterThan(0.07, $"viabilidade minima por nivel; nivel {level} ficou desequilibrado");

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
    }
    private static (IReadOnlyDictionary<WeaponCategory, WeaponProfile> Weapons,
        IReadOnlyDictionary<ArmorCategory, ArmorProfile> Armors) BuildLevelProfiles(int level)
    {
        // Equipamentos já começam no tier 1 (nível 1 => tier 1), e sobem a cada 2 níveis.
        var tier = 1 + (level - 1) / 2;

        var weapons = new Dictionary<WeaponCategory, WeaponProfile>
        {
            [WeaponCategory.Light] = new WeaponProfile(
                Difficulty: 1,
                HitBonus: 1,
                DamageBonusPerHit: tier * 3),
            [WeaponCategory.Medium] = new WeaponProfile(
                Difficulty: 2,
                HitBonus: 0,
                DamageBonusPerHit: tier * 5),
            [WeaponCategory.Heavy] = new WeaponProfile(
                Difficulty: 3,
                HitBonus: 0,
                DamageBonusPerHit: tier * 6 + 2)
        };

        var armors = new Dictionary<ArmorCategory, ArmorProfile>
        {
            [ArmorCategory.Light] = new ArmorProfile(DodgeBonus: 2, Block: 2 + tier * 1),
            [ArmorCategory.Medium] = new ArmorProfile(DodgeBonus: 1, Block: 4 + tier * 2),
            [ArmorCategory.Heavy] = new ArmorProfile(DodgeBonus: -1, Block: 6 + tier * 3)
        };

        return (weapons, armors);
    }

    [Fact(DisplayName = "Average HP needed to survive 4 rounds per level")]
    public void HitPointsNeededForFourRounds()
    {
        var report = new List<string>();

        foreach (var level in Enumerable.Range(1, MaxLevel))
        {
            var (weaponProfiles, armorProfiles) = BuildLevelProfiles(level);
            var dicePerAttack = GetDicePerAttackForLevel(level);
            var rng = new Random(Seed + level * 17);
            var matrix = RunMatrix(SearchSamples, rng, weaponProfiles, armorProfiles, dicePerAttack);

            foreach (var armor in ArmorsUnderTest)
            {
                // Pior caso: arma com maior DPS médio contra esta armadura.
                var worst = matrix
                    .Where(e => e.Key.Armor == armor)
                    .MaxBy(e => e.Value);

                var hpNeeded = Math.Ceiling(worst.Value * 4); // 4 turnos de fôlego.
                hpNeeded.Should().BeGreaterThan(0);
                report.Add($"Level {level:00} Armor {armor,-6}: needs ~{hpNeeded} HP (worst vs {worst.Key.Weapon}, avg dmg {worst.Value:F2})");
            }
        }

        foreach (var line in report)
        {
            _testOutputHelper.WriteLine(line);
        }
    }

    private static int GetAttributeDiceForLevel(int level)
    {
        var bonus = 0;
        if (level >= 5) bonus++;
        if (level >= 10) bonus++;
        if (level >= 15) bonus++;
        if (level >= 20) bonus++;
        return AttributeDice + bonus;
    }

    private static int GetDicePerAttackForLevel(int level) => GetAttributeDiceForLevel(level) + SkillDice;

    private static Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> RunMatrix(int samples)
    {
        var rng = new Random(Seed);
        return RunMatrix(samples, rng, WeaponProfiles, ArmorProfiles, DicePerAttack);
    }

    private static Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> RunMatrix(
        int samples,
        Random rng,
        IReadOnlyDictionary<WeaponCategory, WeaponProfile> weaponProfiles,
        IReadOnlyDictionary<ArmorCategory, ArmorProfile> armorProfiles,
        int dicePerAttack = DicePerAttack)
    {
        var output = new Dictionary<(WeaponCategory, ArmorCategory), double>();

        foreach (var weapon in WeaponsUnderTest)
        {
            foreach (var armor in ArmorsUnderTest)
            {
                double total = 0;
                var profile = armorProfiles[armor];
                var weaponProfile = weaponProfiles[weapon];

                for (var i = 0; i < samples; i++)
                {
                    total += ResolveAttack(RollDice(rng, dicePerAttack), weaponProfile, profile).TotalDamage;
                }

                output[(weapon, armor)] = total / samples;
            }
        }

        return output;
    }

    private static AttackOutcome ResolveAttack(IReadOnlyCollection<int> rolls, WeaponProfile weapon, ArmorProfile armor)
    {
        var complexity = BaseComplexity + armor.DodgeBonus;
        var successes = rolls
            .Select(roll => roll + weapon.HitBonus - complexity)
            .Where(over => over >= 0)
            .OrderByDescending(over => over)
            .ToList();

        var hits = successes.Count / weapon.Difficulty;
        var damages = new List<int>(hits);

        for (var i = 0; i < hits; i++)
        {
            // Group successes per difficulty, sum them, then apply block once per hit.
            var chunkDamage = successes.Skip(i * weapon.Difficulty).Take(weapon.Difficulty).Sum() +
                              weapon.DamageBonusPerHit;
            damages.Add(Math.Max(chunkDamage - armor.Block, 0));
        }

        return new AttackOutcome(hits, damages.Sum(), damages);
    }

    private static IReadOnlyCollection<int> RollDice(Random rng, int dicePerAttack = DicePerAttack)
    {
        var rolls = new int[dicePerAttack];
        for (var i = 0; i < dicePerAttack; i++)
        {
            rolls[i] = rng.Next(1, 21);
        }

        return rolls;
    }

    private record ArmorProfile(int DodgeBonus, int Block);

    private record WeaponProfile(int Difficulty, int HitBonus, int DamageBonusPerHit);

    private record AttackOutcome(int Hits, int TotalDamage, IReadOnlyList<int> DamagePerHit);
}

