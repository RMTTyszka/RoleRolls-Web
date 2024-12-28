using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;

namespace RoleRollsPocketEdition._Domain.Campaigns
{
    public interface ICampaignRepository
    {
        Task<CampaignTemplate> GetCreatureTemplateAggregateAsync(Guid id);
        Task SaveChangesAsync();
    }
}