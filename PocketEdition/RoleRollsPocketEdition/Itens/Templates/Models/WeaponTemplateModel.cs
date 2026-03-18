using RoleRollsPocketEdition.Itens.Dtos.Templates;

namespace RoleRollsPocketEdition.Itens.Templates.Models;

public class WeaponTemplateModel : EquipableTemplateModel
{
    public WeaponTemplateModel() : base()
    {
        
    }
    public static WeaponTemplateModel FromTemplate(WeaponTemplate template)
    {
        var equipable = EquipableTemplateModel.FromTemplate<WeaponTemplateModel>(template);
        equipable.Category = template.Category;
        equipable.DamageType = template.DamageType;
        equipable.IsRanged = template.IsRanged;
        equipable.Range = template.Range;
        return equipable;
    }

    public WeaponCategory Category { get; set; }
    public WeaponDamageType DamageType { get; set; }
    public bool IsRanged { get; set; }
    public string? Range { get; set; }
}


