using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Application.Itens.Services;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Creatures.Services;

public interface ICreatureItemService
{
    Task Instantiate(Guid campaignId, Guid creatureId, ItemInstantiateInput input);
    Task Destroy(Guid campaignId, Guid id, Guid creatureId);
    Task Update(Guid campaignId, Guid id, Guid creatureId, ItemInstanceUpdate input);
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
    public async Task Instantiate(Guid campaignId, Guid creatureId, ItemInstantiateInput input)
    {
        var template = await _context.ItemTemplates.SingleAsync(x => x.Id == input.TemplateId);
        var creature = await _context.Creatures.SingleAsync(x => x.Id == creatureId);
        var item = template.Instantiate(input);
        creature.AddItem(item);
        using (_unitOfWork.Begin())
        {
            await _context.ItemInstances.AddAsync(item);
            _context.Creatures.Update(creature);
            _unitOfWork.Commit();
        }
    }   
    public async Task Destroy(Guid campaignId, Guid id, Guid creatureId)
    {
        var item = await _context.ItemInstances.SingleAsync(x => x.Id == id);
        var creature = await _context.Creatures.SingleAsync(x => x.Id == creatureId);
        creature.Destroy(item);
        using (_unitOfWork.Begin())
        {
            _context.ItemInstances.Remove(item);
            _context.Creatures.Update(creature);
            _unitOfWork.Commit();
        }
    }    
    public async Task Update(Guid campaignId, Guid id, Guid creatureId, ItemInstanceUpdate input)
    {
        var item = await _context.ItemInstances.SingleAsync(x => x.Id == id);
        var creature = await _context.Creatures.SingleAsync(x => x.Id == creatureId);
        item.Update(input);
        using (_unitOfWork.Begin())
        {
            _context.ItemInstances.Remove(item);
            _context.Creatures.Update(creature);
            _unitOfWork.Commit();
        }
    }
}