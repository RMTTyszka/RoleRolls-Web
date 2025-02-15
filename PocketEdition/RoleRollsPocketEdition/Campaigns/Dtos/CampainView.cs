using RoleRollsPocketEdition.Campaigns.Entities;

namespace RoleRollsPocketEdition.Campaigns.Dtos;

public class CampainView(Campaign campaign)
{
    public Guid Id { get; set; } = campaign.Id;
    public string Name { get; set; } = campaign.Name;
    public Guid MasterId { get; set; } = campaign.MasterId;
    public string TemplateName { get; set; } = campaign.CampaignTemplate.Name;
}