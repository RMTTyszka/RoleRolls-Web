﻿using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Rolls.Application;
using RoleRollsPocketEdition.Rolls.Services;

namespace RoleRollsPocketEdition.Creatures.Application.Services
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