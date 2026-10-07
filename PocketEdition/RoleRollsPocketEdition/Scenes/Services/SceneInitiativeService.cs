using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Scenes.Models;

namespace RoleRollsPocketEdition.Scenes.Services;

public interface ISceneInitiativeService
{
    Task<List<SceneInitiativeEntryModel>?> GetAsync(Guid campaignId, Guid sceneId);
    Task<List<SceneInitiativeEntryModel>?> RollAllAsync(Guid campaignId, Guid sceneId);
    Task<SceneInitiativeEntryModel?> RollCreatureAsync(Guid campaignId, Guid sceneId, Guid creatureId);
    Task<bool> RemoveCreatureAsync(Guid campaignId, Guid sceneId, Guid creatureId);
}

public class SceneInitiativeService : ISceneInitiativeService, ITransientDependency
{
    private readonly RoleRollsDbContext _dbContext;
    private readonly ICreatureRepository _creatureRepository;
    private readonly IInitiativeRoller _initiativeRoller;

    public SceneInitiativeService(
        RoleRollsDbContext dbContext,
        ICreatureRepository creatureRepository,
        IInitiativeRoller initiativeRoller)
    {
        _dbContext = dbContext;
        _creatureRepository = creatureRepository;
        _initiativeRoller = initiativeRoller;
    }

    public async Task<List<SceneInitiativeEntryModel>?> GetAsync(Guid campaignId, Guid sceneId)
    {
        var scene = await GetSceneAsync(campaignId, sceneId);
        return scene is null ? null : await ToModelsAsync(scene.Initiative);
    }

    public async Task<List<SceneInitiativeEntryModel>?> RollAllAsync(Guid campaignId, Guid sceneId)
    {
        var scene = await GetSceneAsync(campaignId, sceneId);
        if (scene is null)
            return null;

        var propertyId = await GetIniciativePropertyIdAsync(campaignId);
        var creatureIds = await _dbContext.SceneCreatures
            .Where(sceneCreature => sceneCreature.SceneId == sceneId)
            .Select(sceneCreature => sceneCreature.CreatureId)
            .ToListAsync();
        var creatures = await _creatureRepository.GetFullCreatures(creatureIds);

        if (creatures.Count != creatureIds.Distinct().Count())
        {
            throw new InvalidOperationException("Every creature in scene must exist before rolling initiative.");
        }

        var orderedRolls = _initiativeRoller.Order(creatures.Select(creature =>
            _initiativeRoller.Roll(creature.Id, InitiativePropertyResolver.GetValue(creature, propertyId))));

        _dbContext.SceneInitiativeEntries.RemoveRange(scene.Initiative);
        scene.Initiative.Clear();

        foreach (var (roll, index) in orderedRolls.Select((roll, index) => (roll, index)))
        {
            var entry = ToEntry(sceneId, roll, index + 1);
            scene.Initiative.Add(entry);
            await _dbContext.SceneInitiativeEntries.AddAsync(entry);
        }

        await _dbContext.SaveChangesAsync();
        return await ToModelsAsync(scene.Initiative);
    }

    public async Task<SceneInitiativeEntryModel?> RollCreatureAsync(Guid campaignId, Guid sceneId, Guid creatureId)
    {
        var scene = await GetSceneAsync(campaignId, sceneId);
        if (scene is null || !await IsSceneCreatureAsync(sceneId, creatureId))
            return null;

        var propertyId = await GetIniciativePropertyIdAsync(campaignId);
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        var newRoll = _initiativeRoller.Roll(
            creature.Id,
            InitiativePropertyResolver.GetValue(creature, propertyId));

        var entriesByCreatureId = scene.Initiative.ToDictionary(entry => entry.CreatureId);
        var rolls = entriesByCreatureId
            .Where(entry => entry.Key != creatureId)
            .Select(entry => new InitiativeRoll(
                entry.Value.CreatureId,
                entry.Value.PropertyValue,
                entry.Value.Score,
                []))
            .Append(newRoll);
        var orderedRolls = _initiativeRoller.Order(rolls);

        foreach (var (roll, index) in orderedRolls.Select((roll, index) => (roll, index)))
        {
            if (!entriesByCreatureId.TryGetValue(roll.CreatureId, out var entry))
            {
                entry = ToEntry(sceneId, roll, index + 1);
                entriesByCreatureId.Add(roll.CreatureId, entry);
                scene.Initiative.Add(entry);
                await _dbContext.SceneInitiativeEntries.AddAsync(entry);
            }
            else if (roll.CreatureId == creatureId)
            {
                entry.Score = roll.Score;
                entry.PropertyValue = roll.PropertyValue;
                entry.DiceResults = JsonSerializer.Serialize(roll.DiceResults);
            }

            entry.Position = index + 1;
        }

        await _dbContext.SaveChangesAsync();
        return (await ToModelsAsync([entriesByCreatureId[creatureId]])).Single();
    }

    public async Task<bool> RemoveCreatureAsync(Guid campaignId, Guid sceneId, Guid creatureId)
    {
        var scene = await GetSceneAsync(campaignId, sceneId);
        if (scene is null)
            return false;

        var entry = scene.Initiative.SingleOrDefault(entry => entry.CreatureId == creatureId);
        if (entry is null)
            return false;

        _dbContext.SceneInitiativeEntries.Remove(entry);
        scene.Initiative.Remove(entry);
        foreach (var (remainingEntry, index) in scene.Initiative
                     .OrderBy(remainingEntry => remainingEntry.Position)
                     .Select((remainingEntry, index) => (remainingEntry, index)))
        {
            remainingEntry.Position = index + 1;
        }
        await _dbContext.SaveChangesAsync();
        return true;
    }

    private async Task<Scene?> GetSceneAsync(Guid campaignId, Guid sceneId)
    {
        return await _dbContext.CampaignScenes
            .Include(scene => scene.Initiative)
            .SingleOrDefaultAsync(scene => scene.Id == sceneId && scene.CampaignId == campaignId);
    }

    private Task<bool> IsSceneCreatureAsync(Guid sceneId, Guid creatureId)
    {
        return _dbContext.SceneCreatures.AnyAsync(sceneCreature =>
            sceneCreature.SceneId == sceneId && sceneCreature.CreatureId == creatureId);
    }

    private async Task<Guid> GetIniciativePropertyIdAsync(Guid campaignId)
    {
        var propertyId = await _dbContext.Campaigns
            .Where(campaign => campaign.Id == campaignId)
            .Select(campaign => campaign.CampaignTemplate.IniciativePropertyId)
            .SingleAsync();

        return propertyId ?? throw new InvalidOperationException(
            $"Campaign {campaignId} template has no IniciativePropertyId configured.");
    }

    private async Task<List<SceneInitiativeEntryModel>> ToModelsAsync(IEnumerable<SceneInitiativeEntry> entries)
    {
        var entryList = entries.OrderBy(entry => entry.Position).ToList();
        var creatureIds = entryList.Select(entry => entry.CreatureId).ToList();
        var creatureNames = await _dbContext.Creatures
            .Where(creature => creatureIds.Contains(creature.Id))
            .Select(creature => new { creature.Id, creature.Name })
            .ToDictionaryAsync(creature => creature.Id, creature => creature.Name);

        return entryList
            .Select(entry => new SceneInitiativeEntryModel(
                entry,
                creatureNames.GetValueOrDefault(entry.CreatureId, string.Empty)))
            .ToList();
    }

    private static SceneInitiativeEntry ToEntry(Guid sceneId, InitiativeRoll roll, int position)
    {
        return new SceneInitiativeEntry
        {
            Id = Guid.NewGuid(),
            SceneId = sceneId,
            CreatureId = roll.CreatureId,
            Position = position,
            Score = roll.Score,
            PropertyValue = roll.PropertyValue,
            DiceResults = JsonSerializer.Serialize(roll.DiceResults)
        };
    }
}
