using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Attacks.Models;

public class EvadeInput
{
    public Guid AttackerId { get; set; }
    public EquipableSlot WeaponSlot { get; set; }
    public Property? VitalityId { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
}
