using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities;

public class Equipment : Entity
{
    public Guid CreatureId { get; set; }
    public Creature Creature { get; set; }
    public WeaponInstance? MainHand { get; set; }
    public WeaponInstance? OffHand { get; set; }
    public EquipableInstance? Head { get; set; }
    public ArmorInstance? Chest { get; set; }
    public EquipableInstance? Feet { get; set; }
    public EquipableInstance? Arms { get; set; }
    public EquipableInstance? Hands { get; set; }
    public EquipableInstance? Waist { get; set; }
    public EquipableInstance? Neck { get; set; }
    public EquipableInstance? LeftRing { get; set; }
    public EquipableInstance? RightRing { get; set; }
    


    public async Task Equip(ItemInstance item, EquipableSlot slot, RoleRollsDbContext context)
    {
        switch (slot)
        {
            case EquipableSlot.MainHand:
                if (item is WeaponInstance instance)
                {
                    var equipedItem = MainHand;
                    if (equipedItem != null)
                    {
                        await Creature.AddItem(equipedItem, context);
                    }

                    MainHand = instance;
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
                if (item is WeaponInstance weaponInstance)
                {
                    var equipedItem = OffHand;
                    if (equipedItem != null)
                    {
                        await Creature.AddItem(equipedItem, context);
                    }
                    OffHand = weaponInstance;
                    break;
                }
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
        }
    }

    public async Task Unequip(Guid itemId, EquipableSlot slot, RoleRollsDbContext context)
    {
        var item = GetItem(slot);
        if (item is not null)
        {
            await Creature.AddItem(item, context);
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

