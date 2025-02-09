using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.CreatureTypes.Entities;

namespace RoleRollsPocketEdition.CreatureTypes.Models;

public class CreatureTypeModel(CreatureType creatureType) : IEntityDto
{
    public Guid Id { get; set; } = creatureType.Id;
    public string Name { get; set; } = creatureType.Name;
    public string Description { get; set; } = creatureType.Description;
    public List<BonusModel> Bonuses { get; set; } = creatureType.Bonuses.ConvertAll(e => new BonusModel(e));
}