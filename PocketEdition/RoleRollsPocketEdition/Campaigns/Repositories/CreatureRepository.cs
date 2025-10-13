using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Campaigns.Repositories;

public interface ICreatureRepository
{
    Task<List<Creature>> GetFullCreatures(List<Guid> ids);
    IQueryable<Creature> GetFullCreatureAsQueryable();
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
            .Include(creature => creature.Vitalities)
            .Include(creature => creature.Defenses)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.MainHand)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.OffHand)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Head)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Chest)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Feet)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Arms)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Hands)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Waist)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Neck)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.LeftRing)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.RightRing)
            .ThenInclude(e => e.Template).Include(creature => creature.Inventory)
            .ThenInclude(inventory => inventory.Items)
            .ThenInclude(item => item.Template)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.SpecificSkills)
            .Where(creature => ids.Contains(creature.Id))
            .ToListAsync();
        return creatures;
    }

    public IQueryable<Creature> GetFullCreatureAsQueryable()
    {
        var query = _dbContext.Creatures
            .AsSplitQuery()
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.SpecificSkills)
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Vitalities)
            .ThenInclude(v => v.VitalityTemplate)
            .Include(creature => creature.Defenses)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.MainHand)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.OffHand)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Head)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Chest)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Feet)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Arms)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Hands)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Waist)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.Neck)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.LeftRing)
            .ThenInclude(e => e.Template)
            .Include(c => c.Equipment)
            .ThenInclude(e => e.RightRing)
            .ThenInclude(e => e.Template)
            .Include(creature => creature.Inventory)
            .ThenInclude(inventory => inventory.Items)
            .ThenInclude(item => item.Template);
        return query;
    }

    public async Task<Creature> GetFullCreature(Guid id)
    {
        var query = GetFullCreatureAsQueryable();
        var creature = await query.FirstAsync(e => e.Id == id);
        return creature;
    }

    public void Update(Creature creature)
    {
        _dbContext.Creatures.Update(creature);
    }
}
