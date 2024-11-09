using RoleRollsPocketEdition._Domain.Itens.Templates.Models;

namespace RoleRollsPocketEdition._Domain.Itens.Templates;

public class ConsumableTemplate : ItemTemplate
{
    public void Update(ItemTemplateModel item)
    {
        base.Update(item);
    }
    public virtual object ToUpperClass()
    {
        return ItemTemplateModel.FromTemplate<ItemTemplateModel>(this);
    }
}