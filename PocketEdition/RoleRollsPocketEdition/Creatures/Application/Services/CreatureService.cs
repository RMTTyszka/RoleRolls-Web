using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain;
using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Entities;
using RoleRollsPocketEdition.Creatures.Domain.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Creatures.Application.Services
{
    public class CreatureService : ICreatureService
    {

        private readonly RoleRollsDbContext _dbContext;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICurrentUser _currentUser;

        public CreatureService(RoleRollsDbContext dbContext, ICampaignRepository campaignsService, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _campaignRepository = campaignsService;
            _currentUser = currentUser;
        }

        public async Task<List<CreatureModel>> GetAllAsync(Guid campaignId, GetAllCampaignCreaturesInput input)
        {
            var query = _dbContext.Creatures
                .AsNoTracking()
                .Include(creature => creature.Attributes)
                .Include(creature => creature.Lifes)
                .Include(creature => creature.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .Where(creature => campaignId == creature.CampaignId);

            if (input.CreatureIds.Any()) 
            {
                query = query.Where(creature => input.CreatureIds.Contains(creature.Id));
            }

            if (input.OwnerId.HasValue) 
            {
                query = query.Where(creature => input.OwnerId == creature.OwnerId);
            }

            if (input.CreatureType.HasValue) 
            {
                query = query.Where(creature => creature.Type == input.CreatureType.Value);
            }
                
            var creatures = await query
                .ToListAsync();
            var output = creatures.Select(creature => new CreatureModel(creature))
                .ToList();
            return output;
        }
        public async Task<CreatureModel> GetAsync(Guid id)
        {
            var creature = await GetFullCreature(id);
            var output = new CreatureModel(creature);
            return output;
        }
        public async Task<CreatureUpdateValidationResult> CreateAsync(Guid campaignId, CreatureModel creatureModel)
        {
            var ownerId = _currentUser.User.Id;
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            var creature = creatureTemplate.InstantiateCreature(creatureModel.Name, campaignId, creatureModel.Type, ownerId);
            var result = creature.Update(creatureModel);
            if (result.Validation == CreatureUpdateValidation.Ok)
            {
                await _dbContext.Creatures.AddAsync(creature);
                await _dbContext.SaveChangesAsync();
                result.Creature = new CreatureModel(creature);
                return result;
            }
            return result;

        }
        public async Task<CreatureUpdateValidationResult> UpdateAsync(Guid creatureId, CreatureModel creatureModel)
        {
            var creature = await GetFullCreature(creatureId);
            var result = creature.Update(creatureModel);
            if (result.Validation == CreatureUpdateValidation.Ok)
            {
                _dbContext.Creatures.Update(creature);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<CreatureModel> InstantiateFromTemplate(Guid campaignId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            var creature = Creature.FromTemplate(creatureTemplate, campaignId);
            var output = new CreatureModel(creature);
            return output;
        }

        private async Task<Creature> GetFullCreature(Guid id)
        {
            var creature = await _dbContext.Creatures
                .Include(creature => creature.Attributes)
                .Include(creature => creature.Lifes)
                .Include(creature => creature.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .FirstAsync(creature => creature.Id == id);
            return creature;
        }
        private async Task<List<Creature>> GetFullCreatures(List<Guid> ids)
        {
            var creatures = await _dbContext.Creatures
                .Include(creature => creature.Attributes)
                .Include(creature => creature.Lifes)
                .Include(creature => creature.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .Where(creature => ids.Contains(creature.Id))
                .ToListAsync();
            return creatures;
        }
    }
}
