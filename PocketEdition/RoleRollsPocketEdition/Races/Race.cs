using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Races;

public class Race : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Bonus> Bonuses { get; set; }
}