using FluentAssertions;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class CombatSimulationTests
{
    private const int SimulationsPerMatchup = 100;
    private const int Seed = 7_381;
    private const int MaximumRoundsPerCombat = 10_000;

    private static readonly WeaponCategory[] CreatureAWeapons =
        [WeaponCategory.Light, WeaponCategory.Medium, WeaponCategory.Heavy];

    private static readonly ArmorCategory[] CreatureBArmors =
        [ArmorCategory.Light, ArmorCategory.Medium, ArmorCategory.Heavy];

    private readonly ITestOutputHelper _output;

    public CombatSimulationTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory(DisplayName = "Simula 100 combates por combinacao de arma e armadura")]
    [InlineData(1)]
    public void SimulatesEquipmentMatchupsAtLevel(int level)
    {
        level.Should().BeGreaterThan(0);

        var matchups = CreatureAWeapons
            .SelectMany(creatureAWeapon => CreatureBArmors.Select(creatureBArmor =>
                SimulateMatchup(level, creatureAWeapon, creatureBArmor)))
            .ToList();

        matchups.Should().HaveCount(CreatureAWeapons.Length * CreatureBArmors.Length);
        matchups.Should().OnlyContain(matchup => matchup.Simulations == SimulationsPerMatchup);
        matchups.Should().OnlyContain(matchup => matchup.AverageRoundsUntilDeath > 0);
        matchups.Should().OnlyContain(matchup => matchup.CreatureAWins + matchup.CreatureBWins == SimulationsPerMatchup);

        LogMatchups(level, matchups);
    }

    private CombatMatchupResult SimulateMatchup(
        int level,
        WeaponCategory creatureAWeapon,
        ArmorCategory creatureBArmor)
    {
        var diceRoller = new RandomDiceRoller(Seed + level * 10_000 + (int)creatureAWeapon * 100 + (int)creatureBArmor);
        var creatureATotalDamage = 0;
        var creatureBTotalDamage = 0;
        var totalRoundsUntilDeath = 0;
        var creatureAWins = 0;
        var creatureBWins = 0;

        for (var simulation = 0; simulation < SimulationsPerMatchup; simulation++)
        {
            var creatureA = BuildCreature("A", level, creatureAWeapon, ArmorCategory.Light);
            var creatureB = BuildCreature("B", level, WeaponCategory.Medium, creatureBArmor);
            var creatureAActsFirst = CreatureAActsFirst(diceRoller);
            var roundsUntilDeath = 0;

            while (!HasDied(creatureA) && !HasDied(creatureB))
            {
                roundsUntilDeath++;
                if (roundsUntilDeath > MaximumRoundsPerCombat)
                {
                    throw new InvalidOperationException(
                        $"Combat did not finish after {MaximumRoundsPerCombat} rounds at level {level}. " +
                        $"A weapon: {creatureAWeapon}; B armor: {creatureBArmor}.");
                }

                if (creatureAActsFirst)
                {
                    creatureATotalDamage += Attack(creatureA, creatureB, diceRoller);
                    if (!HasDied(creatureB))
                    {
                        creatureBTotalDamage += Attack(creatureB, creatureA, diceRoller);
                    }
                }
                else
                {
                    creatureBTotalDamage += Attack(creatureB, creatureA, diceRoller);
                    if (!HasDied(creatureA))
                    {
                        creatureATotalDamage += Attack(creatureA, creatureB, diceRoller);
                    }
                }
            }

            totalRoundsUntilDeath += roundsUntilDeath;
            if (HasDied(creatureB))
            {
                creatureAWins++;
            }
            else
            {
                creatureBWins++;
            }
        }

        return new CombatMatchupResult(
            creatureAWeapon,
            creatureBArmor,
            SimulationsPerMatchup,
            creatureATotalDamage / (double)totalRoundsUntilDeath,
            creatureBTotalDamage / (double)totalRoundsUntilDeath,
            totalRoundsUntilDeath / (double)SimulationsPerMatchup,
            creatureAWins,
            creatureBWins);
    }

    private static Creature BuildCreature(
        string name,
        int level,
        WeaponCategory weapon,
        ArmorCategory armor) =>
        new BaseCreature(LandOfHeroesTemplate.Template, name)
            .WithLevel(level)
            .WithWeapon(weapon, EquipableSlot.MainHand, level)
            .WithArmor(armor, level)
            .Creature;

    private static int Attack(Creature attacker, Creature target, IDiceRoller diceRoller)
    {
        var command = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = LandOfHeroesTemplate.Template.ItemConfiguration
        };

        return attacker.BasicAttack(target, command, diceRoller).TotalDamage;
    }

    private static bool CreatureAActsFirst(IDiceRoller diceRoller)
    {
        while (true)
        {
            var creatureAInitiative = diceRoller.Roll(20);
            var creatureBInitiative = diceRoller.Roll(20);
            if (creatureAInitiative != creatureBInitiative)
            {
                return creatureAInitiative > creatureBInitiative;
            }
        }
    }

    private static bool HasDied(Creature creature) =>
        creature.Vitalities.Single(vitality =>
            vitality.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life]).Value == 0;

    private void LogMatchups(int level, IEnumerable<CombatMatchupResult> matchups)
    {
        _output.WriteLine($"=== Combat simulation | Level {level} | {SimulationsPerMatchup} fights per matchup ===");
        _output.WriteLine("A weapon | B armor | A damage/round | B damage/round | rounds until death | A wins | B wins");

        foreach (var matchup in matchups)
        {
            _output.WriteLine(
                $"{matchup.CreatureAWeapon,-8} | {matchup.CreatureBArmor,-8} | " +
                $"{matchup.CreatureAAverageDamagePerRound,14:F2} | " +
                $"{matchup.CreatureBAverageDamagePerRound,14:F2} | " +
                $"{matchup.AverageRoundsUntilDeath,18:F2} | " +
                $"{matchup.CreatureAWins,6} | {matchup.CreatureBWins,6}");
        }
    }

    private readonly record struct CombatMatchupResult(
        WeaponCategory CreatureAWeapon,
        ArmorCategory CreatureBArmor,
        int Simulations,
        double CreatureAAverageDamagePerRound,
        double CreatureBAverageDamagePerRound,
        double AverageRoundsUntilDeath,
        int CreatureAWins,
        int CreatureBWins);

    private sealed class RandomDiceRoller(int seed) : IDiceRoller
    {
        private readonly Random _random = new(seed);

        public int Roll(int size) => _random.Next(1, size + 1);

        public int[] RollMany(int size, int times) =>
            Enumerable.Range(0, times).Select(_ => Roll(size)).ToArray();
    }
}
