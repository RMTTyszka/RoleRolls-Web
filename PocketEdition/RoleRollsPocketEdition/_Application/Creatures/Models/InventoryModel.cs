using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Domain.Creatures.Entities;

namespace RoleRollsPocketEdition._Application.Creatures.Models;

public class InventoryModel
{
    public List<ItemModel> Items { get; set; } = new List<ItemModel>();

    public static InventoryModel FromCreature(Creature creature)
    {
        return new InventoryModel
        {
            Items = creature.Inventory.Items.Select(ItemModel.FromItem).ToList(),
        };
    }
}