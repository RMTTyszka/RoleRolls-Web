using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Creatures.Dtos;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Application.Itens.Services;
using RoleRollsPocketEdition._Domain.Campaigns.Repositories;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Creatures.Services;

public interface ICreatureEquipmentService
{
    Task Equip(Guid campaignId, Guid creatureId, EquipItemInput input);
    Task Unequip(Guid campaignId, Guid creatureId, EquipItemInput input);
}

public class CreatureEquipmentService : ICreatureEquipmentService, ITransientDependency
{
    private readonly RoleRollsDbContext _context;
    private readonly ICreatureRepository _creatureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatureEquipmentService(RoleRollsDbContext dbContext, ICreatureRepository creatureRepository, IUnitOfWork unitOfWork)
    {
        _context = dbContext;
        _creatureRepository = creatureRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Equip(Guid campaignId, Guid creatureId, EquipItemInput input)
    {
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        var item = creature.Inventory.Get(input.ItemId);
        if (creature.Equipment.Id == Guid.Empty)
        {
            creature.Equipment.Id = Guid.NewGuid();
            creature.Equipment.CreatureId = creatureId;
            creature.Equipment.Creature  = creature;
            await _context.Equipment.AddAsync(creature.Equipment);
            await _context.SaveChangesAsync();
        }
        await creature.Equip(item, input.Slot);
        
        using (_unitOfWork.Begin())
        {
            _context.Creatures.Update(creature);
            _unitOfWork.Commit();
        }
    }

    public async Task Unequip(Guid campaignId, Guid creatureId, EquipItemInput input)
    {
        var creature = await _context.Creatures
            .Include(creature => creature.Inventory)
            .ThenInclude(inventory => inventory.Items)
            .Include(creature => creature.Equipment)
            .SingleAsync(x => x.Id == creatureId);
        if (creature.Equipment.Id == Guid.Empty)
        {
            creature.Equipment.Id = Guid.NewGuid();
            creature.Equipment.CreatureId = creatureId;
            creature.Equipment.Creature  = creature;
            await _context.Equipment.AddAsync(creature.Equipment);
            await _context.SaveChangesAsync();
        }
        await creature.Unequip(input.ItemId, input.Slot);
        
        using (_unitOfWork.Begin())
        {
            _context.Creatures.Update(creature);
            _unitOfWork.Commit();
        }
    }
}