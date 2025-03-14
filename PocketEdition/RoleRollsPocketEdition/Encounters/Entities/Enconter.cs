using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Encounters;

public class Enconter : Entity
{
    public string Name { get; set; }
    public Campaign Campaign { get; set; }
    public ICollection<Creature> Creatures { get; set; } = new List<Creature>();
    // Method to add a creature to the collection
    public void AddCreature(Creature creature)
    {
        if (creature == null)
            throw new ArgumentNullException(nameof(creature), "Creature cannot be null");
            
        Creatures.Add(creature);
    }

    // Method to remove a creature from the collection
    public bool RemoveCreature(Creature creature)
    {
        if (creature == null)
            throw new ArgumentNullException(nameof(creature), "Creature cannot be null");
            
        return Creatures.Remove(creature);
    }

    // Alternative method to remove a creature by ID if Creature inherits from Entity with an ID
    public bool RemoveCreatureById(int creatureId)
    {
        var creature = Creatures.FirstOrDefault(c => c.Id == creatureId);
        if (creature != null)
        {
            return Creatures.Remove(creature);
        }
        return false;
    }
}