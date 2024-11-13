using RoleRollsPocketEdition._Domain.Itens;

namespace RoleRollsPocketEdition._Application.Creatures.Dtos;

public class EquipItemInput
{
    public Guid ItemId { get; set;  }
    public EquipableSlot Slot { get; set; }
}