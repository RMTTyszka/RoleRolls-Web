using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.CreaturesTemplates.Domain.Templates;

namespace RoleRollsPocketEdition.Campaigns.Domain
{
    public interface ICampaignRepository
    {
        Task<CreatureTemplate> GetCreatureTemplateAggregateAsync(Guid id);
        Task SaveChangesAsync();
    }
}