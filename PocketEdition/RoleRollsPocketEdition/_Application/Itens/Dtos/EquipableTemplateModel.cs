using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;

namespace RoleRollsPocketEdition._Application.Itens.Dtos;

public class EquipableTemplateModel : ItemTemplateModel
{
    public EquipableSlot Slot { get; set; }

    public EquipableTemplateModel() : base()
    {
        
    }

    public EquipableTemplateModel(EquipableTemplate template) : base(template)
    {
        Slot = template.Slot;
    }

}