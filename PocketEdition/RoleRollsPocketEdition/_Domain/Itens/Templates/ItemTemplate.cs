using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;
using RoleRollsPocketEdition._Domain.Powers.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Itens.Templates;

public class ItemTemplate : Entity
{
    public ItemTemplate()
    {
        
    }
    public ItemTemplate(ItemTemplateModel item)
    {
        Id = item.Id;
        Name = item.Name;
        PowerId = item.PowerId;
        Type = item.Type;
        CampaignId = item.CampaignId;
    }

    public string Name { get; set; }
    public Guid? PowerId { get; set; }
    public PowerTemplate? Power { get; set; }
    public ItemType Type { get; set; }
    public Guid? CampaignId { get; set; }
    public Campaign? Campaign { get; set; }


    public void Update(ItemTemplateModel item)
    {
        Name = item.Name;
        PowerId = item.PowerId;
    }

    public virtual T ToUpperClass<T>() where T : ItemTemplateModel, new()
    {
        return ItemTemplateModel.FromTemplate<T>(this);
    }

    public ItemInstance Instantiate(ItemInstanceUpdate input)
    {
        return new ItemInstance
        {
            Id = Guid.NewGuid(),
            Name = input.Name ?? Name,
            PowerId = PowerId,
            Level = input.Level,
            TemplateId = Id,
            Template = this
        };
    }
}

public enum ItemType
{
    Consumable = 0,
    Weapon = 1,
    Armor = 2,
}