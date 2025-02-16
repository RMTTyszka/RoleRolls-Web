using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.CreatureTypes.Validations;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.CreatureTypes.Services;

public interface ICreatureTypeService
{
    Task<CreatureTypeModel> GetAsync(Guid campaignTemplateId, Guid id);
    Task<PagedResult<CreatureTypeModel>> GetListAsync(Guid campaignTemplateId, PagedRequestInput input);
    Task<CreatureTypeValidationResult> CreateAsync(Guid campaignTemplateId, CreatureTypeModel model);
    Task<CreatureTypeValidationResult> UpdateAsync(Guid templateId, Guid creatureTypeId, CreatureTypeModel model);
    Task<CreatureTypeValidationResult> RemoveAsync(Guid campaignTemplateId, Guid creatureTypeId);
}

public class CreatureTypeService : ICreatureTypeService, ITransientDependency
{
    private readonly RoleRollsDbContext _dbContext;

    public CreatureTypeService(RoleRollsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatureTypeModel> GetAsync(Guid campaignTemplateId, Guid id)
    {
        var creatureType = await _dbContext.CampaignTemplates
            .AsNoTracking()
            .Include(template => template.CreatureTypes)
            .Where(template => template.Id == campaignTemplateId)
            .SelectMany(template => template.CreatureTypes)
            .Where(type => type.Id == id)
            .Select(e => new CreatureTypeModel(e))
            .FirstAsync();
        
        return creatureType;
    }

    public async Task<PagedResult<CreatureTypeModel>> GetListAsync(Guid campaignTemplateId, PagedRequestInput input)
    {
        var query = _dbContext.CampaignTemplates
            .AsNoTracking()
            .Include(template => template.CreatureTypes)
            .Where(template => template.Id == campaignTemplateId)
            .SelectMany(template => template.CreatureTypes)
            .Select(e => new CreatureTypeModel(e));

        if (!string.IsNullOrEmpty(input.Filter))
        {
            query = query.Where(ct => ct.Name.Contains(input.Filter));
        }

        var totalCount = await query.CountAsync();
        var items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
        
        return new PagedResult<CreatureTypeModel> { Items = items, TotalCount = totalCount };
    }

    public async Task<CreatureTypeValidationResult> CreateAsync(Guid campaignTemplateId, CreatureTypeModel model)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.CreatureTypes)
            .FirstAsync(template => template.Id == campaignTemplateId);

        await template.AddCreatureTypeAsync(model, _dbContext);
        _dbContext.CampaignTemplates.Update(template);
        await _dbContext.SaveChangesAsync();
        return CreatureTypeValidationResult.Ok;
    }

    public async Task<CreatureTypeValidationResult> UpdateAsync(Guid templateId, Guid creatureTypeId, CreatureTypeModel model)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.CreatureTypes)
            .FirstAsync(template => template.Id == templateId);
        
        await template.UpdateCreatureType(creatureTypeId, model, _dbContext);
        _dbContext.CampaignTemplates.Update(template);
        await _dbContext.SaveChangesAsync();
        return CreatureTypeValidationResult.Ok;
    }

    public async Task<CreatureTypeValidationResult> RemoveAsync(Guid campaignTemplateId, Guid creatureTypeId)
    {
        var template = await _dbContext.CampaignTemplates
            .Include(template => template.CreatureTypes)
            .FirstAsync(template => template.Id == campaignTemplateId);
        template.RemoveCreatureType(creatureTypeId, _dbContext);
        _dbContext.CampaignTemplates.Update(template);
        await _dbContext.SaveChangesAsync();
        return CreatureTypeValidationResult.Ok;
    }
}
