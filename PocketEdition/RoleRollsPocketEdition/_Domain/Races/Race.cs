using RoleRollsPocketEdition._Domain.Bonuses;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Races;

public class Race : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Bonus> Bonuses { get; set; }
}