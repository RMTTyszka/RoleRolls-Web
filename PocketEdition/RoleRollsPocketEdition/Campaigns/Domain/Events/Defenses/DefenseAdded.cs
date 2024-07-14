using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Domain.Events.Defenses;

public class DefenseAdded
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public DefenseTemplateModel Defense { get; set; }
}