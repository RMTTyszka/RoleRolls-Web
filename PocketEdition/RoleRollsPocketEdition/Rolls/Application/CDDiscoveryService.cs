using RoleRollsPocketEdition.Rolls.Domain.Commands;
using RoleRollsPocketEdition.Rolls.Domain.Entities;
using RoleRollsPocketEdition.Rolls.Domain.Models;

namespace RoleRollsPocketEdition.Rolls.Application;

public class CdDiscoveryService : ICdDiscoveryService
{

        private Roll roller = new Roll();
        private int difficultRange = 50;
        private int complexityRange = 10;
        private decimal numberOfRolls = 10000;
        private int standardDeviation = 5;

        public List<CdDiscoveryResult> GetDc(int points, int bonus, decimal chance) {

            var results = new List<CdDiscoveryResult>();
            for (var difficulty = 1; difficulty < difficultRange; difficulty++) {
                for (var complexity = 1; complexity < complexityRange; complexity++) {
                    var currentSuccesses = 0;
                    for (var rolls = 0; rolls < numberOfRolls; rolls++) {
                        var success = GetChance(points, bonus, difficulty, complexity);
                        if (success) {
                            currentSuccesses++;
                        }
                    }
                    var currentChance = currentSuccesses / numberOfRolls * 100;
                    if (currentChance - chance >= -standardDeviation && currentChance - chance <= standardDeviation) {
                        results.Add((CdDiscoveryResult)new CdDiscoveryResult(difficulty, complexity, currentChance));
                    }
                }
            }

            return results;
        }
        public decimal VerifyDc(int points, int bonus, int difficulty, int complexity) {
            var chance = 0m;

            for (var rolls = 0; rolls < numberOfRolls; rolls++)
            {
                var command = new RollDiceCommand(points, 0, bonus, difficulty, complexity, new List<int>());
                chance += roller.Process(command).Success ? 1 : 0;
            }

            return chance / numberOfRolls * 100;
        }

        private bool GetChance(int points, int bonus, int difficulty, int complexity) {
            var command = new RollDiceCommand(points, 0, bonus, difficulty, complexity, new List<int>());
            var roll = roller.Process(command);
            return roll.Success;
        }


}