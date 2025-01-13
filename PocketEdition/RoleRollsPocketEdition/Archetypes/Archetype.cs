using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Archetypes;

public class Archetype : Entity, IHaveBonuses
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Bonus> Bonuses { get; set; }
}