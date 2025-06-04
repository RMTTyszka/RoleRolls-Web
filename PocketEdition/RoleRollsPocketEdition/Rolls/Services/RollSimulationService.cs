using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Rolls.Services;

public class RollSimulationService : IRollSimulationService
{
    private const int ComplexityRange = 50;
    private const int DifficultyRange = 10;
    private const int NumberOfRolls = 10000;
    private const int StandardDeviation = 10;
    private readonly ILogger<RollSimulationService> _logger;

    public RollSimulationService(ILogger<RollSimulationService> logger)
    {
        _logger = logger;
    }

    public List<CdSimulationResult> GetDc(int points, int bonus, decimal chance) 
{
    var results = new List<CdSimulationResult>();
    var adjustedDifficultRange = Math.Min(DifficultyRange, points);
    var adjustedComplexityRange = Math.Min(ComplexityRange, 20 + bonus);

    // Pre-allocate capacity to avoid multiple resizes
    results.Capacity = adjustedDifficultRange * adjustedComplexityRange;

    for (var difficulty = 1; difficulty < adjustedDifficultRange; difficulty++) 
    {
        for (var complexity = 1; complexity < adjustedComplexityRange; complexity++) 
        {
            long currentSuccesses = 0;


            Parallel.For(0, NumberOfRolls, 
                () => 0L,
                (rolls, loopState, localSuccesses) => 
                {
                    if (getChance(points, bonus, difficulty, complexity)) 
                    {
                        localSuccesses++;
                    }
                    return localSuccesses;
                },
                localSuccesses => Interlocked.Add(ref currentSuccesses, localSuccesses)
            );

            var currentChance = (decimal)currentSuccesses / NumberOfRolls * 100;
            var comparedChance = currentChance - chance;

            if (comparedChance >= -StandardDeviation && comparedChance <= StandardDeviation) 
            {
                results.Add(new CdSimulationResult(difficulty, complexity, currentChance));
                _logger.LogInformation($"{complexity} / {difficulty} = {currentChance}");
            }
        }
    }

    return results.OrderBy(c => c.Chance).ToList();
}

    public decimal VerifyDc(int points, int bonus, int difficulty, int complexity) {
        var chance = 0m;

        for (var rolls = 0; rolls < NumberOfRolls; rolls++)
        {
            var roll = new Roll();
            var rollCommand = new RollDiceCommand(points, 0, bonus, difficulty, complexity, new List<int>(), 0);
            chance += roll.Process(rollCommand).Success ? 1 : 0;
        }

        return chance / NumberOfRolls * 100;
    }
    private bool getChance(int points, int bonus, int difficulty, int complexity) {
        var rollCommand = new RollDiceCommand(points, 0, bonus, difficulty, complexity, new List<int>(), 0);
        var roll = new Roll().Process(rollCommand);
        return roll.Success;
    }
}