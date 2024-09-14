using RoleRollsPocketEdition._Domain.Itens.Templates;

namespace RoleRollsPocketEdition._Domain.Itens;

public class WeaponInstance : EquipableInstance
{
    public WeaponCategory Category { get; set; }
    public WeaponDamageType DamageType { get; set; }
    public new WeaponTemplate Template { get; set; }
}