using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Campaigns.Domain
{
    public interface ICampaignRepository
    {
        Task<CreatureTemplate> GetCreatureTemplateAggregateAsync(Guid id);
    }
}