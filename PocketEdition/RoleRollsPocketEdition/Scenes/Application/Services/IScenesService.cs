using RoleRollsPocketEdition.Scenes.Domain.Models;

namespace RoleRollsPocketEdition.Scenes.Application.Services
{
    public interface IScenesService
    {
        Task AddHero(Guid campaignId, Guid sceneId, List<SceneCreatureModel> creatures);
        Task AddMonster(Guid campaignId, Guid sceneId, List<SceneCreatureModel> creatures);
        Task Create(Guid campaignId, SceneModel sceneModel);
        Task<List<SceneModel>> GetAllAsync(Guid campaignId);
        Task<SceneModel> GetAsync(Guid sceneId);
        Task Update(Guid campaignId, Guid sceneId, SceneModel sceneModel);
        Task DeleteAsync(Guid campaignId, Guid sceneId);
        Task RemoveCreature(Guid campaignId, Guid sceneId, Guid creatureId);
    }
}