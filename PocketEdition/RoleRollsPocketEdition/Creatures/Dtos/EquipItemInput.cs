using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Creatures.Dtos;

public class EquipItemInput
{
    public Guid ItemId { get; set;  }
    public EquipableSlot Slot { get; set; }
}