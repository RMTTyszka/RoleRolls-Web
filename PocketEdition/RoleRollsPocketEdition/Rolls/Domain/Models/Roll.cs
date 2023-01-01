using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Campaigns.Domain.Entities
{
    public class RollModel
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public Guid ActorId { get; set; }
        public string ActorName { get; set; }

        public string RolledDices { get; set; }
        public int NumberOfDices { get; set; }

        public Guid PropertyId { get; set; }
        public RollPropertyType PropertyType { get; set; }
        public string PropertyName { get; set; }

        public int NumberOfSuccesses { get; set; }
        public int NumberOfCriticalSuccesses { get; set; }
        public int NumberOfCriticalFailures { get; set; }
        public int Difficulty { get; set; }
        public int Complexity { get; set; }
        public bool Success { get; set; }

        public RollModel(Roll roll)
        {
            Id = roll.Id;
            CampaignId = roll.CampaignId;
            ActorId = roll.ActorId;
            ActorName = roll.ActorName;
            RolledDices = roll.RolledDices;
            NumberOfDices = roll.NumberOfDices;
            PropertyId = roll.PropertyId;
            PropertyType = roll.PropertyType;
            PropertyName = roll.PropertyName;
            NumberOfSuccesses = roll.NumberOfSuccesses;
            NumberOfCriticalSuccesses = roll.NumberOfCriticalSuccesses;
            NumberOfCriticalFailures = roll.NumberOfCriticalFailures;
            Difficulty = roll.Difficulty;
            Complexity = roll.Complexity;
            Success = roll.Success;
        }
    }
}
