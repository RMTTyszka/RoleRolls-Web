using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Rolls.Services;
using Xunit;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.UnitTests.Rolls;

/// <summary>
/// Replica as verificações de dificuldade/alvo para atributos e perícias usando o pipeline real de Roll.
/// </summary>
public class SkillAndAttributeRollTests
{
    private const double TargetChance = 0.5;
    private const double MaxAllowedDelta = 0.1;
    private const int ComplexityMin = 10;
    private const int ComplexityMax = 22;
    private const int Samples = 5000;
    private const int Seed = 123;

    private const int AttributeDiceBase = 3;
    private const int SkillDiceBase = 2;

    private readonly ITestOutputHelper _output;

    public SkillAndAttributeRollTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "CD por nível ~50% (atributo vs perícia) usando Roll")]
    public void DcPerLevelStaysNearFiftyPercent()
    {
        foreach (var level in Enumerable.Range(1, 20))
        {
            var attributeDice = GetAttributeDiceForLevel(level);
            var skillDice = GetSkillDiceForLevel(level);

            var attributeDc = FindClosestDc(attributeDice, bonus: 0, isSkillDice: false);
            var skillDc = FindClosestDc(skillDice, bonus: 0, isSkillDice: true);

            attributeDc.Delta.Should().BeLessThanOrEqualTo(MaxAllowedDelta,
                $"Nivel {level}: atributo com CD {attributeDc.Complexity}/{attributeDc.Difficulty} ficou com chance {attributeDc.Chance:P2}");
            skillDc.Delta.Should().BeLessThanOrEqualTo(MaxAllowedDelta,
                $"Nivel {level}: pericia com CD {skillDc.Complexity}/{skillDc.Difficulty} ficou com chance {skillDc.Chance:P2}");

            _output.WriteLine(
                $"Level {level:00} | Attr CD {attributeDc.Complexity}/{attributeDc.Difficulty}: {attributeDc.Chance:P2} | Skill CD {skillDc.Complexity}/{skillDc.Difficulty}: {skillDc.Chance:P2}");
        }
    }

    [Fact(DisplayName = "Luck +1 aumenta chance perto da CD de 50% (atributo e perícia) usando Roll")]
    public void LuckOneImprovesSuccessRateNearFiftyPercent()
    {
        var attrDeltas = new List<double>();
        var skillDeltas = new List<double>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            var attrDice = GetAttributeDiceForLevel(level);
            var skillDice = GetSkillDiceForLevel(level);

            var attrBaseline = FindClosestDc(attrDice, bonus: 0);
            var attrLuck = SimulatedChance(attrDice, bonus: 0, attrBaseline.Difficulty, attrBaseline.Complexity, luck: 1);
            attrLuck.Should().BeGreaterThan(attrBaseline.Chance, $"sorte +1 deve ajudar atributo no nivel {level}");
            attrDeltas.Add(attrLuck - attrBaseline.Chance);

            var skillBaseline = FindClosestDc(skillDice, bonus: 0);
            var skillLuck = SimulatedChance(skillDice, bonus: 0, skillBaseline.Difficulty, skillBaseline.Complexity, luck: 1);
            skillLuck.Should().BeGreaterThan(skillBaseline.Chance, $"sorte +1 deve ajudar pericia no nivel {level}");
            skillDeltas.Add(skillLuck - skillBaseline.Chance);

            _output.WriteLine(
                $"Level {level:00} | Attr: base {attrBaseline.Chance:P2} -> luck {attrLuck:P2} (\u0394 {(attrLuck - attrBaseline.Chance):P2}) | Skill: base {skillBaseline.Chance:P2} -> luck {skillLuck:P2} (\u0394 {(skillLuck - skillBaseline.Chance):P2})");
        }

        var attrAvg = attrDeltas.DefaultIfEmpty(0).Average();
        var skillAvg = skillDeltas.DefaultIfEmpty(0).Average();
        _output.WriteLine($"Media dos deltas de sorte +1 | Attr: {attrAvg:P2} | Skill: {skillAvg:P2}");
    }

    [Fact(DisplayName = "Vantagem +1 dado aumenta chance perto da CD de 50% usando Roll")]
    public void AdvantageImprovesSuccessRateNearFiftyPercent()
    {
        var attrDeltas = new List<double>();
        var skillDeltas = new List<double>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            var attrDice = GetAttributeDiceForLevel(level);
            var skillDice = GetSkillDiceForLevel(level);

            var attrBaseline = FindClosestDc(attrDice, bonus: 0);
            var attrAdv = SimulatedChance(attrDice, bonus: 0, attrBaseline.Difficulty, attrBaseline.Complexity, luck: 0, advantageDice: 1);
            attrAdv.Should().BeGreaterThan(attrBaseline.Chance, $"vantagem +1 dado deve ajudar atributo no nivel {level}");
            attrDeltas.Add(attrAdv - attrBaseline.Chance);

            var skillBaseline = FindClosestDc(skillDice, bonus: 0);
            var skillAdv = SimulatedChance(skillDice, bonus: 0, skillBaseline.Difficulty, skillBaseline.Complexity, luck: 0, advantageDice: 1);
            skillAdv.Should().BeGreaterThan(skillBaseline.Chance, $"vantagem +1 dado deve ajudar pericia no nivel {level}");
            skillDeltas.Add(skillAdv - skillBaseline.Chance);

            _output.WriteLine(
                $"Level {level:00} | Attr: base {attrBaseline.Chance:P2} -> vant {attrAdv:P2} (\u0394 {(attrAdv - attrBaseline.Chance):P2}) | Skill: base {skillBaseline.Chance:P2} -> vant {skillAdv:P2} (\u0394 {(skillAdv - skillBaseline.Chance):P2})");
        }

        var attrAvg = attrDeltas.DefaultIfEmpty(0).Average();
        var skillAvg = skillDeltas.DefaultIfEmpty(0).Average();
        _output.WriteLine($"Media dos deltas de vantagem (+1 dado) | Attr: {attrAvg:P2} | Skill: {skillAvg:P2}");
    }

    [Fact(DisplayName = "Desvantagem -1 dado reduz chance perto da CD de 50% usando Roll")]
    public void DisadvantageReducesSuccessRateNearFiftyPercent()
    {
        var attrDeltas = new List<double>();
        var skillDeltas = new List<double>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            var attrDice = GetAttributeDiceForLevel(level);
            var skillDice = GetSkillDiceForLevel(level);

            var attrBaseline = FindClosestDc(attrDice, bonus: 0);
            var attrDis = SimulatedChance(attrDice, bonus: 0, attrBaseline.Difficulty, attrBaseline.Complexity, luck: 0, advantageDice: -1);
            attrDis.Should().BeLessThan(attrBaseline.Chance, $"desvantagem -1 dado deve atrapalhar atributo no nivel {level}");
            attrDeltas.Add(attrBaseline.Chance - attrDis);

            var skillBaseline = FindClosestDc(skillDice, bonus: 0);
            var skillDis = SimulatedChance(skillDice, bonus: 0, skillBaseline.Difficulty, skillBaseline.Complexity, luck: 0, advantageDice: -1);
            skillDis.Should().BeLessThan(skillBaseline.Chance, $"desvantagem -1 dado deve atrapalhar pericia no nivel {level}");
            skillDeltas.Add(skillBaseline.Chance - skillDis);

            _output.WriteLine(
                $"Level {level:00} | Attr: base {attrBaseline.Chance:P2} -> desvant {attrDis:P2} (\u0394 {(attrBaseline.Chance - attrDis):P2}) | Skill: base {skillBaseline.Chance:P2} -> desvant {skillDis:P2} (\u0394 {(skillBaseline.Chance - skillDis):P2})");
        }

        var attrAvg = attrDeltas.DefaultIfEmpty(0).Average();
        var skillAvg = skillDeltas.DefaultIfEmpty(0).Average();
        _output.WriteLine($"Media dos deltas de desvantagem (-1 dado) | Attr: {attrAvg:P2} | Skill: {skillAvg:P2}");
    }

    private static DcResult FindClosestDc(int dice, int bonus, bool isSkillDice = false)
    {
        var best = new DcResult(0, 0, 0, double.MaxValue);

        var minDifficulty = GetMinDifficultyForDice(dice, isSkillDice);
        var minComplexity = Math.Max(ComplexityMin, 10);

        for (var difficulty = minDifficulty; difficulty <= dice; difficulty++)
        {
            for (var complexity = minComplexity; complexity <= ComplexityMax; complexity++)
            {
                var chance = SimulatedChance(dice, bonus, difficulty, complexity, luck: 0);
                var delta = Math.Abs(chance - TargetChance);

                var isBetter = delta < best.Delta ||
                               (Math.Abs(delta - best.Delta) < 1e-9 &&
                                (difficulty < best.Difficulty ||
                                 (difficulty == best.Difficulty && complexity < best.Complexity)));

                if (isBetter)
                {
                    best = new DcResult(complexity, difficulty, chance, delta);
                }
            }
        }

        return best;
    }

    private static double SimulatedChance(int dice, int bonus, int difficulty, int complexity, int luck, int advantageDice = 0)
    {
        var rng = new Random(SeedFor(dice, bonus, difficulty, complexity, luck, advantageDice));
        var success = 0;

        for (var i = 0; i < Samples; i++)
        {
            var command = new RollDiceCommand(
                propertyValue: dice,
                advantage: advantageDice,
                bonus: bonus,
                difficulty: difficulty,
                complexity: complexity,
                predefinedRolls: new List<int>(),
                luck: luck);

            var roll = new Roll(Guid.Empty, Guid.Empty, Guid.Empty, null, false, string.Empty);
            roll.Process(command, new RandomDiceRoller(rng.Next()), 20);

            if (roll.Success) success++;
        }

        return success / (double)Samples;
    }

    private static int SeedFor(int dice, int bonus, int difficulty, int complexity, int luck, int advantage) =>
        HashCode.Combine(dice, bonus, difficulty, complexity, luck, advantage, Seed);

    private static int GetAttributeDiceForLevel(int level)
    {
        var bonus = 0;
        if (level >= 6) bonus++;
        if (level >= 11) bonus++;
        if (level >= 16) bonus++;
        return AttributeDiceBase + bonus;
    }

    private static int GetSkillDiceForLevel(int level)
    {
        var attr = GetAttributeDiceForLevel(level);
        var skill = SkillDiceBase + (level >= 4 ? 1 : 0) + (level >= 8 ? 1 : 0) + (level >= 12 ? 1 : 0);
        return attr + skill;
    }

    private static int GetMinDifficultyForDice(int dice, bool isSkillDice)
    {
        var level = FindLevelForDiceTotal(dice, isSkillDice);
        var bonus = 1;
        if (level >= 6) bonus++;
        if (level >= 11) bonus++;
        if (level >= 16) bonus++;
        return Math.Min(dice, bonus);
    }

    private static int FindLevelForDiceTotal(int totalDice, bool isSkillDice)
    {
        for (var level = 1; level <= 20; level++)
        {
            var candidate = isSkillDice ? GetSkillDiceForLevel(level) : GetAttributeDiceForLevel(level);
            if (candidate == totalDice) return level;
        }

        return 20;
    }

    private class RandomDiceRoller : IDiceRoller
    {
        private readonly Random _random;

        public RandomDiceRoller(int seed)
        {
            _random = new Random(seed);
        }

        public int Roll(int sides) => _random.Next(1, sides + 1);

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

    private record DcResult(int Complexity, int Difficulty, double Chance, double Delta);
}
