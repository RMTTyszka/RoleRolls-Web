using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition._Domain.Itens.Configurations;

namespace RoleRollsPocketEdition._Domain.Campaigns.Models
{
    public class CampaignModel
    {
        public Guid Id { get; set; }
        public Guid MasterId { get; set; }
        public Guid? CampaignTemplateId { get; set; }

        public CampaignTemplateModel? CreatureTemplate { get; set; }
        public ItemConfigurationModel? ItemConfiguration { get; set; }
        public string Name { get; set; }

        public CampaignModel()
        {
                
        }
        public CampaignModel(Campaign campaign)
        {
            Id = campaign.Id;
            MasterId = campaign.MasterId;
            Name = campaign.Name;
            CampaignTemplateId = campaign.CreatureTemplateId;
            CreatureTemplate = CampaignTemplateModel.FromTemplate(campaign.CampaignTemplate);
            ItemConfiguration = ItemConfigurationModel.FromConfiguration(campaign.ItemConfiguration);
        }

    }
}
