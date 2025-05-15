namespace RoleRollsPocketEdition.Campaigns.Models;

public class CampaignCreateInput
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid? MasterId { get; set; }
    public Guid? CampaignTemplateId { get; set; }
    public bool IsTemplate { get; set; }
}
public class CampaignUpdateInput
{
    public string? Name { get; set; }
}