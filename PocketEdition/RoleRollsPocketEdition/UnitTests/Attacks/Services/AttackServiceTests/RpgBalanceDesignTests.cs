using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RoleRollsPocketEdition.Itens;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class RpgBalanceDesignTests
{
    private const int BaseComplexity = 10;
    private const int AttributeDice = 3;
    private const int SkillDice = 2;
    private const int DicePerAttack = AttributeDice + SkillDice;
    private const int SimulationSamples = 20000;
    private const int Seed = 42;

    private static readonly IReadOnlyDictionary<WeaponCategory, int> DifficultyByWeapon = new Dictionary<WeaponCategory, int>
    {
        [WeaponCategory.Light] = 1,
        [WeaponCategory.Medium] = 2,
        [WeaponCategory.Heavy] = 3
    };

    private static readonly IReadOnlyDictionary<ArmorCategory, ArmorProfile> ArmorProfiles =
        new Dictionary<ArmorCategory, ArmorProfile>
        {
            [ArmorCategory.Light] = new ArmorProfile(DodgeBonus: 2, Block: 0),
            [ArmorCategory.Medium] = new ArmorProfile(DodgeBonus: 1, Block: 2),
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

    [Fact(DisplayName = "Fixed roll example from the prompt is respected")]
    public void FixedExampleMatchesPrompt()
    {
        var rolls = new[] { 12, 8, 14, 15, 9 };
        var noArmor = new ArmorProfile(0, 0);

        var lightOutcome = ResolveAttack(rolls, DifficultyByWeapon[WeaponCategory.Light], noArmor);
        lightOutcome.Hits.Should().Be(3);
        lightOutcome.DamagePerHit.Should().BeEquivalentTo(new[] { 2, 4, 5 });
        lightOutcome.TotalDamage.Should().Be(11);

        var heavyOutcome = ResolveAttack(rolls, DifficultyByWeapon[WeaponCategory.Heavy], noArmor);
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
    }

    private static Dictionary<(WeaponCategory Weapon, ArmorCategory Armor), double> RunMatrix(int samples)
    {
        var rng = new Random(Seed);
        var output = new Dictionary<(WeaponCategory, ArmorCategory), double>();

        foreach (var weapon in WeaponsUnderTest)
        {
            foreach (var armor in ArmorsUnderTest)
            {
                double total = 0;
                var profile = ArmorProfiles[armor];

                for (var i = 0; i < samples; i++)
                {
                    total += ResolveAttack(RollDice(rng), DifficultyByWeapon[weapon], profile).TotalDamage;
                }

                output[(weapon, armor)] = total / samples;
            }
        }

        return output;
    }

    private static AttackOutcome ResolveAttack(IReadOnlyCollection<int> rolls, int difficulty, ArmorProfile armor)
    {
        var complexity = BaseComplexity + armor.DodgeBonus;
        var successes = rolls
            .Select(roll => roll - complexity)
            .Where(over => over >= 0)
            .OrderByDescending(over => over)
            .ToList();

        var hits = successes.Count / difficulty;
        var damages = new List<int>(hits);

        for (var i = 0; i < hits; i++)
        {
            // Group successes per difficulty, sum them, then apply block once per hit.
            var chunkDamage = successes.Skip(i * difficulty).Take(difficulty).Sum();
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

    private record AttackOutcome(int Hits, int TotalDamage, IReadOnlyList<int> DamagePerHit);
}
