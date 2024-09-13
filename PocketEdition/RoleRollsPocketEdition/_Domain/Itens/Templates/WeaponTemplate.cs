using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition.Domain.Itens.Models;

namespace RoleRollsPocketEdition.Domain.Itens;

public class WeaponTemplate : EquipableTemplate
{
    public WeaponSize Size { get; set; }
    public WeaponTemplate()
    {
        
    }
    public WeaponTemplate(WeaponTemplateModel item) : base(item)
    {
        Size = item.Size;
    }

    public void Update(WeaponTemplateModel item)
    {
        base.Update(item);
        Size = item.Size;
    }
    public override object ToUpperClass()
    {
        return new WeaponTemplateModel(this);
    }
}