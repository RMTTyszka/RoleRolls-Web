using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Itens.Dtos;

namespace RoleRollsPocketEdition.Creatures.Models;

public class InventoryModel
{
    public List<ItemModel?> Items { get; set; } = new();

    public static InventoryModel FromCreature(Creature creature)
    {
        return new InventoryModel
        {
            Items = creature.Inventory.Items.Select(e =>
            {
                return ItemModel.FromItem(e);
            }).ToList(),
        };
    }
}


