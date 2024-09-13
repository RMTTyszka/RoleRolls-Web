using RoleRollsPocketEdition.Domain.Itens;

namespace RoleRollsPocketEdition._Domain.Itens;

public class WeaponInstance : EquipableInstance
{
    public WeaponSize Size { get; set; }
    public WeaponCategory Category { get; set; }
    public new WeaponTemplate Template { get; set; }
}