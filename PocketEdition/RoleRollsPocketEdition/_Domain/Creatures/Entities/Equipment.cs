using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities;

public class Equipment : Entity
{
    public Guid CreatureId { get; set; }
    public Creature Creature { get; set; }
    public EquipableInstance? MainHand { get; set; }
    public EquipableInstance? OffHand { get; set; }
    public EquipableInstance? Head { get; set; }
    public EquipableInstance? Chest { get; set; }
    public EquipableInstance? Feet { get; set; }
    public EquipableInstance? Arms { get; set; }
    public EquipableInstance? Waist { get; set; }
    public EquipableInstance? Neck { get; set; }
    public EquipableInstance? LeftRing { get; set; }
    public EquipableInstance? RightRing { get; set; }
}

