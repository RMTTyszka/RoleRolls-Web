using System.Text.Json;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Services;
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
        public int NumberOfRollSuccesses { get; set; }
        public int NumberOfCriticalSuccesses { get; set; }
        public int NumberOfCriticalFailures { get; set; }
        public int Difficulty { get; set; }
        public int Complexity { get; set; }
        public bool Success { get; set; }

        public bool Hidden { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public int Advantage { get; set; }
        public int Luck { get; set; }

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

        public Roll Process(RollDiceCommand command, IDiceRoller diceRoller, int sizes)
        {
            NumberOfDices = command.PredefinedRolls.Any()
                ? command.PredefinedRolls.Count
                : command.PropertyValue + command.Advantage;
            Complexity = command.Complexity;
            Difficulty = command.Difficulty;
            Luck = command.Luck;
            Advantage = command.Advantage;
            Bonus = command.Bonus;

            var baseRolls = RollAllDice(command, diceRoller, sizes);
            ApplyLuck(ref baseRolls, command.Luck, diceRoller, sizes);
            ProcessRolls(baseRolls);

            RolledDices = JsonSerializer.Serialize(baseRolls.Select(r => r + Bonus).ToList());
            this.DateTime = DateTime.UtcNow;
            return this;
        }

        private List<int> RollAllDice(RollDiceCommand command, IDiceRoller diceRoller, int sizes)
        {
            var rolls = new List<int>();
            for (int i = 0; i < NumberOfDices; i++)
            {
                int roll = command.PredefinedRolls.Any() ? command.PredefinedRolls[i] : RollDice(diceRoller, sizes);
                rolls.Add(roll);
            }
            return rolls;
        }

        private void ApplyLuck(ref List<int> rolls, int luck, IDiceRoller diceRoller, int sizes)
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
                var newRoll = RollDice(diceRoller, sizes);
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
            NumberOfRollSuccesses = NumberOfSuccesses / Difficulty;
        }



        private int RollDice(IDiceRoller diceRoller, int sizes)
        {
            var roll = diceRoller.Roll(sizes);
            return roll;
        }
    }
}


