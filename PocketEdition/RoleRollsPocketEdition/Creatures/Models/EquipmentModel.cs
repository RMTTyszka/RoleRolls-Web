using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Itens.Dtos;

namespace RoleRollsPocketEdition.Creatures.Models;

public class EquipmentModel
{
    public ItemModel? MainHand { get; set; }
    public ItemModel? OffHand { get; set; }
    public ItemModel? Head { get; set; }
    public ItemModel? Chest { get; set; }
    public ItemModel? Feet { get; set; }
    public ItemModel? Arms { get; set; }
    public ItemModel? Hands { get; set; }
    public ItemModel? Waist { get; set; }
    public ItemModel? Neck { get; set; }
    public ItemModel? LeftRing { get; set; }
    public ItemModel? RightRing { get; set; }
    public static EquipmentModel FromCreature(Creature creature)
    {
        return new EquipmentModel
        {
            MainHand = ItemModel.FromItem(creature.Equipment.MainHand),
            OffHand = ItemModel.FromItem(creature.Equipment.OffHand),
            Head = ItemModel.FromItem(creature.Equipment.Head),
            Chest = ItemModel.FromItem(creature.Equipment.Chest),
            Feet = ItemModel.FromItem(creature.Equipment.Feet),
            Arms = ItemModel.FromItem(creature.Equipment.Arms),
            Hands = ItemModel.FromItem(creature.Equipment.Hands),
            Waist = ItemModel.FromItem(creature.Equipment.Waist),
            Neck = ItemModel.FromItem(creature.Equipment.Neck),
            LeftRing = ItemModel.FromItem(creature.Equipment.LeftRing),
            RightRing = ItemModel.FromItem(creature.Equipment.RightRing),
        };
    }
}


