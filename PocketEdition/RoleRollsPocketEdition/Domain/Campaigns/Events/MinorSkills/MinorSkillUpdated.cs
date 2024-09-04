using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Domain.Campaigns.Events.MinorSkills;

public class MinorSkillUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public MinorSkillTemplateModel MinorSkill { get; set; }
}