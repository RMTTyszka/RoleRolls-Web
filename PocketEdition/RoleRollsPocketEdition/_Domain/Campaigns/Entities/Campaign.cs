using System.ComponentModel.DataAnnotations.Schema;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition._Domain.Itens.Configurations;
using RoleRollsPocketEdition._Domain.Powers.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Campaigns.Entities
{
    public class Campaign : Entity
    {
        public Guid MasterId { get; set; }
        public string Name { get; set; }
        public Guid CreatureTemplateId { get; set; }
        public CreatureTemplate CreatureTemplate { get; set; }
        public Guid InvitationSecret { get; set; }
        public ICollection<PowerTemplate> PowerTemplates { get; set; } = new List<PowerTemplate>();
        public ICollection<CampaignPlayer> CampaignPlayers { get; set; } = new List<CampaignPlayer>();
        public ICollection<Scene> Scenes { get; set; } = new List<Scene>();
        public ItemConfiguration ItemConfiguration { get; set; }
    }
}
