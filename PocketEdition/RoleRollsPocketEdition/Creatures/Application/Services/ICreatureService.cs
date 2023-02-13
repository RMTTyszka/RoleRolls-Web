﻿using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Models;

namespace RoleRollsPocketEdition.Creatures.Application.Services
{
    public interface ICreatureService
    {
        Task CreateAsync(string name, Guid campaignId, CreatureType type);
        Task<List<CreatureModel>> GetAllAsync(Guid campaignId, GetAllCampaignCreaturesInput input);
        Task<CreatureModel> GetAsync(Guid id);
        Task<bool> UpdateAsync(Guid creatureId, CreatureModel creatureModel);
    }
}