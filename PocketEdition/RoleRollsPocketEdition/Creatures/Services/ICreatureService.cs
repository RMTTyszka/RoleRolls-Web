using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Creatures.Dtos;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Rolls.Services;

namespace RoleRollsPocketEdition.Creatures.Services
{
    public interface ICreatureService
    {
        Task<CreatureUpdateValidationResult> CreateAsync(Guid campaignId, CreatureModel creature);
        Task<PagedResult<CreatureModel>> GetAllAsync(Guid campaignId, GetAllCampaignCreaturesInput input);
        Task<CreatureModel> GetAsync(Guid id);
        Task<CreatureUpdateValidationResult> UpdateAsync(Guid creatureId, CreatureModel creatureModel);
        Task<CreatureModel> InstantiateFromTemplate(Guid campaignId, CreatureCategory creatureCategory, bool isTemplate);

        Task<List<CdSimulationResult>> SimulateCd(Guid campaignId, Guid sceneId, Guid creatureId,
            SimulateCdInput input);
    }
}