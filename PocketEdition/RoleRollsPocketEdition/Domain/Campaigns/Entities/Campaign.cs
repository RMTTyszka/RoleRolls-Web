using RoleRollsPocketEdition.Global;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Domain.Campaigns.Entities
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
