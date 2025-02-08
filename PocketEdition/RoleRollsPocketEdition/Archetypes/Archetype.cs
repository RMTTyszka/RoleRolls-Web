using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Archetypes;

public class Archetype : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Bonus> Bonuses { get; set; }
}