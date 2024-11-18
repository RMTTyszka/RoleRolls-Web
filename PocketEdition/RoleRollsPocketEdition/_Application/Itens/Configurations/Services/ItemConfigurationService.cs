using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Domain.Itens.Configurations;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Itens.Configurations.Services;

public class ItemConfigurationService : IItemConfigurationService, ITransientDependency
{
    private readonly RoleRollsDbContext _dbContext;

    public ItemConfigurationService(RoleRollsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ItemConfigurationModel> GetItemConfiguration(Guid campaignId)
    {
        var itemConfiguration = await _dbContext.ItemConfigurations
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.CampaignId == campaignId);
        var output = ItemConfigurationModel.FromConfiguration(itemConfiguration);
        return output;
    }   
    public async Task Update(Guid campaignId, ItemConfigurationModel model)
    {
        var itemConfiguration = await _dbContext.ItemConfigurations
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.CampaignId == campaignId);
        if (itemConfiguration == null)
        {
            itemConfiguration = new ItemConfiguration
            {
                CampaignId = campaignId,
            };
            await _dbContext.ItemConfigurations.AddAsync(itemConfiguration);
            await _dbContext.SaveChangesAsync();
        }
        itemConfiguration.Update(model);
        _dbContext.ItemConfigurations.Update(itemConfiguration);
        await _dbContext.SaveChangesAsync();
    }
}