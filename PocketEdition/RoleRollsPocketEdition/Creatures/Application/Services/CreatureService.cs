using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Domain;
using RoleRollsPocketEdition.Campaigns.Domain.Services;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Creatures.Application.Services
{
    public class CreatureService
    {

        private readonly RoleRollsDbContext _dbContext;
        private readonly ICampaignRepository _campaignRepository;

        public CreatureService(ICampaignRepository campaignsService)
        {
            _campaignRepository = campaignsService;
        }

        public CreatureService(RoleRollsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreatureModel> GetAsync(Guid id)
        {
            var creature = await GetFullCreature(id);
            var output = new CreatureModel(creature);
            return output;
        }     
        public async Task CreateAsync(string name, Guid campaignId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CreatureTemplateId);
            var creature = creatureTemplate.InstantiateCreature(name, campaignId);
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

        private Task<Creature> GetFullCreature(Guid id)
        {
            var creature = _dbContext.Creatures
                .Include(creature => creature.Attributes)
                .Include(creature => creature.Lifes)
                .Include(creature => creature.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .FirstAsync(creature => creature.Id == id);
            return creature;
        }
    }
}
