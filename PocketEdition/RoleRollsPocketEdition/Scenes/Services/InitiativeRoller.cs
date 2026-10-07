using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Rolls.Services;

namespace RoleRollsPocketEdition.Scenes.Services;

public sealed record InitiativeRoll(
    Guid CreatureId,
    int PropertyValue,
    int Score,
    IReadOnlyList<int> DiceResults);

public interface IInitiativeRoller
{
    InitiativeRoll Roll(Guid creatureId, int propertyValue);
    IReadOnlyList<InitiativeRoll> Order(IEnumerable<InitiativeRoll> rolls);
}

public sealed class InitiativeRoller : IInitiativeRoller, ITransientDependency
{
    private const int DiceSides = 20;
    private readonly IDiceRoller _diceRoller;

    public InitiativeRoller(IDiceRoller diceRoller)
    {
        _diceRoller = diceRoller;
    }

    public InitiativeRoll Roll(Guid creatureId, int propertyValue)
    {
        var diceResults = _diceRoller.RollMany(DiceSides, GetDiceCount(propertyValue));
        return new InitiativeRoll(creatureId, propertyValue, diceResults.Max(), diceResults);
    }

    public IReadOnlyList<InitiativeRoll> Order(IEnumerable<InitiativeRoll> rolls)
    {
        return rolls
            .GroupBy(roll => roll.Score)
            .OrderByDescending(group => group.Key)
            .SelectMany(OrderScoreGroup)
            .ToList();
    }

    private IEnumerable<InitiativeRoll> OrderScoreGroup(IGrouping<int, InitiativeRoll> scoreGroup)
    {
        return scoreGroup
            .GroupBy(roll => roll.PropertyValue)
            .OrderByDescending(group => group.Key)
            .SelectMany(group => ResolveRemainingTie(group.ToList()));
    }

    private IReadOnlyList<InitiativeRoll> ResolveRemainingTie(IReadOnlyList<InitiativeRoll> tiedRolls)
    {
        if (tiedRolls.Count <= 1)
        {
            return tiedRolls;
        }

        var ordered = new List<InitiativeRoll>();
        var rerolledGroups = tiedRolls
            .Select(roll => new
            {
                Roll = roll,
                TieBreakerScore = _diceRoller.RollMany(DiceSides, GetDiceCount(roll.PropertyValue)).Max()
            })
            .GroupBy(result => result.TieBreakerScore)
            .OrderByDescending(group => group.Key);

        foreach (var rerolledGroup in rerolledGroups)
        {
            var candidates = rerolledGroup.Select(result => result.Roll).ToList();
            ordered.AddRange(ResolveRemainingTie(candidates));
        }

        return ordered;
    }

    private static int GetDiceCount(int propertyValue) => propertyValue < 0 ? 1 : propertyValue + 1;
}
