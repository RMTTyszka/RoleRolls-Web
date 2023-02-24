using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Rolls.Domain.Commands;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoleRollsPocketEdition.Campaigns.Domain.Entities
{
    public class Roll : Entity
    {
        public Guid CampaignId { get; set; }
        public Guid SceneId { get; set; }
        public Guid? ActorId { get; set; }
        public string RolledDices { get; set; }
        public int NumberOfDices { get; set; }
        public int RollBonus { get; set; }

        public Guid PropertyId { get; set; }
        public RollPropertyType PropertyType { get; set; }
        public int NumberOfSuccesses { get; set; }
        public int NumberOfCriticalSuccesses { get; set; }
        public int NumberOfCriticalFailures { get; set; }
        public int Difficulty { get; set; }
        public int Complexity { get; set; }
        public bool Success { get; set; }

        public bool Hidden { get; set; }
        public DateTime DateTime { get; set; }

        public Roll()
        {
        }
        public Roll(Guid campaignId, Guid sceneId, Guid? actorId, Guid propertyId, RollPropertyType propertyType, bool hidden)
        {
            CampaignId = campaignId;
            ActorId = actorId;
            Hidden = hidden;
            SceneId = sceneId;
            PropertyId = propertyId;
            PropertyType = propertyType;
        }

        public Roll Process(RollDiceCommand command)
        {
            NumberOfDices = command.PredefinedRolls.Any() ? command.PredefinedRolls.Count() : command.PropertyValue;
            Complexity = command.Complexity;
            Difficulty = command.Difficulty;
            RollBonus = command.RollBonus;
            var rolls = new List<int>();
            foreach (var rollIndex in Enumerable.Range( 0, NumberOfDices))
            {
                var roll = command.PredefinedRolls.Any() ? command.PredefinedRolls[rollIndex] : RollDice();
                rolls.Add(roll);
                var rollWithBonus = roll += RollBonus;
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
