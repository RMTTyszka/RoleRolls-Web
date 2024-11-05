using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Itens.Services;

public class ItemService
{
    private readonly RoleRollsDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public ItemService(RoleRollsDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Instantiate(Guid campaignId, Guid itemTemplateId, Guid creatureId, ItemInstanceUpdate input)
    {
        var template = await _context.ItemTemplates.SingleAsync(x => x.Id == itemTemplateId);
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