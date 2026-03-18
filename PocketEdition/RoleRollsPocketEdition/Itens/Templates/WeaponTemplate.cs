using RoleRollsPocketEdition.Itens.Templates.Models;

namespace RoleRollsPocketEdition.Itens.Templates;

public class WeaponTemplate : EquipableTemplate
{
    public WeaponCategory Category { get; set; }
    public WeaponDamageType DamageType { get; set; }
    public bool IsRanged { get; set; }
    public string? Range { get; set; }
    public WeaponTemplate()
    {
        
    }
    public WeaponTemplate(WeaponTemplateModel item) : base(item)
    {
        Category = item.Category;
        DamageType = item.DamageType;
        IsRanged = item.IsRanged;
        Range = item.Range;
    }

    public void Update(WeaponTemplateModel item)
    {
        base.Update(item);
        Category = item.Category;
        DamageType = item.DamageType;
        IsRanged = item.IsRanged;
        Range = item.Range;
    }
    public virtual object ToUpperClass()
    {
        return WeaponTemplateModel.FromTemplate(this);
    }
}
