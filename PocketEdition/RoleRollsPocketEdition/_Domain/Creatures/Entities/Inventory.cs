using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities;

public class Inventory : Entity
{
    public Guid CreatureId { get; set; }
    public Creature Creature { get; set; }
    public List<ItemInstance> Items { get; set; } = new();

    public void AddItem(ItemInstance item)
    {
        if (Items.Any(e => e.Id == item.Id))
        {
            return;
        }
        Items.Add(item);
    }

    public void Destroy(ItemInstance item)
    {
        Items.Remove(item);
    }

    public ItemInstance Get(Guid idItem)
    {
        return Items.FirstOrDefault(e => e.Id == idItem);
    }
}