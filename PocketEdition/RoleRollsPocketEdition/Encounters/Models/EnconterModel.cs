using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Encounters.Entities;

namespace RoleRollsPocketEdition.Encounters.Models;

public class EnconterModel : IEntityDto
{
    public string Name { get; set; }
    public Guid Id { get; set; }
    
    public List<CreatureModel> Creatures { get; set; } = [];

    public EnconterModel()
    {
        
    }

    public EnconterModel(Encounter encounter)
    {
        Name = encounter.Name;
        Id = encounter.Id;
        Creatures = encounter.Creatures.Select(e => new CreatureModel(e)).ToList();
    }
}


