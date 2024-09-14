using RoleRollsPocketEdition._Application.Creatures.Dtos;
using RoleRollsPocketEdition._Application.Creatures.Models;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Rolls.Services;

namespace RoleRollsPocketEdition._Application.Creatures.Services
{
    public interface ICreatureService
    {
        Task<CreatureUpdateValidationResult> CreateAsync(Guid campaignId, CreatureModel creature);
        Task<List<CreatureModel>> GetAllAsync(Guid campaignId, GetAllCampaignCreaturesInput input);
        Task<CreatureModel> GetAsync(Guid id);
        Task<CreatureUpdateValidationResult> UpdateAsync(Guid creatureId, CreatureModel creatureModel);
        Task<CreatureModel> InstantiateFromTemplate(Guid campaignId, CreatureType creatureType);

        Task<List<CdSimulationResult>> SimulateCd(Guid campaignId, Guid sceneId, Guid creatureId,
            SimulateCdInput input);
    }
}