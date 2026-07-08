using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Attacks.Models;

public class BasicAttackInput
{
    public Guid TargetId { get; set; }
    public Guid? DefenseId { get; set; }
    public Property? VitalityId { get; set; }
    public EquipableSlot WeaponSlot { get; set; }
    public List<Guid> CombatManeuverIds { get; set; } = [];
    public int Luck { get; set; }
    public int Advantage { get; set; }
}
