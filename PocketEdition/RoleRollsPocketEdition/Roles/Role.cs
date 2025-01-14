using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Roles;

public class Role : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Bonus> Bonuses { get; set; }
}