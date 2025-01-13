using RoleRollsPocketEdition.Itens.Templates.Models;

namespace RoleRollsPocketEdition.Itens.Templates;

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
    public virtual object ToUpperClass()
    {
        return WeaponTemplateModel.FromTemplate(this);
    }
}