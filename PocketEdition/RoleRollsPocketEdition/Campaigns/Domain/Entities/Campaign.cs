using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Campaigns.Domain
{
    public class Campaign : Entity
    {
        public Guid MasterId { get; set; }
        public string Name { get; set; }
        public Guid CreatureTemplateId { get; set; }
        public Guid InvitationSecret { get; set; }
    }
}
