using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Campaigns.Domain.Entities
{
    public class CampaignPlayer : Entity
    {
        public Guid CampaignId { get; set; }
        public Guid PlayerId { get; set; }
    }
}
