using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Archetypes.Entities;

public class ArchertypePowerSchematic : Entity
{
    public Guid ArchetypeId { get; set; }
    public Archetype Archetype { get; set; }
    
    public int Level { get; set; }
    public int NumberOfChoises { get; set; }
    public Guid PowerId { get; set; }
    public List<PowerTemplate> Powers { get; set; }
    public string Name { get; set; }
}