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
    private readonly ITestOutputHelper _testOutputHelper;

    private static readonly IReadOnlyDictionary<WeaponCategory, WeaponProfile> WeaponProfiles =
        new Dictionary<WeaponCategory, WeaponProfile>
        {
            // Baseline escolhido pela busca: Hit(L/M/H) = (1,0,0)
            [WeaponCategory.Light] = new WeaponProfile(Difficulty: 1, HitBonus: 1),
            [WeaponCategory.Medium] = new WeaponProfile(Difficulty: 2, HitBonus: 0),
            [WeaponCategory.Heavy] = new WeaponProfile(Difficulty: 3, HitBonus: 0)
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
        var lightProfile = new WeaponProfile(Difficulty: 1, HitBonus: 0);
        var heavyProfile = new WeaponProfile(Difficulty: 3, HitBonus: 0);

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
                [WeaponCategory.Light] = new WeaponProfile(1, lightHit),
                [WeaponCategory.Medium] = new WeaponProfile(2, mediumHit),
                [WeaponCategory.Heavy] = new WeaponProfile(3, heavyHit),
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

    private static Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> RunMatrix(int samples)
    {
        var rng = new Random(Seed);
        return RunMatrix(samples, rng, WeaponProfiles, ArmorProfiles);
    }

    private static Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> RunMatrix(
        int samples,
        Random rng,
        IReadOnlyDictionary<WeaponCategory, WeaponProfile> weaponProfiles,
        IReadOnlyDictionary<ArmorCategory, ArmorProfile> armorProfiles)
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
                    total += ResolveAttack(RollDice(rng), weaponProfile, profile).TotalDamage;
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
            var chunkDamage = successes.Skip(i * weapon.Difficulty).Take(weapon.Difficulty).Sum();
            damages.Add(Math.Max(chunkDamage - armor.Block, 0));
        }

        return new AttackOutcome(hits, damages.Sum(), damages);
    }

    private static IReadOnlyCollection<int> RollDice(Random rng)
    {
        var rolls = new int[DicePerAttack];
        for (var i = 0; i < DicePerAttack; i++)
        {
            rolls[i] = rng.Next(1, 21);
        }

        return rolls;
    }

    private record ArmorProfile(int DodgeBonus, int Block);

    private record WeaponProfile(int Difficulty, int HitBonus);

    private record AttackOutcome(int Hits, int TotalDamage, IReadOnlyList<int> DamagePerHit);
}
