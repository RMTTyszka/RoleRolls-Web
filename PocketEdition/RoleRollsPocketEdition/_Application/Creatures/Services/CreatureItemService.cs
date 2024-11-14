using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Application.Itens.Services;
using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Creatures.Services;

public interface ICreatureItemService
{
    Task<ItemModel?> Instantiate(Guid campaignId, Guid creatureId, ItemInstantiateInput input);
    Task Destroy(Guid campaignId, Guid creatureId, Guid id);
    Task Update(Guid campaignId, Guid creatureId, Guid id, ItemInstanceUpdate input);
}

public class CreatureItemService : ICreatureItemService, ITransientDependency
{
    private readonly RoleRollsDbContext _context;
    private readonly IItemService _itemService;
    private readonly IUnitOfWork _unitOfWork;

    public CreatureItemService(RoleRollsDbContext dbContext, IItemService itemServic, IUnitOfWork unitOfWork)
    {
        _context = dbContext;
        _itemService = itemServic;
        _unitOfWork = unitOfWork;
    }
    public async Task<ItemModel?> Instantiate(Guid campaignId, Guid creatureId, ItemInstantiateInput input)
    {
        var template = await _context.ItemTemplates.SingleAsync(x => x.Id == input.TemplateId);
        var creature = await _context.Creatures
            .Include(creature => creature.Inventory)
            .ThenInclude(inventory => inventory.Items)
            .SingleAsync(x => x.Id == creatureId);
        var item = template.Instantiate(input);
        if (creature.Inventory.Id == Guid.Empty)
        {
            creature.Inventory.Id = Guid.NewGuid();
            creature.Inventory.CreatureId = creatureId;
            creature.Inventory.Creature  = creature;
            await _context.Inventory.AddAsync(creature.Inventory);
            await _context.SaveChangesAsync();
        }
        await creature.AddItem(item, _context);
        
        using (_unitOfWork.Begin())
        {
            _context.Creatures.Update(creature);
            _unitOfWork.Commit();
        }

        return ItemModel.FromItem(item);
    }   
    public async Task Destroy(Guid campaignId, Guid creatureId, Guid id)
    {
        var item = await _context.ItemInstances.SingleAsync(x => x.Id == id);
        var creature = await _context.Creatures
            .Include(creature => creature.Inventory)
            .ThenInclude(inventory => inventory.Items)     
            .SingleAsync(x => x.Id == creatureId);
        creature.Destroy(item);
        using (_unitOfWork.Begin())
        {
            _context.ItemInstances.Remove(item);
            _context.Creatures.Update(creature);
            _unitOfWork.Commit();
        }
    }    
    public async Task Update(Guid campaignId, Guid creatureId, Guid id, ItemInstanceUpdate input)
    {
        var item = await _context.ItemInstances.SingleAsync(x => x.Id == id);
        var creature = await _context.Creatures
            .Include(creature => creature.Inventory)
            .ThenInclude(inventory => inventory.Items)
            .SingleAsync(x => x.Id == creatureId);
        item.Update(input);
        using (_unitOfWork.Begin())
        {
            _context.ItemInstances.Remove(item);
            _context.Creatures.Update(creature);
            _unitOfWork.Commit();
        }
    }
}