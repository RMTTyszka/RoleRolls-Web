using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Domain.Campaigns.Repositories;

public interface ICreatureRepository
{
    Task<List<Creature>> GetFullCreatures(List<Guid> ids);
    Task<Creature> GetFullCreature(Guid id);
    void Update(Creature creature);
}

public class CreatureRepository : ICreatureRepository, ITransientDependency
{
    private readonly RoleRollsDbContext _dbContext;

    public CreatureRepository(RoleRollsDbContext roleRollsDbContext)
    {
        _dbContext = roleRollsDbContext;
    }
    
    public async Task<List<Creature>> GetFullCreatures(List<Guid> ids)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Lifes)
            .Include(creature => creature.Defenses)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.MainHand)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.OffHand)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Head)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Chest)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Feet)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Arms)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Hands)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Waist)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Neck)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.LeftRing)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.RightRing)
            .Include(creature => creature.Inventory)
            .ThenInclude(inventory => inventory.Items)
            .ThenInclude(item => item.Template)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .Where(creature => ids.Contains(creature.Id))
            .ToListAsync();
        return creatures;
    }
    public async Task<Creature> GetFullCreature(Guid id)
    {
        var creature = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Lifes)
            .Include(creature => creature.Defenses)
            .Include(creature => creature.Inventory)
            .ThenInclude(inventory => inventory.Items)
            .ThenInclude(item => item.Template)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .FirstAsync(creature => creature.Id == id);
        creature.ProcessLifes();
        return creature;
    }

    public void Update(Creature creature)
    {
        _dbContext.Creatures.Update(creature);
    }
}