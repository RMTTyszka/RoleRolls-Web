using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Rolls.Models
{
    public class RollModel
    {
        public Guid Id { get; set; }
        public Guid? ActorId { get; set; }
        public string ActorName { get; set; }

        public string RolledDices { get; set; }
        public int NumberOfDices { get; set; }

        public string? PropertyName { get; set; }

        public int NumberOfSuccesses { get; set; }
        public int NumberOfCriticalSuccesses { get; set; }
        public int NumberOfCriticalFailures { get; set; }
        public int Difficulty { get; set; }
        public int Complexity { get; set; }
        public bool Success { get; set; }
        public Guid SceneId { get; set; }
        public int Bonus { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public int Advantage { get; set; }


        public RollModel(Roll roll)
        {
            Id = roll.Id;
            SceneId = roll.SceneId;
            ActorId = roll.ActorId;
            RolledDices = roll.RolledDices;
            NumberOfDices = roll.NumberOfDices;
            Property = roll.Property;
            NumberOfSuccesses = roll.NumberOfSuccesses;
            NumberOfCriticalSuccesses = roll.NumberOfCriticalSuccesses;
            NumberOfCriticalFailures = roll.NumberOfCriticalFailures;
            Difficulty = roll.Difficulty;
            Complexity = roll.Complexity;
            Success = roll.Success;
            Bonus = roll.Bonus;
            DateTime = roll.DateTime;
            Description = roll.Description;
            Advantage = roll.Advantage;
        }

        public Property Property { get; set; }

        public RollModel()
        {
        }
    }
}


