namespace RoleRollsPocketEdition.Campaigns.Domain.Events.MinorSkills;

public class MinorSkillRemoved
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public Guid MinorSkillId { get; set; }
}