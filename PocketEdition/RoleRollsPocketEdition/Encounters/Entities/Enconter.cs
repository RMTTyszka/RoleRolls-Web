using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Encounters.Models;

namespace RoleRollsPocketEdition.Encounters.Entities;

public class Enconter : Entity
{
    public Enconter()
    {
        
    }
    public Enconter(EnconterModel encounter)
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

    public bool RemoveCreature(Creature creature)
    {
        if (creature == null)
            throw new ArgumentNullException(nameof(creature), "Creature cannot be null");
            
        return Creatures.Remove(creature);
    }

    public bool RemoveCreatureById(Guid creatureId)
    {
        var creature = Creatures.FirstOrDefault(c => c.Id == creatureId);
        if (creature != null)
        {
            return Creatures.Remove(creature);
        }
        return false;
    }

    public void Update(EnconterModel encounterModel)
    {
        Name = encounterModel.Name;
    }
}