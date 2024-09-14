using RoleRollsPocketEdition._Application.Creatures.Models;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Scenes.Models;

namespace RoleRollsPocketEdition._Application.Scenes.Services
{
    public interface IScenesService
    {
        Task<List<CreatureModel>> GetCreatures(Guid campaignId, Guid sceneId, CreatureType creatureType);
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