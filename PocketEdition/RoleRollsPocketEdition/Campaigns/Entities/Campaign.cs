using RoleRollsPocketEdition.Campaigns.Models;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Campaigns.Entities
{
    public class Campaign : Entity
    {
        public Guid MasterId { get; set; }
        public string Name { get; set; }
        public Guid CampaignTemplateId { get; set; }
        public CampaignTemplate CampaignTemplate { get; set; }
        public Guid InvitationSecret { get; set; }
        public ICollection<PowerTemplate> PowerTemplates { get; set; } = new List<PowerTemplate>();
        public ICollection<CampaignPlayer> CampaignPlayers { get; set; } = new List<CampaignPlayer>();
        public ICollection<Scene> Scenes { get; set; } = new List<Scene>();
        
        public Campaign()
        {
            
        }
        public Campaign(CampaignCreateInput campaignModel)
        {
            Id = campaignModel.Id;
            MasterId = campaignModel.MasterId.GetValueOrDefault();
            Name = campaignModel.Name;
            CampaignTemplateId = campaignModel.CampaignTemplateId ?? Guid.NewGuid();
            CampaignTemplate = null;
            InvitationSecret = Guid.NewGuid();
            PowerTemplates = new List<PowerTemplate>();
            CampaignPlayers = new List<CampaignPlayer>();
            Scenes = new List<Scene>();
            CampaignTemplate = new CampaignTemplate(campaignModel);
        }
        
    }
}
