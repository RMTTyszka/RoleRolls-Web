using RoleRollsPocketEdition._Application.Itens.Dtos;

namespace RoleRollsPocketEdition._Domain.Itens.Templates.Models;

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