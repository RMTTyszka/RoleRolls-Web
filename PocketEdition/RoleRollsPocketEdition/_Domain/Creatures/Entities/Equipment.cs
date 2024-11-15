using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities;

public class Equipment : Entity
{
    public Guid CreatureId { get; set; }
    public Creature Creature { get; set; }
    public ItemInstance? MainHand { get; set; }
    public ItemInstance? OffHand { get; set; }
    public ItemInstance? Head { get; set; }
    public ItemInstance? Chest { get; set; }
    public ItemInstance? Feet { get; set; }
    public ItemInstance? Arms { get; set; }
    public ItemInstance? Hands { get; set; }
    public ItemInstance? Waist { get; set; }
    public ItemInstance? Neck { get; set; }
    public ItemInstance? LeftRing { get; set; }
    public ItemInstance? RightRing { get; set; }
    


    public async Task Equip(ItemInstance item, EquipableSlot slot)
    {
        switch (slot)
        {
            case EquipableSlot.MainHand:
                if (item.Template is WeaponTemplate)
                {
                    var equipedItem = MainHand;
                    if (equipedItem != null)
                    {
                        await Creature.AddItem(equipedItem);
                    }

                    MainHand = item;
                    await Creature.RemoveItem(item);
                    break;
                }
                return;
            case EquipableSlot.Chest:
            case EquipableSlot.Hands:
            case EquipableSlot.Arms:
            case EquipableSlot.RightRing:
            case EquipableSlot.LeftRing:
            case EquipableSlot.Neck:
            case EquipableSlot.Feet:
            case EquipableSlot.Waist:
            case EquipableSlot.Head:
                break;
            case EquipableSlot.OffHand:
                if (item.Template is WeaponTemplate)
                {
                    var equipedItem = OffHand;
                    if (equipedItem != null)
                    {
                        await Creature.AddItem(equipedItem);
                    }
                    OffHand = item;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
        }
    }

    public async Task Unequip(Guid itemId, EquipableSlot slot)
    {
        var item = GetItem(slot);
        if (item is not null)
        {
            await Creature.AddItem(item);
            ItemBySlot[slot] = null;
        }
    }

    private ItemInstance? GetItem(EquipableSlot slot)
    {
        return ItemBySlot.GetValueOrDefault(slot);
    }


    private Dictionary<EquipableSlot, ItemInstance?> ItemBySlot => new()
    {
        { EquipableSlot.MainHand, MainHand },
        { EquipableSlot.Chest, Chest },
        { EquipableSlot.Hands, Hands },
        { EquipableSlot.Arms, Arms },
        { EquipableSlot.RightRing, RightRing },
        { EquipableSlot.LeftRing, LeftRing },
        { EquipableSlot.Neck, Neck },
        { EquipableSlot.Feet, Feet },
        { EquipableSlot.Waist, Waist },
        { EquipableSlot.Head, Head },
        { EquipableSlot.OffHand, OffHand },
    };
}

