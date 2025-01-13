using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Events.MinorSkills;

public class MinorSkillAdded
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public MinorSkillTemplateModel MinorSkill { get; set; }
}