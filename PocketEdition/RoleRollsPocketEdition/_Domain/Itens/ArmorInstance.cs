using RoleRollsPocketEdition.Domain.Itens;

namespace RoleRollsPocketEdition._Domain.Itens;

public class ArmorInstance : EquipableInstance
{
    public ArmorCategory Category { get; set; }
}

public enum ArmorCategory
{
    None = 0,
    Light = 1,
    Medium = 2,
    Heavy = 3
}

