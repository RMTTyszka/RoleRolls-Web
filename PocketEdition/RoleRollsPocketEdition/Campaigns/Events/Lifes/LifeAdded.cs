using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Events.Lifes;

public class LifeAdded
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public LifeTemplateModel Life { get; set; }
}