using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.EntityFramework;
using RoleRollsPocketEdition.Creatures.Dtos;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Creatures.Services;

public interface ICreatureEquipmentService
{
    Task Equip(Guid campaignId, Guid creatureId, EquipItemInput input);
    Task Unequip(Guid campaignId, Guid creatureId, EquipableSlot slot);
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
        creature.Equip(item, input.Slot);
        
        using (_unitOfWork.Begin())
        {
            _context.Creatures.Update(creature);
            await _unitOfWork.CommitAsync();
        }
    }

    public async Task Unequip(Guid campaignId, Guid creatureId, EquipableSlot slot)
    {
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        if (creature.Equipment.Id == Guid.Empty)
        {
            creature.Equipment.Id = Guid.NewGuid();
            creature.Equipment.CreatureId = creatureId;
            creature.Equipment.Creature  = creature;
            await _context.Equipment.AddAsync(creature.Equipment);
            await _context.SaveChangesAsync();
        }
        creature.Unequip(slot);
        
        using (_unitOfWork.Begin())
        {
            _context.Creatures.Update(creature);
            _unitOfWork.CommitAsync();
        }
    }
}