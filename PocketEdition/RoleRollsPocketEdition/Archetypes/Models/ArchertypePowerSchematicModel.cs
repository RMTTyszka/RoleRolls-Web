using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Powers.Models;

namespace RoleRollsPocketEdition.Archetypes.Models;

public class ArchertypePowerSchematicModel : IEntityDto
{
    public int Level { get; set; }
    public Guid PowerId { get; set; }
    public PowerTemplateModel Power { get; set; }

    public Guid Id { get; set; }

    public ArchertypePowerSchematicModel()
    {
        
    }

    public ArchertypePowerSchematicModel(ArchertypePowerSchematic archertypePowerSchematic)
    {
        Level = archertypePowerSchematic.Level;
        Power = new PowerTemplateModel(archertypePowerSchematic.Power);
    }
}