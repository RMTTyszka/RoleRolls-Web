using FluentAssertions;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.Scenes.Services;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Scenes.Services;

public class InitiativeRollerTests
{
    [Fact]
    public void RollUsesOnePlusPropertyDiceAndKeepsHighestScore()
    {
        var dice = new QueueDiceRoller([4, 18, 11]);
        var roller = new InitiativeRoller(dice);

        var result = roller.Roll(Guid.NewGuid(), propertyValue: 2);

        result.DiceResults.Should().Equal(4, 18, 11);
        result.Score.Should().Be(18);
        dice.RollManyCalls.Should().ContainSingle().Which.Should().Be((20, 3));
    }

    [Fact]
    public void RollUsesAtLeastOneDieWhenPropertyIsNegative()
    {
        var dice = new QueueDiceRoller([12]);
        var roller = new InitiativeRoller(dice);

        var result = roller.Roll(Guid.NewGuid(), propertyValue: -2);

        result.Score.Should().Be(12);
        dice.RollManyCalls.Should().ContainSingle().Which.Should().Be((20, 1));
    }

    [Fact]
    public void OrderUsesHighestScoreBeforePropertyValue()
    {
        var dice = new QueueDiceRoller([]);
        var roller = new InitiativeRoller(dice);
        var highestScore = Guid.NewGuid();
        var highestProperty = Guid.NewGuid();
        var lowestProperty = Guid.NewGuid();

        var result = roller.Order(
        [
            new InitiativeRoll(highestProperty, 4, 16, []),
            new InitiativeRoll(lowestProperty, 1, 16, []),
            new InitiativeRoll(highestScore, 0, 17, [])
        ]);

        result.Select(entry => entry.CreatureId).Should().Equal(highestScore, highestProperty, lowestProperty);
        dice.RollManyCalls.Should().BeEmpty();
    }

    [Fact]
    public void OrderRerollsOnlyCreaturesStillTiedAndKeepsOriginalScore()
    {
        var dice = new QueueDiceRoller(
        [
            6, 1, 2, 4, 1, 2,       // first tiebreaker: first creature wins
            15, 1, 1, 2, 1, 1, 2, 1, 1, // second: third creature wins; other two tie
            8, 1, 1, 12, 1, 1       // final two creatures: fifth wins
        ]);
        var roller = new InitiativeRoller(dice);
        var first = Guid.NewGuid();
        var second = Guid.NewGuid();
        var third = Guid.NewGuid();
        var fourth = Guid.NewGuid();
        var fifth = Guid.NewGuid();

        var result = roller.Order(
        [
            new InitiativeRoll(first, 2, 13, []),
            new InitiativeRoll(second, 2, 13, []),
            new InitiativeRoll(third, 2, 12, []),
            new InitiativeRoll(fourth, 2, 12, []),
            new InitiativeRoll(fifth, 2, 12, [])
        ]);

        result.Select(entry => entry.CreatureId).Should().Equal(first, second, third, fifth, fourth);
        result.Select(entry => entry.Score).Should().Equal(13, 13, 12, 12, 12);
        dice.RollManyCalls.Should().OnlyContain(call => call.Sides == 20 && call.Times == 3);
        dice.RollManyCalls.Should().HaveCount(7);
    }

    private sealed class QueueDiceRoller(IEnumerable<int> rolls) : IDiceRoller
    {
        private readonly Queue<int> _rolls = new(rolls);

        public List<(int Sides, int Times)> RollManyCalls { get; } = [];

        public int Roll(int sides) => _rolls.Dequeue();

        public int[] RollMany(int sides, int times)
        {
            RollManyCalls.Add((sides, times));
            return Enumerable.Range(0, times).Select(_ => Roll(sides)).ToArray();
        }
    }
}
