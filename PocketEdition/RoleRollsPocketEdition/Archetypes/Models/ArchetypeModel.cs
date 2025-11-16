using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Spells.Models;

namespace RoleRollsPocketEdition.Archetypes.Models;

public class ArchetypeModel : IEntityDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<BonusModel> Bonuses { get; set; }
    public Guid Id { get; set; }
    public string Details { get; set; } = "";

    public ArchetypeModel()
    {
        
    }

    public ArchetypeModel(Archetype archetype)
    {
        Id = archetype.Id;
        Name = archetype.Name;
        Description = archetype.Description;
        Details = archetype.Details;
        PowerDescriptions = archetype.PowerDescriptions.Select(e => new PowerDescriptionModel(e)).ToList();
        Spells = (archetype.Spells ?? new List<Spells.Entities.Spell>())
            .Select(s => new SpellModel(s))
            .ToList();
        Bonuses = archetype.Bonuses.Select(bonus => new BonusModel(bonus)).ToList();
    }

    public List<PowerDescriptionModel> PowerDescriptions { get; set; }
    public List<SpellModel> Spells { get; set; } = new();
}


