using RoleRollsPocketEdition._Domain.Itens.Templates.Models;

namespace RoleRollsPocketEdition._Domain.Itens.Templates;

public class WeaponTemplate : EquipableTemplate
{
    public WeaponCategory Category { get; set; }
    public WeaponDamageType DamageType { get; set; }
    public WeaponTemplate()
    {
        
    }
    public WeaponTemplate(WeaponTemplateModel item) : base(item)
    {
        Category = item.Category;
        DamageType = item.DamageType;
    }

    public void Update(WeaponTemplateModel item)
    {
        base.Update(item);
        Category = item.Category;
        DamageType = item.DamageType;
    }
    public override object ToUpperClass()
    {
        return new WeaponTemplateModel(this);
    }
}