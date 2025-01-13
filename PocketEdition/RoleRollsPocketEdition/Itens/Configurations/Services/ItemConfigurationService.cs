using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Itens.Configurations.Services;

public class ItemConfigurationService : IItemConfigurationService, ITransientDependency
{
    private readonly RoleRollsDbContext _dbContext;

    public ItemConfigurationService(RoleRollsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ItemConfigurationModel> GetItemConfiguration(Guid campaignId)
    {
        var itemConfiguration = await _dbContext.Campaigns
            .Include(e => e.CampaignTemplate)
            .ThenInclude(e => e.ItemConfiguration).Where(e => e.Id == campaignId)
            .Select(e => e.CampaignTemplate.ItemConfiguration)
            .FirstAsync();
        var output = ItemConfigurationModel.FromConfiguration(itemConfiguration);
        return output;
    }   
    public async Task Update(Guid campaignId, ItemConfigurationModel model)
    {
        var itemConfiguration = await _dbContext.Campaigns
            .Include(e => e.CampaignTemplate)
            .ThenInclude(e => e.ItemConfiguration).Where(e => e.Id == campaignId)
            .Select(e => e.CampaignTemplate.ItemConfiguration)
            .FirstAsync();
        itemConfiguration.Update(model);
        _dbContext.ItemConfigurations.Update(itemConfiguration);
        await _dbContext.SaveChangesAsync();
    }
}