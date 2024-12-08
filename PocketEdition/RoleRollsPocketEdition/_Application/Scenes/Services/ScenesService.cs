﻿using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Attacks.Services;
using RoleRollsPocketEdition._Application.Creatures.Models;
using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition._Domain.Campaigns.Repositories;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Scenes.Entities;
using RoleRollsPocketEdition._Domain.Scenes.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Scenes.Services
{

    public class ScenesService : IScenesService
    {
        private readonly RoleRollsDbContext _roleRollsDbContext;
        private readonly ICreatureRepository _creatureRepository;

        public ScenesService(RoleRollsDbContext roleRollsDbContext, ICreatureRepository creatureRepository)
        {
            _roleRollsDbContext = roleRollsDbContext;
            _creatureRepository = creatureRepository;
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
        public Task<List<CreatureModel>> GetCreatures(Guid campaignId, Guid sceneId, CreatureType creatureType)
        {
            var creatureQuery = _creatureRepository.GetFullCreatureAsQueryable()
                .AsNoTracking();

            var creatures = from sceneCreature in _roleRollsDbContext.SceneCreatures.Where(scene => scene.SceneId == sceneId)
                            join creature in creatureQuery on sceneCreature.CreatureId equals creature.Id
                            where creature.Type == creatureType
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
                CreatureType = CreatureType.Hero

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
                CreatureType = CreatureType.Monster
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

        public void ProcessAction(Guid sceneId, AttackResult attackResult)
        {
            var result = new SceneAction
            {
                Description = $"{attackResult.Attacker.Name} attacked {attackResult.Target.Name} with {attackResult.Weapon.Name} and caused {attackResult.TotalDamage} damage",
                Id = Guid.NewGuid(),
                ActorId = attackResult.Attacker.Id,
                SceneId = sceneId
            };
            
        }
    }
}
