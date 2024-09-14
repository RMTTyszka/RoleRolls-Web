using RoleRollsPocketEdition._Domain.Powers.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Campaigns.Entities
{
    public class Campaign : Entity
    {
        public Guid MasterId { get; set; }
        public string Name { get; set; }
        public Guid CreatureTemplateId { get; set; }
        public Guid InvitationSecret { get; set; }
        public ICollection<PowerTemplate> PowerTemplates { get; set; } = new List<PowerTemplate>();
    }
}
