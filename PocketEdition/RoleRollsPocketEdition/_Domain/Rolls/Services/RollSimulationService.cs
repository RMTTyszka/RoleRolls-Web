using RoleRollsPocketEdition._Domain.Rolls.Commands;
using RoleRollsPocketEdition._Domain.Rolls.Entities;

namespace RoleRollsPocketEdition._Domain.Rolls.Services;

public class RollSimulationService : IRollSimulationService
{
    private const int ComplexityRange = 50;
    private const int DifficultyRange = 10;
    private const decimal NumberOfRolls = 10000;
    private const int StandardDeviation = 10;
    private readonly ILogger<RollSimulationService> _logger;

    public RollSimulationService(ILogger<RollSimulationService> logger)
    {
        _logger = logger;
    }

    public List<CdSimulationResult> GetDc(int points, int bonus, decimal chance) {
        var results = new List<CdSimulationResult>();
        var adjustedDifficultRange = Math.Min(DifficultyRange, points);
        var adjustedComplexityRange = Math.Min(ComplexityRange, 20 + bonus);
        for (var difficulty = 1; difficulty < adjustedDifficultRange; difficulty++) {
            for (var complexity = 1; complexity < adjustedComplexityRange; complexity++) {
                var currentSuccesses = 0;
                for (var rolls = 0; rolls < NumberOfRolls; rolls++) {
                    var success = getChance(points, bonus, difficulty, complexity);
                    if (success) {
                        currentSuccesses++;
                    }
                }
                var currentChance = currentSuccesses / NumberOfRolls * 100;
                var comparedChance = currentChance - chance;
                if (comparedChance > 0)
                {
                    _logger.LogInformation($"{complexity} / {difficulty} = {currentChance}");
                }
                if (comparedChance >= -StandardDeviation && comparedChance <= StandardDeviation) {
                    if (comparedChance > 0)
                    {
                        results.Add(new CdSimulationResult(difficulty, complexity, currentChance));
                    }
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
            var rollCommand = new RollDiceCommand(points, 0, bonus, difficulty, complexity, new List<int>());
            chance += roll.Process(rollCommand).Success ? 1 : 0;
        }

        return chance / NumberOfRolls * 100;
    }
    private bool getChance(int points, int bonus, int difficulty, int complexity) {
        var rollCommand = new RollDiceCommand(points, 0, bonus, difficulty, complexity, new List<int>());
        var roll = new Roll().Process(rollCommand);
        return roll.Success;
    }
}