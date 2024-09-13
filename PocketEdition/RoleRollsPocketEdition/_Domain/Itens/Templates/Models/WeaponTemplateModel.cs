using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Domain.Itens;

namespace RoleRollsPocketEdition.Domain.Itens.Models;

public class WeaponTemplateModel : EquipableTemplateModel
{
    public WeaponTemplateModel() : base()
    {
        
    }
    public WeaponTemplateModel(WeaponTemplate weaponTemplate) : base(weaponTemplate)
    {
        Size = weaponTemplate.Size;
    }

    public WeaponSize Size { get; set; }
}