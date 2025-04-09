using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Archetypes.Validations;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Archetypes.Services;

public interface IArchetypeService
{
    Task<ArchetypeModel> GetAsync(Guid campaignTemplateId, Guid id);
    Task<PagedResult<ArchetypeModel>> GetListAsync(Guid campaignTemplateId, PagedRequestInput input);
    Task<ArchetypeValidationResult> CreateAsync(Guid campaignTemplateId, ArchetypeModel model);
    Task<ArchetypeValidationResult> UpdateAsync(Guid templateId, Guid archetypeId, ArchetypeModel model);
    Task<ArchetypeValidationResult> RemoveAsync(Guid campaignTemplateId, Guid archetypeId);

    public Task<ArchetypeValidationResult> AddBonus(Guid templateId, Guid archetypeId, BonusModel bonus);

    public Task<ArchetypeValidationResult> UpdateBonus(Guid templateId, Guid archetypeId, BonusModel bonus);

    public Task<ArchetypeValidationResult> RemoveBonus(Guid templateId, Guid archetypeId, Guid bonusId);
}

public class ArchetypeService : IArchetypeService, ITransientDependency
{
    private readonly RoleRollsDbContext _dbContext;

    public ArchetypeService(RoleRollsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ArchetypeModel> GetAsync(Guid campaignTemplateId, Guid id)
    {
        var archetype = await _dbContext.CampaignTemplates
            .AsNoTracking()
            .Include(template => template.Archetypes)
            .ThenInclude(a => a.PowerDescriptions)
            .Where(template => template.Id == campaignTemplateId)
            .SelectMany(template => template.Archetypes)
            .Where(type => type.Id == id)
            .Select(e => new ArchetypeModel(e))
            .FirstAsync();
        
        return archetype;
    }

    public async Task<PagedResult<ArchetypeModel>> GetListAsync(Guid campaignTemplateId, PagedRequestInput input)
    {
        var query = _dbContext.CampaignTemplates
            .AsNoTracking()
            .Include(template => template.Archetypes)
            .Where(template => template.Id == campaignTemplateId)
            .SelectMany(template => template.Archetypes)
            .Select(e => new ArchetypeModel(e));

        if (!string.IsNullOrEmpty(input.Filter))
        {
            query = query.Where(ct => ct.Name.Contains(input.Filter));
        }

        var totalCount = await query.CountAsync();
        var items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
        
        return new PagedResult<ArchetypeModel> { Items = items, TotalCount = totalCount };
    }

    public async Task<ArchetypeValidationResult> CreateAsync(Guid campaignTemplateId, ArchetypeModel model)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.Archetypes)
            .FirstAsync(template => template.Id == campaignTemplateId);

        await template.AddArchetypeAsync(model, _dbContext);
        _dbContext.CampaignTemplates.Update(template);
        await _dbContext.SaveChangesAsync();
        return ArchetypeValidationResult.Ok;
    }

    public async Task<ArchetypeValidationResult> UpdateAsync(Guid templateId, Guid archetypeId, ArchetypeModel model)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.Archetypes)
            .FirstAsync(template => template.Id == templateId);
        
        await template.UpdateArchetypeAsync(model, _dbContext);
        _dbContext.CampaignTemplates.Update(template);
        await _dbContext.SaveChangesAsync();
        return ArchetypeValidationResult.Ok;
    }

    public async Task<ArchetypeValidationResult> RemoveAsync(Guid campaignTemplateId, Guid archetypeId)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.Archetypes)
            .FirstAsync(template => template.Id == campaignTemplateId);
        template.RemoveArchetype(archetypeId, _dbContext);
        _dbContext.CampaignTemplates.Update(template);
        await _dbContext.SaveChangesAsync();
        return ArchetypeValidationResult.Ok;
    }
    public async Task<ArchetypeValidationResult> AddBonus(Guid templateId, Guid archetypeId, BonusModel bonus)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.Archetypes)
            .FirstAsync(template => template.Id == templateId);
        var archetype = template.Archetypes.First(t => t.Id == archetypeId);
        await archetype.AddBonus(bonus, _dbContext);
        await _dbContext.SaveChangesAsync();
        return ArchetypeValidationResult.Ok;
    }   
    public async Task<ArchetypeValidationResult> UpdateBonus(Guid templateId, Guid archetypeId, BonusModel bonus)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.Archetypes)
            .FirstAsync(template => template.Id == templateId);
        var archetype = template.Archetypes.First(t => t.Id == archetypeId);
        archetype.UpdateBonus(bonus);
        await _dbContext.SaveChangesAsync();
        return ArchetypeValidationResult.Ok;
    }    
    public async Task<ArchetypeValidationResult> RemoveBonus(Guid templateId, Guid archetypeId, Guid bonusId)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.Archetypes)
            .FirstAsync(template => template.Id == templateId);
        var archetype = template.Archetypes.First(t => t.Id == archetypeId);
        archetype.RemoveBonus(bonusId);
        await _dbContext.SaveChangesAsync();
        return ArchetypeValidationResult.Ok;
    }
}
