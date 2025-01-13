namespace RoleRollsPocketEdition.Campaigns.Events.Attributes;

public class AttributeRemoved
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public Guid AttributeId { get; set; }
}