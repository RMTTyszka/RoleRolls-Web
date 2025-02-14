using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;

namespace RoleRollsPocketEdition.Archetypes.Models;

public class ArchetypeModel : IEntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<BonusModel> Bonuses { get; set; }
    public Guid Id { get; set; }

    public ArchetypeModel()
    {
        
    }

    public ArchetypeModel(Archetype archetype)
    {
        Id = archetype.Id;
        Name = archetype.Name;
        Description = archetype.Description;
        Bonuses = archetype.Bonuses.Select(bonus => new BonusModel(bonus)).ToList();
    }
}