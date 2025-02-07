using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Scenes.Models;

namespace RoleRollsPocketEdition.Scenes.Services
{
    public interface IScenesService
    {
        Task<List<CreatureModel>> GetCreatures(Guid campaignId, Guid sceneId, CreatureCategory creatureCategory);
        Task AddHero(Guid campaignId, Guid sceneId, List<SceneCreatureModel> creatures);
        Task AddMonster(Guid campaignId, Guid sceneId, List<SceneCreatureModel> creatures);
        Task Create(Guid campaignId, SceneModel sceneModel);
        Task<List<SceneModel>> GetAllAsync(Guid campaignId);
        Task<SceneModel> GetAsync(Guid sceneId);
        Task Update(Guid campaignId, Guid sceneId, SceneModel sceneModel);
        Task DeleteAsync(Guid campaignId, Guid sceneId);
        Task RemoveCreature(Guid campaignId, Guid sceneId, Guid creatureId);
        Task ProcessAction(Guid sceneId, AttackResult attackResult);
    }
}