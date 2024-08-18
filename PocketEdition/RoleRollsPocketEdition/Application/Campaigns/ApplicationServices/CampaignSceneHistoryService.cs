using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Host.Campaigns.Dtos;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Host.Campaigns.ApplicationServices;

public interface ICampaignSceneHistoryService
{
    Task<List<CampaignSceneHistoryOutput>> GetList(Guid campaignId, Guid sceneId, GetSceneHistoryInput input);
    DateTime? GetLastHistoryTime(Guid campaignId, Guid sceneId);
}

public class CampaignSceneHistoryService : ICampaignSceneHistoryService, ITransientDepency
{
    private readonly RoleRollsDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    private static string HistoryCacheKey(Guid sceneId)
    {
        return $"LastHistoryFromScene_{sceneId}";
    }

    public CampaignSceneHistoryService(RoleRollsDbContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
    }


    public async Task<List<CampaignSceneHistoryOutput>> GetList(Guid campaignId, Guid sceneId, GetSceneHistoryInput input)
    {
        var rolls = await _dbContext.Rolls
            .AsNoTracking()
            .Where(roll => roll.SceneId == sceneId)
            .ToListAsync();
        var history = rolls.Select(roll => new CampaignSceneHistoryOutput(roll))
            .ToList();
        return history;
    }    
    public DateTime? GetLastHistoryTime(Guid campaignId, Guid sceneId)
    {
        if (_memoryCache.TryGetValue<DateTime?>(HistoryCacheKey(sceneId), out var lastDate));
        {
            return lastDate;
        }
    }
}