using NetTopologySuite.Index.HPRtree;
using RoleRollsPocketEdition._Application.Powers;
using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;

namespace RoleRollsPocketEdition._Application.Itens.Dtos;

public class ItemModel
{
    public static ItemModel FromItem(ItemInstance item)
    {
        return new ItemModel
        {
            Name = item.Name,
            PowerId = item.PowerId,
            Id = item.Id,
            Level = item.Level,
            TemplateId = item.TemplateId,
            Power = PowerModel.FromPower(item.Power),
            Template = ItemTemplateModel.FromTemplate<ItemTemplateModel>(item.Template),
        };
    }

    public ItemTemplateModel Template { get; set; }

    public PowerModel? Power { get; set; }

    public Guid TemplateId { get; set; }

    public int Level { get; set; }

    public Guid Id { get; set; }

    public Guid? PowerId { get; set; }
    public string Name { get; set; }
}