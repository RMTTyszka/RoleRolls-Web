﻿using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Scenes.Models;

namespace RoleRollsPocketEdition.Scenes.Services
{

    public class ScenesService : IScenesService
    {
        private readonly RoleRollsDbContext _roleRollsDbContext;
        private readonly ICreatureRepository _creatureRepository;
        private readonly ISceneNotificationService _sceneNotificationService;
        private readonly ICampaignSceneHistoryBuilderService _campaignSceneHistoryBuilderService;

        public ScenesService(RoleRollsDbContext roleRollsDbContext, ICreatureRepository creatureRepository, ISceneNotificationService sceneNotificationService, ICampaignSceneHistoryBuilderService campaignSceneHistoryBuilderService)
        {
            _roleRollsDbContext = roleRollsDbContext;
            _creatureRepository = creatureRepository;
            _sceneNotificationService = sceneNotificationService;
            _campaignSceneHistoryBuilderService = campaignSceneHistoryBuilderService;
        }

        public async Task<List<SceneModel>> GetAllAsync(Guid campaignId)
        {
            var scenes = await _roleRollsDbContext.CampaignScenes
                .AsNoTracking()
                .Where(scene => scene.CampaignId == campaignId)
                .Select(scene => new SceneModel(scene.Id, scene.Name, scene.CampaignId))
                .ToListAsync();
            return scenes;
        }
        public async Task<SceneModel> GetAsync(Guid sceneId)
        {
            var scene = await _roleRollsDbContext.CampaignScenes
                .AsNoTracking()
                .Where(scene => scene.Id == sceneId)
                .Select(scene => new SceneModel(scene.Id, scene.Name, scene.CampaignId))
                .FirstOrDefaultAsync();
            return scene;
        }
        public async Task Create(Guid campaignId, SceneModel sceneModel)
        {
            var scene = new Scene(campaignId, sceneModel);

            await _roleRollsDbContext.CampaignScenes.AddAsync(scene);
            await _roleRollsDbContext.SaveChangesAsync();
        }
        public async Task Update(Guid campaignId, Guid sceneId, SceneModel sceneModel)
        {
            var scene = await _roleRollsDbContext.CampaignScenes.FindAsync(sceneId);
            scene.Update(sceneModel);
            _roleRollsDbContext.CampaignScenes.Update(scene);
            await _roleRollsDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid campaignId, Guid sceneId)
        {
            var sceneToDelete = await (from scene in _roleRollsDbContext.CampaignScenes
                                .Where(scene => scene.Id == sceneId)
                                       join creature in _roleRollsDbContext.SceneCreatures on scene.Id equals creature.SceneId into creatures
                                       select new
                                       {
                                           Scene = scene,
                                           Creatures = creatures.ToList()
                                       }).FirstAsync();
            _roleRollsDbContext.CampaignScenes.Remove(sceneToDelete.Scene);
            _roleRollsDbContext.SceneCreatures.RemoveRange(sceneToDelete.Creatures);
            await _roleRollsDbContext.SaveChangesAsync();

        }
        public Task<List<CreatureModel>> GetCreatures(Guid campaignId, Guid sceneId, CreatureCategory creatureCategory)
        {
            var creatureQuery = _creatureRepository.GetFullCreatureAsQueryable()
                .AsNoTracking();

            var creatures = from sceneCreature in _roleRollsDbContext.SceneCreatures.Where(scene => scene.SceneId == sceneId)
                            join creature in creatureQuery on sceneCreature.CreatureId equals creature.Id
                            where creature.Category == creatureCategory
                            select new CreatureModel(creature);
            return creatures.ToListAsync();
        }
        public async Task AddHero(Guid campaignId, Guid sceneId, List<SceneCreatureModel> creatures)
        {
            var creatureIds = creatures.Select(creature => creature.CreatureId);
            var existingCreaturesIds = await _roleRollsDbContext.SceneCreatures
                .Where(creature => creatureIds.Contains(creature.CreatureId))
                .Where(creature => creature.SceneId == sceneId)
                .Select(creature => creature.CreatureId)
                .ToListAsync();
            var newCreatures = creatures.Where(creature => !existingCreaturesIds.Contains(creature.CreatureId))
                .ToList();
            var sceneCreatures = newCreatures.Select(creature => new SceneCreature
            {
                CreatureId = creature.CreatureId,
                SceneId = sceneId,
                Hidden = creature.Hidden,
                CreatureCategory = CreatureCategory.Hero

            }).ToList(); ;
            await _roleRollsDbContext.SceneCreatures.AddRangeAsync(sceneCreatures);
            await _roleRollsDbContext.SaveChangesAsync();
        }
        public async Task AddMonster(Guid campaignId, Guid sceneId, List<SceneCreatureModel> creatures)
        {
            var sceneCreatures = creatures.Select(creature => new SceneCreature
            {
                CreatureId = creature.CreatureId,
                SceneId = sceneId,
                Hidden = creature.Hidden,
                CreatureCategory = CreatureCategory.Monster
            }).ToList(); ;
            var monsterIds = creatures.Select(c => c.CreatureId).ToList();
            var monsters = await _roleRollsDbContext.Creatures.Where(e => monsterIds.Contains(e.Id))
                .ToListAsync();
            foreach (var monster in monsters)
            {
                monster.FullRestore();
            }
            await _roleRollsDbContext.SceneCreatures.AddRangeAsync(sceneCreatures);
            await _roleRollsDbContext.SaveChangesAsync();
        }

        public async Task RemoveCreature(Guid campaignId, Guid sceneId, Guid creatureId)
        {
            var sceneCreature = await _roleRollsDbContext.SceneCreatures.FirstAsync(creature => creature.CreatureId == creatureId);
            _roleRollsDbContext.SceneCreatures.Remove(sceneCreature);
            await _roleRollsDbContext.SaveChangesAsync();
        }

        public async Task ProcessAction(Guid sceneId, AttackResult attackResult)
        {
            var result = new SceneAction
            {
                Description = $"{attackResult.Attacker.Name} attacked {attackResult.Target.Name} with {attackResult.Weapon.Name} and caused {attackResult.TotalDamage} damage",
                Id = Guid.NewGuid(),
                ActorId = attackResult.Attacker.Id,
                SceneId = sceneId
            };
            var history = await _campaignSceneHistoryBuilderService.BuildHistory(result);
            _roleRollsDbContext.SceneActions.Add(result);
            await _roleRollsDbContext.SaveChangesAsync();
            await _sceneNotificationService.NotifyScene(sceneId, history);

        }
    }
}
