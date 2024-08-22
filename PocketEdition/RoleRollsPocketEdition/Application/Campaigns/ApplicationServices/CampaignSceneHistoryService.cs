using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RoleRollsPocketEdition.Application.Campaigns.Dtos;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Host.Campaigns.Dtos;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Application.Campaigns.ApplicationServices;

public interface ICampaignSceneHistoryBuilderService
{
    Task<List<CampaignSceneHistoryOutput>> GetList(Guid campaignId, Guid sceneId, GetSceneHistoryInput input);
    DateTime? GetLastHistoryTime(Guid campaignId, Guid sceneId);
    Task<SceneHistory> BuildHistory(Roll roll);
    Task<List<SceneHistory>> GetListV2(Guid campaignId, Guid sceneId);
}

public class CampaignSceneHistoryBuilderService : ICampaignSceneHistoryBuilderService, ITransientDepency
{
    private readonly RoleRollsDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    private static string HistoryCacheKey(Guid sceneId)
    {
        return $"LastHistoryFromScene_{sceneId}";
    }

    public CampaignSceneHistoryBuilderService(RoleRollsDbContext dbContext, IMemoryCache memoryCache)
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
    public async Task<List<SceneHistory>> GetListV2(Guid campaignId, Guid sceneId)
    {
        var rolls = await _dbContext.Rolls
            .AsNoTracking()
            .Where(roll => roll.SceneId == sceneId)
            .ToListAsync();
        var history = new List<SceneHistory>();
        foreach (var roll in rolls)
        {
            history.Add(await BuildHistory(roll));   
        }
        return history.OrderByDescending(e => e.AsOfDate).ToList();
    }     
    public async Task<SceneHistory> BuildHistory(Roll roll)
    {
        var actor = await _dbContext.Creatures.Where(c => c.Id == roll.ActorId)
            .Select(c => c.Name)
            .FirstAsync();
        var property = roll.PropertyType switch
        {
            RollPropertyType.Attribute => await _dbContext.Attributes.Where(e => e.Id == roll.PropertyId)
                .Select(a => a.Name)
                .FirstAsync(),
            RollPropertyType.Skill => await _dbContext.Skills.Where(e => e.Id == roll.PropertyId)
                .Select(a => a.Name)
                .FirstAsync(),
            RollPropertyType.MinorSkill => await _dbContext.MinorSkills.Where(e => e.Id == roll.PropertyId)
                .Select(a => a.Name)
                .FirstAsync(),
            _ => throw new ArgumentOutOfRangeException()
        };
        return new RollSceneHistory
        {
            AsOfDate = roll.DateTime,
            Actor = actor,
            Bonus = roll.RollBonus,
            Rolls = roll.RolledDices,
            Success = roll.Success,
            Complexity = roll.Complexity,
            Difficulty = roll.Difficulty,
            Property = property
        };
    }    
    public DateTime? GetLastHistoryTime(Guid campaignId, Guid sceneId)
    {
        if (_memoryCache.TryGetValue<DateTime?>(HistoryCacheKey(sceneId), out var lastDate));
        {
            return lastDate;
        }
    }
}