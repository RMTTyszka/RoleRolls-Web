using RoleRollsPocketEdition.Rolls.Domain.Commands;
using RoleRollsPocketEdition.Rolls.Domain.Entities;

namespace RoleRollsPocketEdition.Rolls.Domain.Services;

public class CdDiscoveryService : ICdDiscoveryService
{

        private readonly int _difficultRange = 50;
        private readonly int _complexityRange = 10;
        private readonly decimal _numberOfRolls = 10000;
        private readonly int _standardDeviation = 5;

        public List<CdDiscoveryResult> GetDc(int points, int bonus, decimal chance) {

            var results = new List<CdDiscoveryResult>();
            for (var difficulty = 1; difficulty < _difficultRange; difficulty++) {
                for (var complexity = 1; complexity < _complexityRange; complexity++) {
                    var currentSuccesses = 0;
                    for (var rolls = 0; rolls < _numberOfRolls; rolls++) {
                        var success = GetChance(points, bonus, difficulty, complexity);
                        if (success) {
                            currentSuccesses++;
                        }
                    }
                    var currentChance = currentSuccesses / _numberOfRolls * 100;
                    if (currentChance - chance >= -_standardDeviation && currentChance - chance <= _standardDeviation) {
                        results.Add((CdDiscoveryResult)new CdDiscoveryResult(difficulty, complexity, currentChance));
                    }
                }
            }

            return results;
        }
        public decimal VerifyDc(int points, int bonus, int difficulty, int complexity) {
            var chance = 0m;

            var roller = new Roll();
            for (var rolls = 0; rolls < _numberOfRolls; rolls++)
            {
                var command = new RollDiceCommand(points, 0, bonus, difficulty, complexity, new List<int>());
                chance += roller.Process(command).Success ? 1 : 0;
            }

            return chance / _numberOfRolls * 100;
        }

        private bool GetChance(int points, int bonus, int difficulty, int complexity) {
            var command = new RollDiceCommand(points, 0, bonus, difficulty, complexity, new List<int>());
            var roller = new Roll();
            var roll = roller.Process(command);
            return roll.Success;
        }


}