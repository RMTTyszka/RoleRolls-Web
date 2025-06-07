using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Events.MinorSkills;

public class MinorSkillUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public SpecificSkillTemplateModel SpecificSkill { get; set; }
}