using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Archetypes;

public class Archetype : Entity, IHaveBonuses
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Bonus> Bonuses { get; set; }

    public Archetype()
    {
        
    }
    public Archetype(ArchetypeModel archetype)
    {
        Id = archetype.Id;
        Name = archetype.Name;
        Description = archetype.Description;
        Bonuses = archetype.Bonuses.Select(bonus => new Bonus(bonus)).ToList();
    }

    public async Task Update(ArchetypeModel archetypeModel, RoleRollsDbContext dbContext)
    {
        Name = archetypeModel.Name;
        Description = archetypeModel.Description;
        var syncedBonuses = Bonuses.Synchronize(archetypeModel.Bonuses);
        foreach (var bonusModel in syncedBonuses.ToAdd)       
        {
            await this.AddBonus(bonusModel, dbContext);
        }     
        foreach (var bonusModel in syncedBonuses.ToUpdate)       
        {
            var bonus = Bonuses.First(e => e.Id == bonusModel.Id);
            bonus.Update(bonusModel);
        }      
        foreach (var bonusModel in syncedBonuses.ToRemove)       
        {
            var bonus = Bonuses.First(e => e.Id == bonusModel.Id);
            Bonuses.Remove(bonus);
            dbContext.Bonus.Remove(bonus);
        }
        dbContext.Archetypes.Update(this);
    }
}