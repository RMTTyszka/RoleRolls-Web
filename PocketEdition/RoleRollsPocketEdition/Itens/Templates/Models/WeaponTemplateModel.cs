using RoleRollsPocketEdition.Itens.Dtos.Templates;

namespace RoleRollsPocketEdition.Itens.Templates.Models;

public class WeaponTemplateModel : EquipableTemplateModel
{
    public WeaponTemplateModel() : base()
    {
        
    }
    public static WeaponTemplateModel FromTemplate(WeaponTemplate template)
    {
        var equipable = ItemTemplateModel.FromTemplate<WeaponTemplateModel>(template);
        equipable.Category = template.Category;
        equipable.DamageType = template.DamageType;
        return equipable;
    }

    public WeaponCategory Category { get; set; }
    public WeaponDamageType DamageType { get; set; }
}


