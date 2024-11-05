using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Itens.Services;

public interface IItemTemplateService
{
    Task<PagedResult<object>> GetItemsAsync(Guid? campaignId, GetAllItensTemplateInput input);
    Task<ItemTemplateModel> GetItemAsync(Guid id);
    Task InsertItem(ItemTemplateModel item);
    Task InsertWeapon(WeaponTemplateModel item);
    Task UpdateWeapon(Guid id, WeaponTemplateModel item);
    Task UpdateItem(Guid id, ItemTemplateModel item);
    Task DeleteItem(Guid id);
    Task InsertArmor(ArmorTemplateModel item);
    Task UpdateArmor(Guid id, ArmorTemplateModel item);
}

public class ItemTemplateService : IItemTemplateService, ITransientDependency
{
    private readonly RoleRollsDbContext _roleRollsDbContext;

    public ItemTemplateService(RoleRollsDbContext roleRollsDbContext)
    {
        _roleRollsDbContext = roleRollsDbContext;
    }

    [NoTrackingAspect]
    public async Task<PagedResult<object>> GetItemsAsync(Guid? campaignId, GetAllItensTemplateInput input)
    {
            var query = _roleRollsDbContext.ItemTemplates
                .AsNoTracking()
                .WhereIf(campaignId.HasValue, c => c.CampaignId == campaignId)
                .WhereIf(input.ItemType.HasValue, c => c.Type == input.ItemType);
            var totalItems = await query.CountAsync();
            var items = await query
                .PageBy(input)
                .ToListAsync();
            var fixedItems = items.Select(e => e.ToUpperClass()).ToList();
            return new PagedResult<object>(totalItems, fixedItems);
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

    public async Task InsertWeapon(WeaponTemplateModel item)
    {
        var template = new WeaponTemplate(item);
        await _roleRollsDbContext.WeaponTemplates.AddAsync(template);
        await _roleRollsDbContext.SaveChangesAsync();
    }

    public async Task UpdateWeapon(Guid id, WeaponTemplateModel item)
    {
        var template = await _roleRollsDbContext.WeaponTemplates.FindAsync(id);
        if (template is not null)
        {
            template.Update(item);
            _roleRollsDbContext.WeaponTemplates.Update(template);
            await _roleRollsDbContext.SaveChangesAsync();   
        }
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

    public async Task InsertArmor(ArmorTemplateModel item)
    {
        var template = new ArmorTemplate(item);
        await _roleRollsDbContext.ArmorTemplates.AddAsync(template);
        await _roleRollsDbContext.SaveChangesAsync();
    }

    public async Task UpdateArmor(Guid id, ArmorTemplateModel item)
    {
        var template = await _roleRollsDbContext.ArmorTemplates.FindAsync(id);
        if (template is not null)
        {
            template.Update(item);
            _roleRollsDbContext.ArmorTemplates.Update(template);
            await _roleRollsDbContext.SaveChangesAsync();   
        }
    }
}