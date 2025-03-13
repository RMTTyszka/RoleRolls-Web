using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Events.Vitalities;

public class VitalityAdded
{
    public Guid CampaignId { get; set; }
    public Guid CreatureTemplateId { get; set; }
    public VitalityTemplateModel Vitality { get; set; }
}