using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Spells.Entities;

public class Spell : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<SpellCircle> Circles { get; set; } = new List<SpellCircle>();
    public ICollection<Archetype> Archetypes { get; set; } = new List<Archetype>();
}


