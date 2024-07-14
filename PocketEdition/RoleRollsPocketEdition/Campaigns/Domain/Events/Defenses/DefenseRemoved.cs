namespace RoleRollsPocketEdition.Campaigns.Domain.Events.Defenses;

public class DefenseRemoved
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public Guid DefenseId { get; set; }
}