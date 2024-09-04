using RoleRollsPocketEdition.Domain.CreatureTemplates.Entities;

namespace RoleRollsPocketEdition.Domain.Campaigns
{
    public interface ICampaignRepository
    {
        Task<CreatureTemplate> GetCreatureTemplateAggregateAsync(Guid id);
        Task SaveChangesAsync();
    }
}