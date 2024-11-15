using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Itens;

namespace RoleRollsPocketEdition._Application.Creatures.Models;

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


