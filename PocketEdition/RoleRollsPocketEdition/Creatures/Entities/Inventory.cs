using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Creatures.Entities;

public class Inventory : Entity
{
    public Guid CreatureId { get; set; }
    public Creature Creature { get; set; }
    public List<ItemInstance> Items { get; set; } = [];

    public void AddItem(ItemInstance item)
    {
        if (Items.Any(e => e.Id == item.Id))
        {
            return;
        }
        Items.Add(item);
    }

    public void Remove(ItemInstance? item)
    {
        Items.Remove(item);
    }

    public ItemInstance Get(Guid idItem)
    {
        return Items.FirstOrDefault(e => e.Id == idItem);
    }

}