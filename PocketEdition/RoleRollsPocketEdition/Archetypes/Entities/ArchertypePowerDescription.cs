using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Archetypes.Entities;

public class ArchertypePowerDescription : Entity
{
    public Guid ArchetypeId { get; set; }
    public Archetype Archetype { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string GameDescription { get; set; }
    public int Level { get; set; }

    public ArchertypePowerDescription()
    {
        
    }

    public ArchertypePowerDescription(PowerDescriptionModel model)
    {
        Name = model.Name;
        Description = model.Description;
        GameDescription = model.GameDescription;
        Level = model.Level;
    }

    public void Update(PowerDescriptionModel model)
    {
        Name = model.Name;
        Description = model.Description;
        GameDescription = model.GameDescription;
        Level = model.Level;
    }
}