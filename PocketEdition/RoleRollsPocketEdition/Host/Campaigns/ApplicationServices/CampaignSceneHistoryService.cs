using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Global.Dtos;
using RoleRollsPocketEdition.Host.Campaigns.Dtos;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Campaigns.Application.Services;

public class CampaignSceneHistoryService
{
    private readonly RoleRollsDbContext _dbContext;



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
}