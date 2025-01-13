using RoleRollsPocketEdition.Itens.Templates.Models;

namespace RoleRollsPocketEdition.Itens.Templates;

public class ConsumableTemplate : ItemTemplate
{

    public ConsumableTemplate()
    {
        
    }

    public ConsumableTemplate(ConsumableTemplateModel template) : base(template)
    {
    }
    public virtual object ToUpperClass()
    {
        return ItemTemplateModel.FromTemplate<ItemTemplateModel>(this);
    }
}