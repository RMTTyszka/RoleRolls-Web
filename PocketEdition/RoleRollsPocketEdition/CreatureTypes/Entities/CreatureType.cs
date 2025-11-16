using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.CreatureTypes.Entities;

public class CreatureType : Entity, IHaveBonuses
{
   

    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanBeAlly { get; set; }
    public bool CanBeEnemy { get; set; }
    public Guid CampaignTemplateId { get; set; }
    public CampaignTemplate CampaignTemplate { get; set; }
    public List<Bonus> Bonuses { get; set; }
    public ICollection<Creature> Creatures { get; set; }

    public async Task Update(CreatureTypeModel creatureTypeModel, RoleRollsDbContext dbContext)
    {
        Name = creatureTypeModel.Name;
        Description = creatureTypeModel.Description;
        CanBeAlly = creatureTypeModel.CanBeAlly;
        CanBeEnemy = creatureTypeModel.CanBeEnemy;
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
    public void UpdateBonus(BonusModel bonusModel)
    {
        var bonus = Bonuses.First(e => e.Id == bonusModel.Id);
        bonus.Update(bonusModel);
    }

    public void RemoveBonus(Guid bonusId)
    {
        Bonuses.RemoveAll(b => b.Id == bonusId);
    }
}


