using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Domain.Campaigns.Entities;
using RoleRollsPocketEdition.Domain.CreatureTemplates.Entities;

namespace RoleRollsPocketEdition.Domain.Campaigns.Models
{
    public class CampaignModel
    {
        public Guid Id { get; set; }
        public Guid MasterId { get; set; }
        public Guid? CreatureTemplateId { get; set; }

        public CreatureTemplateModel? CreatureTemplate { get; set; }
        public string Name { get; set; }

        public CampaignModel()
        {
                
        }
        public CampaignModel(Campaign campaign, CreatureTemplate creatureTemplate)
        {
            Id = campaign.Id;
            MasterId = campaign.MasterId;
            Name = campaign.Name;
            CreatureTemplateId = campaign.CreatureTemplateId;
            if (creatureTemplate is not null)
            {
                CreatureTemplate = new CreatureTemplateModel(creatureTemplate);
            }
        }

    }
}
