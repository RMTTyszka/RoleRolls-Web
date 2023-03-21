﻿using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Models;

namespace RoleRollsPocketEdition.Creatures.Application.Services
{
    public interface ICreatureService
    {
        Task<CreatureModel> CreateAsync(Guid campaignId, CreatureModel creature);
        Task<List<CreatureModel>> GetAllAsync(Guid campaignId, GetAllCampaignCreaturesInput input);
        Task<CreatureModel> GetAsync(Guid id);
        Task<CreatureUpdateValidationResult> UpdateAsync(Guid creatureId, CreatureModel creatureModel);
    }
}