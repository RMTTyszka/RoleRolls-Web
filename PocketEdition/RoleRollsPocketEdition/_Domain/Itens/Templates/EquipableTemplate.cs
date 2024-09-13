using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition.Domain.Itens.Models;

namespace RoleRollsPocketEdition.Domain.Itens;

public class EquipableTemplate : ItemTemplate
{
    public EquipableSlot Slot { get; set; }

    public EquipableTemplate()
    {
        
    }

    public EquipableTemplate(EquipableTemplateModel model) : base(model)
    {
        Slot = model.Slot;
    }

    public void Update(EquipableTemplateModel model)
    {
        base.Update(model);
        Slot = model.Slot;
    }
}