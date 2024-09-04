using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Domain.Campaigns.Events.Skills;

public class SkillAdded
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public SkillTemplateModel Skill { get; set; }
}