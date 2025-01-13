using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Campaigns
{
    public interface ICampaignRepository
    {
        Task<CampaignTemplate> GetCreatureTemplateAggregateAsync(Guid id);
        Task SaveChangesAsync();
    }
}