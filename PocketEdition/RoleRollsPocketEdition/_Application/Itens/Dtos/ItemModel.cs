using NetTopologySuite.Index.HPRtree;
using RoleRollsPocketEdition._Application.Powers;
using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;

namespace RoleRollsPocketEdition._Application.Itens.Dtos;

public class ItemModel
{
    protected ItemModel(ItemInstance item)
    {
        Name = item.Name;
        PowerId = item.PowerId;
        Id = item.Id;
        Level = item.Level;
        TemplateId = item.TemplateId;
        Power = PowerModel.FromPower(item.Power);
        Template = item.Template switch
        {
            WeaponTemplate template => WeaponTemplateModel.FromTemplate(template),
            ArmorTemplate template => ArmorTemplateModel.FromTemplate(template),
            _ => ItemTemplateModel.FromTemplate<ConsumableTemplateModel>(item.Template)
        };
    }
    public static ItemModel? FromItem(ItemInstance? item)
    {
        if (item == null)
        {
            return null;
        }

        return new ItemModel(item);
    }

    public ItemTemplateModel Template { get; set; }

    public PowerModel? Power { get; set; }

    public Guid TemplateId { get; set; }

    public int Level { get; set; }

    public Guid Id { get; set; }

    public Guid? PowerId { get; set; }
    public string Name { get; set; }
}