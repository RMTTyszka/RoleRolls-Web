using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Powers.Models;

namespace RoleRollsPocketEdition.Archetypes.Models;

public class PowerDescriptionModel : IEntityDto
{
    public Guid Id { get; set; }

    public PowerDescriptionModel()
    {
        
    }

    public PowerDescriptionModel(ArchertypePowerDescription archertypePowerDescription)
    {
        Name = archertypePowerDescription.Name;
        Id = archertypePowerDescription.Id;
        Description = archertypePowerDescription.Description;
        GameDescription = archertypePowerDescription.GameDescription;
        Level = archertypePowerDescription.Level;
    }

    public int Level { get; set; }

    public string GameDescription { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }
}