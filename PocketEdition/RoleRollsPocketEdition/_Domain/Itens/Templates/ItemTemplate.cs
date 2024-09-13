using RoleRollsPocketEdition._Domain.Itens;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Domain.Campaigns.Entities;
using RoleRollsPocketEdition.Domain.Itens.Models;
using RoleRollsPocketEdition.Domain.Powers.Entities;

namespace RoleRollsPocketEdition.Domain.Itens;

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

    public virtual object ToUpperClass()
    {
        return new ItemTemplateModel(this);
    }
}

public enum ItemType
{
    Consumable = 0,
    Weapon = 1,
    Armor = 2
}