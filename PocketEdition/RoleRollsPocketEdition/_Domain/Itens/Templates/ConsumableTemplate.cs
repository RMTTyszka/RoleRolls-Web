using RoleRollsPocketEdition._Domain.Itens.Templates.Models;

namespace RoleRollsPocketEdition._Domain.Itens.Templates;

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