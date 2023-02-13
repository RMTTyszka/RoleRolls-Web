using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Domain;
using RoleRollsPocketEdition.Campaigns.Domain.Services;
using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Creatures.Application.Services
{
    public class CreatureService : ICreatureService
    {

        private readonly RoleRollsDbContext _dbContext;
        private readonly ICampaignRepository _campaignRepository;

        public CreatureService(RoleRollsDbContext dbContext, ICampaignRepository campaignsService)
        {
            _dbContext = dbContext;
            _campaignRepository = campaignsService;
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
                .Select(creature => new CreatureModel(creature))
                .ToListAsync();
            return creatures;
        }
        public async Task<CreatureModel> GetAsync(Guid id)
        {
            var creature = await GetFullCreature(id);
            var output = new CreatureModel(creature);
            return output;
        }
        public async Task CreateAsync(string name, Guid campaignId, CreatureType type)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            var creature = creatureTemplate.InstantiateCreature(name, campaignId, type);
            await _dbContext.Creatures.AddAsync(creature);
        }
        public async Task<bool> UpdateAsync(Guid creatureId, CreatureModel creatureModel)
        {
            var creature = await _dbContext.Creatures.FindAsync(creatureId);
            var success = creature.Update(creatureModel);
            if (success)
            {
                _dbContext.Creatures.Update(creature);
            }
            return success;
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
