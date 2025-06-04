namespace RoleRollsPocketEdition.Rolls.Commands
{
    public class RollDiceCommand
    {
        public RollDiceCommand(int propertyValue, int advantage, int bonus, int difficulty, int complexity, List<int> predefinedRolls, int luck)
        {
            PropertyValue = propertyValue;
            Bonus = bonus;
            Advantage = advantage;
            Difficulty = difficulty;
            Complexity = complexity;
            PredefinedRolls = predefinedRolls;
            Luck = luck;
        }

        public int Luck { get; set; }

        public int PropertyValue { get; init; } 
        public int Bonus { get; init; } 
        public int Advantage { get; init; } 
        public int Difficulty { get; init; }
        public int Complexity { get; init; }
        public List<int> PredefinedRolls { get; init; }
    }
}
