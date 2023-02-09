using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Campaigns.Domain.Entities
{
    public class CampaignScene : Entity
    {
        public Guid CampaignId { get; set; }
        public string Name { get; set; }
    }
}
