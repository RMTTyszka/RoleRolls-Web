namespace RoleRollsPocketEdition.Itens.Templates.Models;

public class ConsumableTemplateModel : ItemTemplateModel
{
    public static ConsumableTemplateModel FromTemplate(WeaponTemplate template)
    {
        var consumable = ItemTemplateModel.FromTemplate<ConsumableTemplateModel>(template);
        return consumable;
    }
}


