using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Itens.Templates.Models;
using RoleRollsPocketEdition.Powers;

namespace RoleRollsPocketEdition.Itens.Dtos;

public class ItemModel
{
    protected ItemModel(ItemInstance item)
    {
        Name = item.Name;
        PowerInstanceId = item.PowerInstanceId;
        Id = item.Id;
        Level = item.Level;
        TemplateId = item.TemplateId;
        PowerInstance = PowerInstanceModel.FromPower(item.PowerInstance);
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

    public PowerInstanceModel? PowerInstance { get; set; }

    public Guid TemplateId { get; set; }

    public int Level { get; set; }

    public Guid Id { get; set; }

    public Guid? PowerInstanceId { get; set; }
    public string Name { get; set; }
}


