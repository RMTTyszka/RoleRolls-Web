using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RoleRollsPocketEdition.Campaigns.Dtos;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Scenes.Entities;

namespace RoleRollsPocketEdition.Campaigns.ApplicationServices;

public interface ICampaignSceneHistoryBuilderService
{
    Task<List<CampaignSceneHistoryOutput>> GetList(Guid campaignId, Guid sceneId, GetSceneHistoryInput input);
    Task<SceneHistory> BuildHistory(Roll roll);
    Task<List<SceneHistory>> GetListV2(Guid campaignId, Guid sceneId);
    Task<SceneHistory> BuildHistory(SceneAction result);
}

public class CampaignSceneHistoryBuilderService : ICampaignSceneHistoryBuilderService, ITransientDependency
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
        var actions = await _dbContext.SceneActions
            .AsNoTracking()
            .Where(roll => roll.SceneId == sceneId)
            .ToListAsync();
        var history = new List<SceneHistory>();
        foreach (var roll in rolls)
        {
            history.Add(await BuildHistory(roll));   
        }    
        foreach (var action in actions)
        {
            history.Add(await BuildHistory(action));   
        }
        return history.OrderByDescending(e => e.AsOfDate).ToList();
    }

    public async Task<SceneHistory> BuildHistory(SceneAction result)
    {
        var actor = await GetActor(result.ActorId);
        return new ActionSceneHistory
        {
            Actor = actor,
            AsOfDate = DateTime.Now,
            Description = result.Description,
        };
    }

    private async Task<string> GetActor(Guid actorId)
    {
        if (_memoryCache.TryGetValue<string?>($"Actor_{actorId}", out var resultActor) &&
            !string.IsNullOrWhiteSpace(resultActor))
        {
            return resultActor;
        }
        var actor = await _dbContext.Creatures
            .AsNoTracking()
            .Where(c => c.Id == actorId)
            .Select(c => c.Name)
            .FirstAsync();
        var actorName = actor ?? "Desconhecido";
        _memoryCache.Set($"Actor_{actorId}", actorName);
        return actorName;
    }


    public async Task<SceneHistory> BuildHistory(Roll roll)
    {
        var actor = await GetActor(roll.ActorId);
        var property = roll.Property != null ? roll.Property.Type switch
        {
            PropertyType.Attribute => await _dbContext.Attributes.Where(e => e.Id == roll.Property.Id)
                .Select(a => a.Name)
                .FirstAsync(),
            PropertyType.Skill => await _dbContext.Skills.Where(e => e.Id == roll.Property.Id)
                .Select(a => a.Name)
                .FirstAsync(),
            PropertyType.MinorSkill => await _dbContext.MinorSkills.Where(e => e.Id == roll.Property.Id)
                .Select(a => a.Name)
                .FirstAsync(),   
            PropertyType.All => string.Empty,
            _ => throw new ArgumentOutOfRangeException()
        } : "";
        return new RollSceneHistory
        {
            AsOfDate = roll.DateTime,
            Actor = actor,
            Bonus = roll.Bonus,
            Rolls = roll.RolledDices,
            Success = roll.Success,
            Complexity = roll.Complexity,
            Difficulty = roll.Difficulty,
            Property = property,
            Id = roll.Id
        };
    }    
}
