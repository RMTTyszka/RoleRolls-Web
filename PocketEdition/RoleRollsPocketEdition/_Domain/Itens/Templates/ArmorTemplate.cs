using RoleRollsPocketEdition._Domain.Itens.Templates.Models;

namespace RoleRollsPocketEdition._Domain.Itens.Templates;

public class ArmorTemplate : EquipableTemplate
{
    public ArmorTemplate()
    {
        
    }
    public ArmorCategory Category { get; set; }
    public ArmorTemplate(ArmorTemplateModel item) : base(item)
    {
        Category = item.Category;
    }

    public void Update(ArmorTemplateModel item)
    {
        base.Update(item);
        Category = item.Category;
    }
    public override object ToUpperClass()
    {
        return new ArmorTemplateModel(this);
    }
}