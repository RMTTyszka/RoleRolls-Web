using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Events.Attributes;

public class AttributeAdded
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public AttributeTemplateModel Attribute { get; set; }
}