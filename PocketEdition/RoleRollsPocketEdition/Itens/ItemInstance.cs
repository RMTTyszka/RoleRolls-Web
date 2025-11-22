using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Dtos;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Itens;

public class ItemInstance : Entity
{
    public string Name { get; set; }
    public Guid? PowerInstanceId { get; set; }
    public PowerInstance? PowerInstance { get; set; }
    public int Level { get; set; }
    public int LevelBonus => Level / 2;
    public int GetBonus => LevelBonus;
    /*public Guid? InventoryId { get; set; }
    public Inventory? Inventory { get; set; }*/
    public Guid TemplateId { get; set; }

    public ItemTemplate Template { get; set; }
    public ArmorTemplate? ArmorTemplate => Template as ArmorTemplate;
    public WeaponTemplate? WeaponTemplate => Template as WeaponTemplate;
    public bool IsWeaponTemplate => Template is WeaponTemplate;

    public int GetDefenseBonus1() =>
        ArmorTemplate is null ? 0 : ArmorDefinition.DefenseBonus1(ArmorTemplate.Category);

    public int GetDefenseBonus2() =>
        ArmorTemplate is null ? 0 : ArmorDefinition.DefenseBonus2(ArmorTemplate.Category);

    public void Update(ItemInstanceUpdate input)
    {
        throw new NotImplementedException();
    }
}
