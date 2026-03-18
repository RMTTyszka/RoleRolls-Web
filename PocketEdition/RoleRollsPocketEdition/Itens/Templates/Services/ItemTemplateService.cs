using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Core.EntityFramework;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens.Dtos.Templates;
using RoleRollsPocketEdition.Itens.Templates.Models;

namespace RoleRollsPocketEdition.Itens.Templates.Services;

public interface IItemTemplateService
{
    Task<PagedResult<T>> GetItemsAsync<T, TEntity>(Guid? campaignId, GetAllItensTemplateInput input) where T : ItemTemplateModel, new()
        where TEntity : ItemTemplate;
    Task<T?> GetItemAsync<T>(Guid id) where T : ItemTemplateModel, new();
    Task InsertItem(ConsumableTemplateModel item);
    Task InsertWeapon(WeaponTemplateModel item);
    Task UpdateWeapon(Guid id, WeaponTemplateModel item);
    Task UpdateItem(Guid id, ConsumableTemplateModel item);
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
    public async Task<PagedResult<T>> GetItemsAsync<T, TEntity>(Guid? campaignId, GetAllItensTemplateInput input) where T : ItemTemplateModel, new()
    where TEntity : ItemTemplate
    {
        var dbSet = _roleRollsDbContext.Set<TEntity>();
        var query = dbSet
            .AsNoTracking()
            .WhereIf(campaignId.HasValue, c => c.CampaignId == campaignId);
        var totalItems = await query.CountAsync();
        var items = await query
            .PageBy(input)
            .ToListAsync();
        var fixedItems = items.Select(ToTemplateModel<T>).ToList();
        return new PagedResult<T>(totalItems, fixedItems);
    }

    [NoTrackingAspect]
    public async Task<T?> GetItemAsync<T>(Guid id) where T : ItemTemplateModel, new()
    {
        var item = await _roleRollsDbContext.ItemTemplates
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        return item is null ? null : ToTemplateModel<T>(item);
    }

    public async Task InsertItem(ConsumableTemplateModel item)
    {
        var template = new ConsumableTemplate(item);
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

    public async Task UpdateItem(Guid id, ConsumableTemplateModel item)
    {
        var template = await _roleRollsDbContext.ConsumableTemplates.FindAsync(id);
        if (template is null)
        {
            return;
        }
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

    private static T ToTemplateModel<T>(ItemTemplate item) where T : ItemTemplateModel, new()
    {
        ItemTemplateModel model = item switch
        {
            WeaponTemplate weapon => WeaponTemplateModel.FromTemplate(weapon),
            ArmorTemplate armor => ArmorTemplateModel.FromTemplate(armor),
            ConsumableTemplate consumable => ItemTemplateModel.FromTemplate<ConsumableTemplateModel>(consumable),
            _ => ItemTemplateModel.FromTemplate<ItemTemplateModel>(item)
        };

        if (model is T typedModel)
        {
            return typedModel;
        }

        throw new InvalidOperationException(
            $"Item template '{item.Id}' of type '{item.GetType().Name}' cannot be mapped to '{typeof(T).Name}'.");
    }
}
