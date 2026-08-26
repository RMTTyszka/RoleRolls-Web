using System.Text.Json;
using FluentAssertions;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class AttackEvasionComparisonTests
{
    private const int SamplesPerLevel = 10_000;
    private const int Seed = 4242;
    private readonly ITestOutputHelper _output;

    public AttackEvasionComparisonTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "Matriz compara Attack e Evasion pelo runtime")]
    public void MatrixKeepsAttackAndEvasionHitChancesBalanced()
    {
        var rows = BuildComparisonMatrix();

        rows.Should().HaveCount(20 * 2);

        foreach (var row in rows)
        {
            _output.WriteLine(row.Format());
        }

        foreach (var attackRow in rows.Where(row => row.PlayerAction == "Attack"))
        {
            var evasionRow = rows.Single(row =>
                row.Level == attackRow.Level && row.PlayerAction == "Evasion");

            attackRow.Profile.HitChance.Should().BeApproximately(
                evasionRow.Profile.HitChance,
                0.03,
                $"Attack e Evasion devem ter a mesma chance de acerto no nível {attackRow.Level}");
        }
    }

    private List<ComparisonRow> BuildComparisonMatrix()
    {
        var rows = new List<ComparisonRow>();
        foreach (var level in Enumerable.Range(1, 20))
        {
            rows.Add(new ComparisonRow(level, "Attack", SimulateAttack(level)));
            rows.Add(new ComparisonRow(level, "Evasion", SimulateEvasion(level)));
        }

        return rows;
    }

    private static RuntimeProfile SimulateAttack(int level)
    {
        var attacker = BuildCreature("attacker", level);
        var defender = BuildCreature("defender", level);
        var command = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = LandOfHeroesTemplate.Template.ItemConfiguration
        };
        var diceRoller = new RandomDiceRoller(Seed + level);
        BasicAttackResult? firstResult = null;
        var hits = 0;

        for (var sample = 0; sample < SamplesPerLevel; sample++)
        {
            var result = attacker.BasicAttack(defender, command, diceRoller);
            firstResult ??= result;
            hits += result.Success ? 1 : 0;
            defender.FullRestore();
        }

        var firstRolls = JsonSerializer.Deserialize<List<int>>(firstResult!.RolledDices)!;
        return new RuntimeProfile(firstRolls.Count, firstResult.Bonus, firstResult.Complexity,
            hits / (double)SamplesPerLevel);
    }

    private static RuntimeProfile SimulateEvasion(int level)
    {
        var attacker = BuildCreature("attacker", level);
        var defender = BuildCreature("defender", level);
        var command = new EvadeCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = LandOfHeroesTemplate.Template.ItemConfiguration
        };
        var diceRoller = new RandomDiceRoller(Seed + level);
        EvadeResult? firstResult = null;
        var hits = 0;

        for (var sample = 0; sample < SamplesPerLevel; sample++)
        {
            var result = defender.Evade(attacker, command, diceRoller);
            firstResult ??= result;
            hits += result.Success ? 0 : 1;
            defender.FullRestore();
        }

        return new RuntimeProfile(firstResult!.BaseDice, firstResult.EvadeBonus, firstResult.Difficulty,
            hits / (double)SamplesPerLevel);
    }

    private static Creature BuildCreature(string name, int level) =>
        new BaseCreature(LandOfHeroesTemplate.Template, name)
            .WithLevel(level)
            .WithWeapon(WeaponCategory.Medium, EquipableSlot.MainHand, level)
            .WithArmor(ArmorCategory.Medium, level)
            .Creature;

    private readonly record struct RuntimeProfile(
        int BaseDice,
        int Bonus,
        int Difficulty,
        double HitChance);

    private readonly record struct ComparisonRow(int Level, string PlayerAction, RuntimeProfile Profile)
    {
        public string Format()
        {
            var hitLabel = PlayerAction == "Evasion" ? "NPC hit" : "hit";
            return $"{PlayerAction} | L{Level:00} | Medium/Medium | " +
                   $"{Profile.BaseDice}d20+{Profile.Bonus} vs {Profile.Difficulty} " +
                   $"({hitLabel} {Profile.HitChance:P1})";
        }
    }

    private sealed class RandomDiceRoller(int seed) : IDiceRoller
    {
        private readonly Random _random = new(seed);

        public int Roll(int size) => _random.Next(1, size + 1);

        public int[] RollMany(int size, int times) =>
            Enumerable.Range(0, times).Select(_ => Roll(size)).ToArray();
    }
}
