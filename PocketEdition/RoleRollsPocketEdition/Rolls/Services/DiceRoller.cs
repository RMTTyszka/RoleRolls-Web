using RoleRollsPocketEdition.Core.Abstractions;

namespace RoleRollsPocketEdition.Rolls.Services;

public interface IDiceRoller
{
    int Roll(int sides);
    int[] RollMany(int sides, int times);
}

public class DiceRoller : IDiceRoller, ITransientDependency
{
    private readonly Random _random = new();

    public int Roll(int sides)
    {
        return _random.Next(1, sides + 1);
    }

    public int[] RollMany(int sides, int times)
    {
        var result = new int[times];
        for (int i = 0; i < times; i++)
        {
            result[i] = Roll(sides);
        }

        return result;
    }
}