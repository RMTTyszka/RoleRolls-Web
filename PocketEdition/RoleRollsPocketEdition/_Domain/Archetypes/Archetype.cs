using RoleRollsPocketEdition._Domain.Bonuses;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Archetypes;

public class Archetype : Entity, IHaveBonuses
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Bonus> Bonuses { get; set; }
}