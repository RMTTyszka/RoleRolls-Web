using System;
using System.Threading.Tasks;
using RoleRollsPocketEdition.CreaturesTemplates.Entities;

namespace RoleRollsPocketEdition.Campaigns.Domain
{
    public interface ICampaignRepository
    {
        Task<CreatureTemplate> GetCreatureTemplateAggregateAsync(Guid id);
        Task SaveChangesAsync();
    }
}