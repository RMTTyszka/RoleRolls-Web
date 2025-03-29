using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Services
{
    public class DefenseTemplateService : IDefenseTemplateService
    {
        private readonly RoleRollsDbContext _dbContext;

        public DefenseTemplateService(RoleRollsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Guid creatureTemplateId, DefenseTemplateModel model)
        {
            var template = await _dbContext.CampaignTemplates
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == creatureTemplateId);

            var defenseAdded = await template.AddDefenseAsync(model, _dbContext);
            await _dbContext.SaveChangesAsync();
        }      

        public async Task UpdateAsync(Guid creatureTemplateId, DefenseTemplateModel model)
        {
            var template = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.SpecificSkills)
                .Include(template => template.Vitalities)
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == creatureTemplateId);
            
            var defenseUpdated = template.UpdateDefense(model, _dbContext);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid creatureTemplateId, Guid defenseTemplateId)
        {
            var template = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.SpecificSkills)
                .Include(template => template.Vitalities)
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == creatureTemplateId);
            template.RemoveDefense(defenseTemplateId, _dbContext);
            await _dbContext.SaveChangesAsync();
        }
    }
}
