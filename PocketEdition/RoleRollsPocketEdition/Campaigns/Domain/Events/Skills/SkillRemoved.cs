namespace RoleRollsPocketEdition.Campaigns.Domain.Events.Skills;

public class SkillRemoved
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public Guid SkillId { get; set; }
}