using System.Text.Json;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Scenes.Entities;

namespace RoleRollsPocketEdition.Rolls.Entities
{
    public class Roll : Entity
    {
        public Guid SceneId { get; set; }
        public Scene Scene { get; set; }
        public Guid ActorId { get; set; }
        public ActionActorType ActorType { get; set; }
        public string RolledDices { get; set; }
        public int NumberOfDices { get; set; }
        public int Bonus { get; set; }

        public Property Property { get; set; }
        public int NumberOfSuccesses { get; set; }
        public int NumberOfCriticalSuccesses { get; set; }
        public int NumberOfCriticalFailures { get; set; }
        public int Difficulty { get; set; }
        public int Complexity { get; set; }
        public bool Success { get; set; }

        public bool Hidden { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public int Advantage { get; set; }

        public Roll()
        {
        }
        public Roll(Guid campaignId, Guid sceneId, Guid actorId, Property property,
            bool hidden, string description)
        {
            Id = Guid.NewGuid();
            ActorId = actorId;
            Hidden = hidden;
            SceneId = sceneId;
            Property = property;
            Description = description;
        }

        public Roll Process(RollDiceCommand command)
        {
            NumberOfDices = command.PredefinedRolls.Any() ? command.PredefinedRolls.Count() : command.PropertyValue;
            Complexity = command.Complexity;
            Difficulty = command.Difficulty;
            Bonus = command.RollBonus;
            var rolls = new List<int>();
            foreach (var rollIndex in Enumerable.Range( 0, NumberOfDices))
            {
                var roll = command.PredefinedRolls.Any() ? command.PredefinedRolls[rollIndex] : RollDice();
                rolls.Add(roll);
                var rollWithBonus = roll += Bonus;
                if (rollWithBonus >= Complexity)
                {
                    NumberOfSuccesses++;
                    if (roll == 20)
                    {
                        NumberOfCriticalSuccesses++;
                    }
                } else 
                {
                    if (roll == 1) 
                    {
                        NumberOfCriticalFailures++;
                    }
                }
            }
            if (NumberOfSuccesses >= Difficulty)
            {
                Success = true;
            }
            RolledDices = JsonSerializer.Serialize(rolls);
            this.DateTime = DateTime.UtcNow;
            return this;
        }

        private int RollDice()
        {
            var randomMaker = new Random();
            var roll = randomMaker.Next(20) + 1;
            return roll;
        }
    }
}
