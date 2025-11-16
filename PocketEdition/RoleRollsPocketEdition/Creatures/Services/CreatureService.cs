using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Authentication.Application.Services;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Core.Entities;
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
        private readonly ICreatureBuilderService _creatureBuilderService;
        private readonly IDiceRoller _diceRoller;

        public CreatureService(RoleRollsDbContext dbContext, ICampaignRepository campaignsService, ICurrentUser currentUser, IRollSimulationService simulationService, ICreatureRepository creatureRepository, ICreatureBuilderService creatureBuilderService, IDiceRoller diceRoller)
        {
            _dbContext = dbContext;
            _campaignRepository = campaignsService;
            _currentUser = currentUser;
            _simulationService = simulationService;
            _creatureRepository = creatureRepository;
            _creatureBuilderService = creatureBuilderService;
            _diceRoller = diceRoller;
        }

        public async Task<PagedResult<CreatureModel>> GetAllAsync(Guid campaignId, GetAllCampaignCreaturesInput input)
        {
            var masterId = await _dbContext.Campaigns.Where(e => e.Id == campaignId)
                .Select(e => e.MasterId)
                .FirstAsync();
            var userId = _currentUser.User.Id;
            var isMaster = masterId == userId;
            var query = _creatureRepository.GetFullCreatureAsQueryable()
                .WhereIf(input.CreatureCategory.HasValue, creature => creature.Category == input.CreatureCategory)
                .WhereIf(input.OnlyTemplates.HasValue, creature => creature.IsTemplate == input.OnlyTemplates)
                .WhereIf(!isMaster, creature => creature.OwnerId == userId)
                .Where(creature => campaignId == creature.CampaignId);
            var creatures = await query
                .PageBy(input)
                .ToListAsync();
            var count = await query.CountAsync();
            var itens = creatures.Select(creature => new CreatureModel(creature))
                .ToList();
            return new PagedResult<CreatureModel>(count, itens);
        }
        public async Task<CreatureModel> GetAsync(Guid id)
        {
            var creature = await _creatureRepository.GetFullCreature(id);
            var output = new CreatureModel(creature);
            return output;
        }
        public async Task<CreatureUpdateValidationResult> CreateAsync(Guid campaignId, CreatureModel creatureModel)
        {
            var result = await _creatureBuilderService.BuildCreature(campaignId, creatureModel);
            if (result.Validation == CreatureUpdateValidation.Ok)
            {
                await _dbContext.Creatures.AddAsync(result.Creature);
                await _dbContext.SaveChangesAsync();
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

        public async Task<CreatureModel> InstantiateFromTemplate(Guid campaignId, CreatureCategory creatureCategory,
            bool isTemplate)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId)
                           ?? throw new InvalidOperationException($"Campaign {campaignId} was not found.");
            var creatureTemplate =
                await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
            var creature = Creature.FromTemplate(creatureTemplate, campaignId, creatureCategory, isTemplate);
            var output = new CreatureModel(creature);
            return output;
        }
        

        public async Task<List<CdSimulationResult>> SimulateCd(Guid campaignId, Guid sceneId, Guid creatureId,
            SimulateCdInput input)
        {
            var creature = await _creatureRepository.GetFullCreature(creatureId);
            var propertyValue = creature.GetPropertyValue(new PropertyInput(
                input.Property, 
                null
            ));            var simulation = _simulationService.GetDc(propertyValue.Value, propertyValue.Bonus,
                input.ExpectedChance, _diceRoller);
            return simulation;
        }



    }
}
