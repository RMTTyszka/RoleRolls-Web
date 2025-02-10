using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.CreatureTypes.Entities;

public class CreatureType : Entity
{
   

    public string Name { get; set; }
    public string Description { get; set; }
    public List<Bonus> Bonuses { get; set; }

    public async Task Update(CreatureTypeModel creatureTypeModel, RoleRollsDbContext dbContext)
    {
        Name = creatureTypeModel.Name;
        Description = creatureTypeModel.Description;
        var syncedBonuses = Bonuses.Synchronize(creatureTypeModel.Bonuses);
        foreach (var bonusModel in syncedBonuses.ToAdd)       
        {
            await AddBonus(bonusModel, dbContext);
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
        dbContext.CreatureTypes.Update(this);
    }
    public CreatureType()
    {
        
    }
    public CreatureType(CreatureTypeModel creatureTypeModel)
    {
        Id = creatureTypeModel.Id;
        Name = creatureTypeModel.Name;
        Description = creatureTypeModel.Description;
        Bonuses = creatureTypeModel.Bonuses.ConvertAll(e => new Bonus(e));
    }

    public async Task AddBonus(BonusModel bonusModel, RoleRollsDbContext dbContext)
    {
        var bonus = new Bonus(bonusModel);
        Bonuses.Add(bonus);
        await dbContext.Bonus.AddAsync(bonus);
    }
}