using RoleRollsPocketEdition._Application.Itens.Dtos;

namespace RoleRollsPocketEdition._Domain.Itens.Templates.Models;

public class ArmorTemplateModel : EquipableTemplateModel
{
    public ArmorTemplateModel() : base()
    {
        
    }
    public ArmorTemplateModel(ArmorTemplate template) : base(template)
    {
        Category = template.Category;       
    }

    public ArmorCategory Category { get; set; }
}