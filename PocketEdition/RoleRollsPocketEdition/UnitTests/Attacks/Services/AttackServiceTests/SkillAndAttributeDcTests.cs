using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RoleRollsPocketEdition.Rolls.Services;
using Xunit;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class SkillAndAttributeDcTests
{
    private const double TargetChance = 0.5;          // probabilidade alvo
    private const double MaxAllowedDelta = 0.1;       // tolerancia de ~50%
    private const int ComplexityMin = 5;              // faixa minima de CD
    private const int ComplexityMax = 22;             // faixa maxima de CD
    private const int Samples = 20000;                // amostras para simular sorte
    private const int Seed = 42;

    // Dados por nivel (mesmo esquema usado no balance de ataques):
    // atributo: base 3, +1 nos niveis 6, 11, 16
    // pericia: base atributo + (base 2, +1 nos niveis 4, 8, 12)
    private const int AttributeDiceBase = 3;
    private const int SkillDiceBase = 2;

    private readonly ITestOutputHelper _output;

    public SkillAndAttributeDcTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "CD por nível ~50% (atributo vs perícia)")]
    public void DcPerLevelStaysNearFiftyPercent()
    {
        foreach (var level in Enumerable.Range(1, 20))
        {
            var attributeDice = GetAttributeDiceForLevel(level);
            var skillDice = GetSkillDiceForLevel(level);

            var attributeDc = FindClosestDc(attributeDice, bonus: 0);
            var skillDc = FindClosestDc(skillDice, bonus: 0);

            attributeDc.Delta.Should().BeLessThanOrEqualTo(MaxAllowedDelta,
                $"Nivel {level}: atributo com CD {attributeDc.Complexity}/{attributeDc.Difficulty} ficou com chance {attributeDc.Chance:P2}, queria perto de 50%");
            skillDc.Delta.Should().BeLessThanOrEqualTo(MaxAllowedDelta,
                $"Nivel {level}: pericia com CD {skillDc.Complexity}/{skillDc.Difficulty} ficou com chance {skillDc.Chance:P2}, queria perto de 50%");

            _output.WriteLine(
                $"Level {level:00} | Attr CD {attributeDc.Complexity}/{attributeDc.Difficulty}: {attributeDc.Chance:P2} | Skill CD {skillDc.Complexity}/{skillDc.Difficulty}: {skillDc.Chance:P2}");
        }
    }

    [Fact(DisplayName = "Luck +1 aumenta chance perto da CD de 50% (atributo e perícia)")]
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
                $"Level {level:00} | Attr: base {attrBaseline.Chance:P2} -> luck {attrLuck:P2} (Δ {(attrLuck - attrBaseline.Chance):P2}) | Skill: base {skillBaseline.Chance:P2} -> luck {skillLuck:P2} (Δ {(skillLuck - skillBaseline.Chance):P2})");
        }

        var attrAvg = attrDeltas.DefaultIfEmpty(0).Average();
        var skillAvg = skillDeltas.DefaultIfEmpty(0).Average();
        _output.WriteLine($"Media dos deltas de sorte +1 | Attr: {attrAvg:P2} | Skill: {skillAvg:P2}");
    }

    [Fact(DisplayName = "Vantagem +1 dado aumenta chance perto da CD de 50%")]
    public void AdvantageImprovesSuccessRateNearFiftyPercent()
    {
        var attrDeltas = new List<double>();
        var skillDeltas = new List<double>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            var attrDice = GetAttributeDiceForLevel(level);
            var skillDice = GetSkillDiceForLevel(level);

            var attrBaseline = FindClosestDc(attrDice, bonus: 0);
            var attrAdv = SimulatedChance(attrDice + 1, bonus: 0, attrBaseline.Difficulty, attrBaseline.Complexity, luck: 0);
            attrAdv.Should().BeGreaterThan(attrBaseline.Chance, $"vantagem +1 dado deve ajudar atributo no nivel {level}");
            attrDeltas.Add(attrAdv - attrBaseline.Chance);

            var skillBaseline = FindClosestDc(skillDice, bonus: 0);
            var skillAdv = SimulatedChance(skillDice + 1, bonus: 0, skillBaseline.Difficulty, skillBaseline.Complexity, luck: 0);
            skillAdv.Should().BeGreaterThan(skillBaseline.Chance, $"vantagem +1 dado deve ajudar pericia no nivel {level}");
            skillDeltas.Add(skillAdv - skillBaseline.Chance);

            _output.WriteLine(
                $"Level {level:00} | Attr: base {attrBaseline.Chance:P2} -> vant {attrAdv:P2} (Δ {(attrAdv - attrBaseline.Chance):P2}) | Skill: base {skillBaseline.Chance:P2} -> vant {skillAdv:P2} (Δ {(skillAdv - skillBaseline.Chance):P2})");
        }

        var attrAvg = attrDeltas.DefaultIfEmpty(0).Average();
        var skillAvg = skillDeltas.DefaultIfEmpty(0).Average();
        _output.WriteLine($"Media dos deltas de vantagem (+1 dado) | Attr: {attrAvg:P2} | Skill: {skillAvg:P2}");
    }

    [Fact(DisplayName = "Desvantagem -1 dado reduz chance perto da CD de 50%")]
    public void DisadvantageReducesSuccessRateNearFiftyPercent()
    {
        var attrDeltas = new List<double>();
        var skillDeltas = new List<double>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            var attrDice = GetAttributeDiceForLevel(level);
            var skillDice = GetSkillDiceForLevel(level);

            var attrBaseline = FindClosestDc(attrDice, bonus: 0);
            var attrDis = SimulatedChance(Math.Max(attrDice - 1, 0), bonus: 0, attrBaseline.Difficulty, attrBaseline.Complexity, luck: 0);
            attrDis.Should().BeLessThan(attrBaseline.Chance, $"desvantagem -1 dado deve atrapalhar atributo no nivel {level}");
            attrDeltas.Add(attrBaseline.Chance - attrDis);

            var skillBaseline = FindClosestDc(skillDice, bonus: 0);
            var skillDis = SimulatedChance(Math.Max(skillDice - 1, 0), bonus: 0, skillBaseline.Difficulty, skillBaseline.Complexity, luck: 0);
            skillDis.Should().BeLessThan(skillBaseline.Chance, $"desvantagem -1 dado deve atrapalhar pericia no nivel {level}");
            skillDeltas.Add(skillBaseline.Chance - skillDis);

            _output.WriteLine(
                $"Level {level:00} | Attr: base {attrBaseline.Chance:P2} -> desvant {attrDis:P2} (Δ {(attrBaseline.Chance - attrDis):P2}) | Skill: base {skillBaseline.Chance:P2} -> desvant {skillDis:P2} (Δ {(skillBaseline.Chance - skillDis):P2})");
        }

        var attrAvg = attrDeltas.DefaultIfEmpty(0).Average();
        var skillAvg = skillDeltas.DefaultIfEmpty(0).Average();
        _output.WriteLine($"Media dos deltas de desvantagem (-1 dado) | Attr: {attrAvg:P2} | Skill: {skillAvg:P2}");
    }

    [Fact(DisplayName = "Luck -1 reduz chance perto da CD de 50% (atributo e perícia)")]
    public void LuckMinusOneReducesSuccessRateNearFiftyPercent()
    {
        var attrDeltas = new List<double>();
        var skillDeltas = new List<double>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            var attrDice = GetAttributeDiceForLevel(level);
            var skillDice = GetSkillDiceForLevel(level);

            var attrBaseline = FindClosestDc(attrDice, bonus: 0);
            var attrUnluck = SimulatedChance(attrDice, bonus: 0, attrBaseline.Difficulty, attrBaseline.Complexity, luck: -1);
            attrUnluck.Should().BeLessThan(attrBaseline.Chance, $"azar -1 deve atrapalhar atributo no nivel {level}");
            attrDeltas.Add(attrBaseline.Chance - attrUnluck);

            var skillBaseline = FindClosestDc(skillDice, bonus: 0);
            var skillUnluck = SimulatedChance(skillDice, bonus: 0, skillBaseline.Difficulty, skillBaseline.Complexity, luck: -1);
            skillUnluck.Should().BeLessThan(skillBaseline.Chance, $"azar -1 deve atrapalhar pericia no nivel {level}");
            skillDeltas.Add(skillBaseline.Chance - skillUnluck);

            _output.WriteLine(
                $"Level {level:00} | Attr: base {attrBaseline.Chance:P2} -> azar {attrUnluck:P2} (Δ {(attrBaseline.Chance - attrUnluck):P2}) | Skill: base {skillBaseline.Chance:P2} -> azar {skillUnluck:P2} (Δ {(skillBaseline.Chance - skillUnluck):P2})");
        }

        var attrAvg = attrDeltas.DefaultIfEmpty(0).Average();
        var skillAvg = skillDeltas.DefaultIfEmpty(0).Average();
        _output.WriteLine($"Media dos deltas de azar -1 | Attr: {attrAvg:P2} | Skill: {skillAvg:P2}");
    }

    private static DcResult FindClosestDc(int dice, int bonus)
    {
        var best = new DcResult(0, 0, 0, double.MaxValue);

        for (var difficulty = 1; difficulty <= dice; difficulty++)
        {
            for (var complexity = ComplexityMin; complexity <= ComplexityMax; complexity++)
            {
                var chance = SuccessChance(dice, difficulty, complexity, bonus);
                var delta = Math.Abs(chance - TargetChance);

                var isBetter = delta < best.Delta ||
                               (Math.Abs(delta - best.Delta) < 1e-9 &&
                                (complexity < best.Complexity ||
                                 (complexity == best.Complexity && difficulty < best.Difficulty)));

                if (isBetter)
                {
                    best = new DcResult(complexity, difficulty, chance, delta);
                }
            }
        }

        return best;
    }

    private static double SuccessChance(int dice, int difficulty, int complexity, int bonus)
    {
        var single = SingleDieSuccessChance(complexity, bonus);
        return ProbabilityAtLeast(dice, difficulty, single);
    }

    private static double SingleDieSuccessChance(int complexity, int bonus)
    {
        var threshold = complexity - bonus;
        if (threshold <= 1) return 1.0;
        if (threshold > 20) return 0.0;
        return (21 - threshold) / 20.0;
    }

    private static double SimulatedChance(int dice, int bonus, int difficulty, int complexity, int luck)
    {
        var rng = new Random(SeedFor(dice, bonus, difficulty, complexity, luck));
        var success = 0;

        for (var i = 0; i < Samples; i++)
        {
            var rolls = RollDice(rng, dice, luck, complexity);
            var successes = rolls.Count(r => r + bonus >= complexity);
            if (successes >= difficulty) success++;
        }

        return success / (double)Samples;
    }

    private static IReadOnlyList<int> RollDice(Random rng, int diceCount, int luck, int complexity)
    {
        var rolls = new int[diceCount];
        for (var i = 0; i < diceCount; i++)
        {
            rolls[i] = rng.Next(1, 21);
        }

        if (diceCount == 0 || luck == 0) return rolls;

        if (luck > 0)
        {
            // Sorte positiva: re-rolo o menor dado que falhou (exceto 1/20) e fico com o melhor.
            var candidateIndex = -1;
            var lowest = int.MaxValue;
            for (var i = 0; i < diceCount; i++)
            {
                var roll = rolls[i];
                if (roll >= complexity) continue;
                if (roll == 1 || roll == 20) continue;
                if (roll < lowest)
                {
                    lowest = roll;
                    candidateIndex = i;
                }
            }

            if (candidateIndex != -1)
            {
                var reroll = rng.Next(1, 21);
                rolls[candidateIndex] = Math.Max(rolls[candidateIndex], reroll);
            }
        }
        else
        {
            // Azar negativo: re-rolo o maior dado bem-sucedido (exceto 1/20) e fico com o pior.
            var candidateIndex = -1;
            var highest = int.MinValue;
            for (var i = 0; i < diceCount; i++)
            {
                var roll = rolls[i];
                if (roll < complexity) continue;
                if (roll == 1 || roll == 20) continue;
                if (roll > highest)
                {
                    highest = roll;
                    candidateIndex = i;
                }
            }

            if (candidateIndex != -1)
            {
                var reroll = rng.Next(1, 21);
                rolls[candidateIndex] = Math.Min(rolls[candidateIndex], reroll);
            }
        }

        return rolls;
    }

    private static int SeedFor(int dice, int bonus, int difficulty, int complexity, int luck) =>
        HashCode.Combine(dice, bonus, difficulty, complexity, luck, Seed);

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

    private static double ProbabilityAtLeast(int dice, int difficulty, double singleSuccess)
    {
        if (difficulty <= 0) return 1.0;
        if (difficulty > dice) return 0.0;

        double total = 0;
        for (var successes = difficulty; successes <= dice; successes++)
        {
            total += BinomialProbability(dice, successes, singleSuccess);
        }

        return total;
    }

    private static double BinomialProbability(int n, int k, double p)
    {
        if (p <= 0) return k == 0 ? 1.0 : 0.0;
        if (p >= 1) return k == n ? 1.0 : 0.0;

        var logCoeff = LogCombination(n, k);
        return Math.Exp(logCoeff + k * Math.Log(p) + (n - k) * Math.Log(1 - p));
    }

    private static double LogCombination(int n, int k)
    {
        if (k < 0 || k > n) return double.NegativeInfinity;
        k = Math.Min(k, n - k);

        double log = 0;
        for (var i = 1; i <= k; i++)
        {
            log += Math.Log(n - (k - i));
            log -= Math.Log(i);
        }

        return log;
    }

    private record DcResult(int Complexity, int Difficulty, double Chance, double Delta);
}
