using RoleRollsPocketEdition.Campaigns.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Rolls.Domain.Commands
{
    public class RollDiceCommand
    {
        public RollDiceCommand(int propertyValue, int propertyBonus, int rollBonus, int difficulty, int complexity, List<int> predefinedRolls)
        {
            PropertyValue = propertyValue;
            RollBonus = rollBonus;
            PropertyBonus = propertyBonus;
            Difficulty = difficulty;
            Complexity = complexity;
            PredefinedRolls = predefinedRolls;
        }

        public int PropertyValue { get; init; } 
        public int RollBonus { get; init; } 
        public int PropertyBonus { get; init; } 
        public int Difficulty { get; init; }
        public int Complexity { get; init; }
        public List<int> PredefinedRolls { get; init; }
    }
}
