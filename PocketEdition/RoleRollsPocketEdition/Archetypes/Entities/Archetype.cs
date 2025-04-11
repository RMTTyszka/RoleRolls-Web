using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Archetypes.Entities;

public class Archetype : Entity, IHaveBonuses
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
    public Guid CampaignTemplateId { get; set; }
    public CampaignTemplate CampaignTemplate { get; set; }
    public List<Bonus> Bonuses { get; set; } = [];
    public ICollection<Creature> Creatures { get; set; }
    public List<ArchertypePowerDescription> PowerDescriptions { get; set; } = [];

    public Archetype()
    {
        
    }
    public Archetype(ArchetypeModel archetype)
    {
        Id = archetype.Id;
        Name = archetype.Name;
        Description = archetype.Description;
        Details = archetype.Details;
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
        var syncedPowerDescriptions = PowerDescriptions.Synchronize(archetypeModel.PowerDescriptions);
        foreach (var toAdd in syncedPowerDescriptions.ToAdd)
        {
            var entity = new ArchertypePowerDescription(toAdd);
            PowerDescriptions.Add(entity);
            await dbContext.ArchertypePowerDescriptions.AddAsync(entity);
        }     
        foreach (var toUpdate in syncedPowerDescriptions.ToUpdate)       
        {
            var entity = PowerDescriptions.First(e => e.Id == toUpdate.Id);
            entity.Update(toUpdate);
        }      
        foreach (var toRemove in syncedPowerDescriptions.ToRemove)       
        {
            var entity = PowerDescriptions.First(e => e.Id == toRemove.Id);
            PowerDescriptions.Remove(entity);
            dbContext.ArchertypePowerDescriptions.Remove(entity);
        }      
        dbContext.Archetypes.Update(this);
    }
    public async Task AddPowerDescription(PowerDescriptionModel powerDescriptionModel, RoleRollsDbContext dbContext)
    {
        var powerDescription = new ArchertypePowerDescription(powerDescriptionModel);
        PowerDescriptions.Add(powerDescription);
        await dbContext.ArchertypePowerDescriptions.AddAsync(powerDescription);
    }    
    public void UpdatePowerDescription(PowerDescriptionModel powerDescriptionModel)
    {
        var powerDescription = PowerDescriptions.First(e => e.Id == powerDescriptionModel.Id);
        powerDescription.Update(powerDescriptionModel);
    }

    public void RemovePowerDescription(Guid powerDescriptionId)
    {
        PowerDescriptions.RemoveAll(b => b.Id == powerDescriptionId);
    }
}