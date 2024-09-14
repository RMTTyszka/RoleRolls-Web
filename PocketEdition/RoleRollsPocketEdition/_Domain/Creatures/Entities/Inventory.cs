using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities;

public class Inventory : Entity
{
    public Guid CreatureId { get; set; }
    public Creature Creature { get; set; }
    public List<ItemInstance> Items { get; set; } = new List<ItemInstance>();
}