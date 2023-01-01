using RoleRollsPocketEdition.Campaigns.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Rolls.Domain.Commands
{
    public class RollDiceCommand
    {
        public RollDiceCommand(int propertyValue, int rollBonus, int propertyBonus, int difficulty, int complexity)
        {
            PropertyValue = propertyValue;
            RollBonus = rollBonus;
            PropertyBonus = propertyBonus;
            Difficulty = difficulty;
            Complexity = complexity;
        }

        public int PropertyValue { get; init; } 
        public int RollBonus { get; init; } 
        public int PropertyBonus { get; init; } 
        public int Difficulty { get; init; }
        public int Complexity { get; init; }
    }
}
