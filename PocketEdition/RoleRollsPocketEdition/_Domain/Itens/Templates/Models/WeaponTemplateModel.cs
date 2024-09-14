using RoleRollsPocketEdition._Application.Itens.Dtos;

namespace RoleRollsPocketEdition._Domain.Itens.Templates.Models;

public class WeaponTemplateModel : EquipableTemplateModel
{
    public WeaponTemplateModel() : base()
    {
        
    }
    public WeaponTemplateModel(WeaponTemplate weaponTemplate) : base(weaponTemplate)
    {
        Category = weaponTemplate.Category;
    }

    public WeaponCategory Category { get; set; }
    public WeaponDamageType DamageType { get; set; }
}