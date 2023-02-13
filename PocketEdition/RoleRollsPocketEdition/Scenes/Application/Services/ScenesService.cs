using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Scenes.Domain.Entities;
using RoleRollsPocketEdition.Scenes.Domain.Models;

namespace RoleRollsPocketEdition.Scenes.Application.Services
{

    public class ScenesService : IScenesService
    {
        private readonly RoleRollsDbContext _roleRollsDbContext;

        public ScenesService(RoleRollsDbContext roleRollsDbContext)
        {
            _roleRollsDbContext = roleRollsDbContext;
        }

        public async Task<List<SceneModel>> GetAllAsync(Guid campaignId)
        {
            var scenes = await _roleRollsDbContext.CampaignScenes
                .AsNoTracking()
                .Where(scene => scene.CampaignId == campaignId)
                .Select(scene => new SceneModel(scene.Id, scene.Name))
                .ToListAsync();
            return scenes;
        }
        public async Task<SceneModel> GetAsync(Guid sceneId)
        {
            var scene = await _roleRollsDbContext.CampaignScenes
                .AsNoTracking()
                .Where(scene => scene.Id == sceneId)
                .Select(scene => new SceneModel(scene.Id, scene.Name))
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
        public async Task AddHero(Guid campaignId, Guid sceneId, List<SceneCreatureModel> creatures)
        {
            var sceneCreatures = creatures.Select(creature => new SceneCreature
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
            await _roleRollsDbContext.SceneCreatures.AddRangeAsync(sceneCreatures);
            await _roleRollsDbContext.SaveChangesAsync();
        }
    }
}
