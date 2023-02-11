using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Scenes.Domain.Models;

namespace RoleRollsPocketEdition.Scenes.Application.Services
{

    public class ScenesService
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
        public async Task Update(Guid campaignId, SceneModel sceneModel)
        {
            var scene = new Scene(campaignId, sceneModel);

            await _roleRollsDbContext.CampaignScenes.AddAsync(scene);
            await _roleRollsDbContext.SaveChangesAsync();
        }
    }
}
