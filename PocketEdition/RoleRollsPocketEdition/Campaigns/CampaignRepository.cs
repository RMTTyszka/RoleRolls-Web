using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Campaigns
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly RoleRollsDbContext _dbContext;

        public CampaignRepository(RoleRollsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CampaignTemplate> GetCreatureTemplateAggregateAsync(Guid id)
        {
            var creatureTemplate = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.SpecificSkillTemplates)
                .Include(template => template.Vitalities)
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == id);
            return creatureTemplate;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
