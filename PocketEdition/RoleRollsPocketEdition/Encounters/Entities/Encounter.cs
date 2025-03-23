using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Encounters.Models;

namespace RoleRollsPocketEdition.Encounters.Entities;

public class Encounter : Entity
{
    public Encounter()
    {
        
    }
    public Encounter(EnconterModel encounter)
    {
        Name = encounter.Name;
        Id = encounter.Id;
    }

    public string Name { get; set; }
    public Campaign Campaign { get; set; }
    public ICollection<Creature> Creatures { get; set; } = new List<Creature>();
    public void AddCreature(Creature creature)
    {
        if (creature == null)
            throw new ArgumentNullException(nameof(creature), "Creature cannot be null");
            
        Creatures.Add(creature);
    }

    public void RemoveCreature(Creature creature)
    {
        Creatures.Remove(creature);
    }

    public void Update(EnconterModel encounterModel)
    {
        Name = encounterModel.Name;
    }
}