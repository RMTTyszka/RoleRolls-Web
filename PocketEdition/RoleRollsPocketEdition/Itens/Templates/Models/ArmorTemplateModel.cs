using RoleRollsPocketEdition.Itens.Dtos.Templates;

namespace RoleRollsPocketEdition.Itens.Templates.Models;

public class ArmorTemplateModel : EquipableTemplateModel
{
    public ArmorTemplateModel() : base()
    {
        
    }
    public static ArmorTemplateModel FromTemplate(ArmorTemplate template)
    {
        var armor = EquipableTemplateModel.FromTemplate<ArmorTemplateModel>(template);
        armor.Category = template.Category;
        return armor;
    }

    public ArmorCategory Category { get; set; }
}