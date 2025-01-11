namespace RoleRollsPocketEdition._Domain.Campaigns.Models;

public class CampaignCreateInput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? MasterId { get; set; }
    public Guid? CampaignTemplateId { get; set; }
}