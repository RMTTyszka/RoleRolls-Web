﻿using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Authentication.Application.Services;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Creatures.Dtos;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Rolls.Services;

namespace RoleRollsPocketEdition.Creatures.Services
{
    public class CreatureService : ICreatureService
    {

        private readonly RoleRollsDbContext _dbContext;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IRollSimulationService _simulationService;
        private readonly ICreatureRepository _creatureRepository;

        public CreatureService(RoleRollsDbContext dbContext, ICampaignRepository campaignsService, ICurrentUser currentUser, IRollSimulationService simulationService, ICreatureRepository creatureRepository)
        {
            _dbContext = dbContext;
            _campaignRepository = campaignsService;
            _currentUser = currentUser;
            _simulationService = simulationService;
            _creatureRepository = creatureRepository;
        }

        public async Task<List<CreatureModel>> GetAllAsync(Guid campaignId, GetAllCampaignCreaturesInput input)
        {
            var query = _creatureRepository.GetFullCreatureAsQueryable()
                .WhereIf(input.CreatureType.HasValue, creature => creature.Category == input.CreatureType)
                .Where(creature => campaignId == creature.CampaignId);

            if (input.CreatureIds.Any()) 
            {
                query = query.Where(creature => input.CreatureIds.Contains(creature.Id));
            }

            if (input.OwnerId.HasValue) 
            {
                query = query.Where(creature => input.OwnerId == creature.OwnerId);
            }
                
            var creatures = await query
                .ToListAsync();
            var output = creatures.Select(creature => new CreatureModel(creature))
                .ToList();
            return output;
        }
        public async Task<CreatureModel> GetAsync(Guid id)
        {
            var creature = await _creatureRepository.GetFullCreature(id);
            var output = new CreatureModel(creature);
            return output;
        }
        public async Task<CreatureUpdateValidationResult> CreateAsync(Guid campaignId, CreatureModel creatureModel)
        {
            var ownerId = _currentUser.User.Id;
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            var creature = creatureTemplate.InstantiateCreature(creatureModel.Name, campaignId, creatureModel.Category, ownerId);
            var result = creature.Update(creatureModel);
            if (result.Validation == CreatureUpdateValidation.Ok)
            {
                creature.FullRestore();
                await _dbContext.Creatures.AddAsync(creature);
                await _dbContext.SaveChangesAsync();
                result.Creature = new CreatureModel(creature);
                return result;
            }
            return result;

        }
        public async Task<CreatureUpdateValidationResult> UpdateAsync(Guid creatureId, CreatureModel creatureModel)
        {
            var creature = await _creatureRepository.GetFullCreature(creatureId);
            var result = creature.Update(creatureModel);
            if (result.Validation == CreatureUpdateValidation.Ok)
            {
                _dbContext.Creatures.Update(creature);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<CreatureModel> InstantiateFromTemplate(Guid campaignId, CreatureCategory creatureCategory)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            var creature = Creature.FromTemplate(creatureTemplate, campaignId, creatureCategory);
            var output = new CreatureModel(creature);
            return output;
        }
        

        public async Task<List<CdSimulationResult>> SimulateCd(Guid campaignId, Guid sceneId, Guid creatureId,
            SimulateCdInput input)
        {
            var creature = await _creatureRepository.GetFullCreature(creatureId);
            var propertyValue = creature.GetPropertyValue(input.PropertyType, input.PropertyId);
            var simulation = _simulationService.GetDc(propertyValue.Value, propertyValue.Bonus,
                input.ExpectedChance);
            return simulation;
        }



    }
}
