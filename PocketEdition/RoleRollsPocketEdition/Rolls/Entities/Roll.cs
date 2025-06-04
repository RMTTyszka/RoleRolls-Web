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

        public Property? Property { get; set; }
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
        public Roll(Guid campaignId, Guid sceneId, Guid actorId, Property? property,
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
            NumberOfDices = command.PredefinedRolls.Any()
                ? command.PredefinedRolls.Count
                : command.PropertyValue + command.Advantage;
            Complexity = command.Complexity;
            Difficulty = command.Difficulty;
            Bonus = command.Bonus;

            var baseRolls = RollAllDice(command);
            ApplyLuck(ref baseRolls, command.Luck);
            ProcessRolls(baseRolls);

            RolledDices = JsonSerializer.Serialize(baseRolls.Select(r => r + Bonus).ToList());
            this.DateTime = DateTime.UtcNow;
            return this;
        }

        private List<int> RollAllDice(RollDiceCommand command)
        {
            var rolls = new List<int>();
            for (int i = 0; i < NumberOfDices; i++)
            {
                int roll = command.PredefinedRolls.Any() ? command.PredefinedRolls[i] : RollDice();
                rolls.Add(roll);
            }
            return rolls;
        }

        private void ApplyLuck(ref List<int> rolls, int luck)
        {
            if (luck == 0) return;

            var indicesToReroll = rolls
                .Select((value, index) => new { value, index })
                .OrderBy(pair => luck > 0 ? pair.value : -pair.value)
                .Take(Math.Abs(luck))
                .Select(pair => pair.index)
                .ToList();

            foreach (int index in indicesToReroll)
            {
                var newRoll = RollDice();
                rolls[index] = luck > 0
                    ? Math.Max(rolls[index], newRoll)
                    : Math.Min(rolls[index], newRoll);
            }
        }

        private void ProcessRolls(List<int> baseRolls)
        {
            foreach (var roll in baseRolls)
            {
                int rollWithBonus = roll + Bonus;

                if (rollWithBonus >= Complexity)
                {
                    NumberOfSuccesses++;
                    if (roll == 20)
                        NumberOfCriticalSuccesses++;
                }
                else
                {
                    if (roll == 1)
                        NumberOfCriticalFailures++;
                }
            }

            Success = NumberOfSuccesses >= Difficulty;
        }



        private int RollDice()
        {
            var randomMaker = new Random();
            var roll = randomMaker.Next(20) + 1;
            return roll;
        }
    }
}
