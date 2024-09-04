namespace RoleRollsPocketEdition.Domain.Campaigns.Events.Lifes;

public class LifeRemoved
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public Guid LifeId { get; set; }
}