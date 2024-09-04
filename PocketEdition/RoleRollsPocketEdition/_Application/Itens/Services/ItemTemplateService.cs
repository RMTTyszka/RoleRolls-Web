using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Domain.Itens;
using RoleRollsPocketEdition.Domain.Itens.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Itens.Services;

public interface IItemTemplateService
{
    Task<PagedResult<ItemTemplateModel>> GetItemsAsync(Guid? campaignId, GetAllItensTemplateInput input);
    Task<ItemTemplateModel> GetItemAsync(Guid id);
    Task InsertItem(ItemTemplateModel item);
    Task UpdateItem(Guid id, ItemTemplateModel item);
    Task DeleteItem(Guid id);
}

public class ItemTemplateService : IItemTemplateService, ITransientDependency
{
    private readonly RoleRollsDbContext _roleRollsDbContext;

    public ItemTemplateService(RoleRollsDbContext roleRollsDbContext)
    {
        _roleRollsDbContext = roleRollsDbContext;
    }

    [NoTrackingAspect]
    public async Task<PagedResult<ItemTemplateModel>> GetItemsAsync(Guid? campaignId, GetAllItensTemplateInput input)
    {
            var query = _roleRollsDbContext.ItemTemplates
                .AsNoTracking()
                .WhereIf(campaignId.HasValue, c => c.CampaignId == campaignId)
                .WhereIf(input.ItemType.HasValue, c => c.Type == input.ItemType);
            var totalItems = await query.CountAsync();
            var items = await query
                .PageBy(input)
                .Select(e => new ItemTemplateModel(e))
                .ToListAsync();
            return new PagedResult<ItemTemplateModel>(totalItems, items);
    }    
    [NoTrackingAspect]
    public async Task<ItemTemplateModel> GetItemAsync(Guid id)
    {
            var item = await _roleRollsDbContext.ItemTemplates
                .Where(e => e.Id == id)
                .Select(e => new ItemTemplateModel(e))
                .FirstOrDefaultAsync();
            return item;
    }

    public async Task InsertItem(ItemTemplateModel item)
    {
        var template = new ItemTemplate(item);
        await _roleRollsDbContext.ItemTemplates.AddAsync(template);
        await _roleRollsDbContext.SaveChangesAsync();
    }    
    public async Task UpdateItem(Guid id, ItemTemplateModel item)
    {
        var template = await _roleRollsDbContext.ItemTemplates.FindAsync(id);
        template.Update(item);
        _roleRollsDbContext.ItemTemplates.Update(template);
        await _roleRollsDbContext.SaveChangesAsync();
    }   
    public async Task DeleteItem(Guid id)
    {
        await _roleRollsDbContext.ItemTemplates.Where(e => e.Id == id)
            .ExecuteDeleteAsync();
        await _roleRollsDbContext.SaveChangesAsync();
    }
}