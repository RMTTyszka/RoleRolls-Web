using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Models
{
    public class CampaignModel
    {
        public Guid Id { get; set; }
        public Guid MasterId { get; set; }
        public Guid? CampaignTemplateId { get; set; }
        public CampaignTemplateModel? CampaignTemplate { get; set; }
        public string Name { get; set; }

        public CampaignModel()
        {
                
        }
        public CampaignModel(Campaign campaign)
        {
            Id = campaign.Id;
            MasterId = campaign.MasterId;
            Name = campaign.Name;
            CampaignTemplateId = campaign.CampaignTemplateId;
            CampaignTemplate = CampaignTemplateModel.FromTemplate(campaign.CampaignTemplate);
        }

    }
}
