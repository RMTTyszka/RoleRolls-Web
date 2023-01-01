using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Rolls.Domain.Commands;

namespace RoleRollsPocketEdition.Campaigns.Domain.Entities
{
    public class Roll : Entity
    {
        public Guid CampaignId { get; set; }
        public Guid ActorId { get; set; }
        public string RolledDices { get; set; }
        public int NumberOfDices { get; set; }

        public Guid PropertyId { get; set; }
        public RollPropertyType PropertyType { get; set; }
        public int NumberOfSuccesses { get; set; }
        public int NumberOfCriticalSuccesses { get; set; }
        public int NumberOfCriticalFailures { get; set; }
        public int Difficulty { get; set; }
        public int Complexity { get; set; }
        public bool Success { get; set; }

        public bool Hidden { get; set; }

        public Roll()
        {
        }
        public Roll(Guid campaignId, Guid actorId, bool hidden)
        {
            CampaignId = campaignId;
            ActorId = actorId;
            Hidden = hidden;
        }

        public Roll Process(RollDiceCommand command)
        {
            return this;
        }

    }
}
