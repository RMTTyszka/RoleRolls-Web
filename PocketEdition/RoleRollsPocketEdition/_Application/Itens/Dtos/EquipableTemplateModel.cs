using RoleRollsPocketEdition.Domain.Itens;
using RoleRollsPocketEdition.Domain.Itens.Models;

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