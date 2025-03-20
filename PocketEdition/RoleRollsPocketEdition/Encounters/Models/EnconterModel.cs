using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Encounters.Entities;

namespace RoleRollsPocketEdition.Encounters.Models;

public class EnconterModel : IEntityDto
{
    public string Name { get; set; }
    public Guid Id { get; set; }

    public EnconterModel()
    {
        
    }

    public EnconterModel(Enconter encounter)
    {
        Name = encounter.Name;
        Id = encounter.Id;
    }
}