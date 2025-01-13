using RoleRollsPocketEdition.Itens.Dtos.Templates;

namespace RoleRollsPocketEdition.Itens.Templates;

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