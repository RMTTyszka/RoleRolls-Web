using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Itens;

namespace RoleRollsPocketEdition._Application.Creatures.Models;

public class InventoryModel
{
    public List<ItemModel> Items { get; set; } = new List<ItemModel>();

    public static InventoryModel FromCreature(Creature creature)
    {
        return new InventoryModel
        {
            Items = creature.Inventory.Items.Select(e =>
            {
                if (e is WeaponInstance)
                {
                    return WeaponInstanceModel.FromWeapon(e as WeaponInstance);
                }
            }).ToList(),
        };
    }
}

public class WeaponInstanceModel : EquipableModel
{
    private WeaponInstanceModel(WeaponInstance item) : base(item)
    {
        
    }
    public static object FromWeapon(WeaponInstance? item)
    {
        if (item == null)
        {
            return null;
        }
        return new WeaponInstanceModel(item);
    }
}
public class EquipableModel : ItemModel
{
    protected EquipableModel(EquipableInstance item) : base(item)
    {
        
    }
}