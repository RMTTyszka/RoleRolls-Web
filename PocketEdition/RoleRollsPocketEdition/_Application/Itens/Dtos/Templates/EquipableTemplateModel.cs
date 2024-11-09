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

    public static T FromTemplate<T>(EquipableTemplate template) where T : EquipableTemplateModel, new()
    {
        var equipable = ItemTemplateModel.FromTemplate<T>(template);
        equipable.Slot = template.Slot;
        return equipable;
    }
}