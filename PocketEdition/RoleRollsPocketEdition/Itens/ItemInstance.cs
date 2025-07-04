using RoleRollsPocketEdition.Core.Entities;
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
    public int GetBonus => Level / 2;
    /*public Guid? InventoryId { get; set; }
    public Inventory? Inventory { get; set; }*/
    public Guid TemplateId { get; set; }

    public ItemTemplate Template { get; set; }
    public ArmorTemplate? ArmorTemplate => Template as ArmorTemplate;
    public WeaponTemplate? WeaponTemplate => Template as WeaponTemplate;
    public bool IsWeaponTemplate => Template is WeaponTemplate;

    public void Update(ItemInstanceUpdate input)
    {
        throw new NotImplementedException();
    }
}