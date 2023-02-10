using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Campaigns.Domain.Entities
{
    public class Scene : Entity
    {
        public Guid CampaignId { get; set; }
        public string Name { get; set; }

        public ICollection<Creature> Heroes { get; set; }
    }
}
