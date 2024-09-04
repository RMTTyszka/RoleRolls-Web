using RoleRollsPocketEdition.Domain.Rolls.Entities;

namespace RoleRollsPocketEdition.Domain.Rolls.Models
{
    public class RollModel
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public Guid? ActorId { get; set; }
        public string ActorName { get; set; }

        public string RolledDices { get; set; }
        public int NumberOfDices { get; set; }

        public Guid PropertyId { get; set; }
        public RollPropertyType PropertyType { get; set; }
        public string? PropertyName { get; set; }

        public int NumberOfSuccesses { get; set; }
        public int NumberOfCriticalSuccesses { get; set; }
        public int NumberOfCriticalFailures { get; set; }
        public int Difficulty { get; set; }
        public int Complexity { get; set; }
        public bool Success { get; set; }
        public Guid SceneId { get; set; }
        public int RollBonus { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }

        public RollModel(Roll roll)
        {
            Id = roll.Id;
            CampaignId = roll.CampaignId;
            SceneId = roll.SceneId;
            ActorId = roll.ActorId;
            RolledDices = roll.RolledDices;
            NumberOfDices = roll.NumberOfDices;
            PropertyId = roll.PropertyId;
            PropertyType = roll.PropertyType;
            NumberOfSuccesses = roll.NumberOfSuccesses;
            NumberOfCriticalSuccesses = roll.NumberOfCriticalSuccesses;
            NumberOfCriticalFailures = roll.NumberOfCriticalFailures;
            Difficulty = roll.Difficulty;
            Complexity = roll.Complexity;
            Success = roll.Success;
            RollBonus = roll.RollBonus;
            DateTime = roll.DateTime;
            Description = roll.Description;
        }

        public RollModel()
        {
        }
    }
}
