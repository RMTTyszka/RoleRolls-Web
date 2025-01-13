using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Itens.Templates.Models;

namespace RoleRollsPocketEdition.Itens.Dtos.Templates;

public class EquipableTemplateModel : ItemTemplateModel
{
    public EquipableSlot Slot { get; set; }

    public EquipableTemplateModel() : base()
    {
        
    }

    public static T FromTemplate<T>(EquipableTemplate template) where T : EquipableTemplateModel, new()
    {
        var equipable = ItemTemplateModel.FromTemplate<T>(template);
        equipable.Slot = template.Slot;
        return equipable;
    }
}