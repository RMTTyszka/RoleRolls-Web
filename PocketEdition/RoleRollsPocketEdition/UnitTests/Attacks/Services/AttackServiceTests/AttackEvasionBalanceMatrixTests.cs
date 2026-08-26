using System.Collections.Concurrent;
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

public class AttackEvasionBalanceMatrixTests
{
    private const int SamplesPerLevel = 10_000;
    private const int Seed = 4242;
    private static readonly WeaponCategory[] WeaponCategories =
        [WeaponCategory.Light, WeaponCategory.Medium, WeaponCategory.Heavy];
    private static readonly ArmorCategory[] ArmorCategories =
        [ArmorCategory.Light, ArmorCategory.Medium, ArmorCategory.Heavy];
    private readonly ITestOutputHelper _output;

    public AttackEvasionBalanceMatrixTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "Matriz compara Attack e Evasion pelo runtime")]
    public void MatrixKeepsAttackAndEvasionHitChancesBalanced()
    {
        var rows = BuildComparisonMatrix();

        rows.Should().HaveCount(20 * 2 * WeaponCategories.Length * ArmorCategories.Length);
        rows.Select(row => (row.Weapon, row.Armor)).Should().Equal(
            WeaponCategories.SelectMany(weapon => ArmorCategories.SelectMany(armor =>
                Enumerable.Repeat((weapon, armor), 20 * 2))));

        LogComparisonMatrix(rows);

        foreach (var attackRow in rows.Where(row => row.PlayerAction == "Attack"))
        {
            var evasionRow = rows.Single(row =>
                row.Weapon == attackRow.Weapon &&
                row.Armor == attackRow.Armor &&
                row.Level == attackRow.Level &&
                row.PlayerAction == "Evasion");

            attackRow.Profile.HitChance.Should().BeApproximately(
                evasionRow.Profile.HitChance,
                0.03,
                $"Attack e Evasion devem ter a mesma chance de acerto com arma {attackRow.Weapon}, " +
                $"armadura {attackRow.Armor}, no nível {attackRow.Level}");
        }
    }

    private List<ComparisonRow> BuildComparisonMatrix()
    {
        var rows = new ConcurrentBag<ComparisonRow>();
        var categories = WeaponCategories.SelectMany(weapon =>
            ArmorCategories.Select(armor => (Weapon: weapon, Armor: armor)));

        Parallel.ForEach(categories, category =>
        {
            foreach (var level in Enumerable.Range(1, 20))
            {
                rows.Add(new ComparisonRow(
                    level,
                    category.Weapon,
                    category.Armor,
                    "Attack",
                    SimulateAttack(level, category.Weapon, category.Armor)));
                rows.Add(new ComparisonRow(
                    level,
                    category.Weapon,
                    category.Armor,
                    "Evasion",
                    SimulateEvasion(level, category.Weapon, category.Armor)));
            }
        });

        return rows
            .OrderBy(row => Array.IndexOf(WeaponCategories, row.Weapon))
            .ThenBy(row => Array.IndexOf(ArmorCategories, row.Armor))
            .ThenBy(row => row.Level)
            .ThenBy(row => row.PlayerAction)
            .ToList();
    }

    private static RuntimeProfile SimulateAttack(int level, WeaponCategory weapon, ArmorCategory armor)
    {
        var attacker = BuildCreature("attacker", level, weapon, ArmorCategory.Medium);
        var defender = BuildCreature("defender", level, WeaponCategory.Medium, armor);
        var command = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = LandOfHeroesTemplate.Template.ItemConfiguration
        };
        var diceRoller = new RandomDiceRoller(Seed + level + (int)weapon * 100 + (int)armor * 1_000);
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

    private static RuntimeProfile SimulateEvasion(int level, WeaponCategory weapon, ArmorCategory armor)
    {
        var attacker = BuildCreature("attacker", level, weapon, ArmorCategory.Medium);
        var defender = BuildCreature("defender", level, WeaponCategory.Medium, armor);
        var command = new EvadeCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = LandOfHeroesTemplate.Template.ItemConfiguration
        };
        var diceRoller = new RandomDiceRoller(Seed + level + (int)weapon * 100 + (int)armor * 1_000);
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

    private static Creature BuildCreature(string name, int level, WeaponCategory weapon, ArmorCategory armor) =>
        new BaseCreature(LandOfHeroesTemplate.Template, name)
            .WithLevel(level)
            .WithWeapon(weapon, EquipableSlot.MainHand, level)
            .WithArmor(armor, level)
            .Creature;

    private void LogComparisonMatrix(IEnumerable<ComparisonRow> rows)
    {
        foreach (var categoryRows in rows.GroupBy(row => (row.Weapon, row.Armor)))
        {
            _output.WriteLine($"=== Weapon: {categoryRows.Key.Weapon} | Armor: {categoryRows.Key.Armor} ===");
            _output.WriteLine("Level | Attack                                   | Evasion");

            foreach (var levelRows in categoryRows.GroupBy(row => row.Level))
            {
                var attack = levelRows.Single(row => row.PlayerAction == "Attack");
                var evasion = levelRows.Single(row => row.PlayerAction == "Evasion");
                _output.WriteLine(
                    $"L{levelRows.Key:00}   | {FormatProfile(attack.Profile, "hit"),-40} | " +
                    FormatProfile(evasion.Profile, "NPC hit"));
            }

            _output.WriteLine(string.Empty);
        }
    }

    private static string FormatProfile(RuntimeProfile profile, string hitLabel) =>
        $"{profile.BaseDice}d20+{profile.Bonus} vs {profile.Difficulty} ({hitLabel} {profile.HitChance:P1})";

    private readonly record struct RuntimeProfile(
        int BaseDice,
        int Bonus,
        int Difficulty,
        double HitChance);

    private readonly record struct ComparisonRow(
        int Level,
        WeaponCategory Weapon,
        ArmorCategory Armor,
        string PlayerAction,
        RuntimeProfile Profile);

    private sealed class RandomDiceRoller(int seed) : IDiceRoller
    {
        private readonly Random _random = new(seed);

        public int Roll(int size) => _random.Next(1, size + 1);

        public int[] RollMany(int size, int times) =>
            Enumerable.Range(0, times).Select(_ => Roll(size)).ToArray();
    }
}
