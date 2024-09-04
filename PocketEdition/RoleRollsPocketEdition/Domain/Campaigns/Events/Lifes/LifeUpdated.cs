using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Domain.Campaigns.Events.Lifes;

public class LifeUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public LifeTemplateModel Life { get; set; }
}