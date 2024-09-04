using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Domain.Itens;

namespace RoleRollsPocketEdition.Domain.Creatures.Entities;

public class Inventory : Entity
{
    public Guid CreatureId { get; set; }
    public Creature Creature { get; set; }
    public List<ItemInstance> Items { get; set; } = new List<ItemInstance>();
}