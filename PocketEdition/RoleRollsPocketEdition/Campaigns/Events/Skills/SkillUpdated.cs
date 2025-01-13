using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Events.Skills;

public class SkillUpdated
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public SkillTemplateModel Skill { get; set; }
}