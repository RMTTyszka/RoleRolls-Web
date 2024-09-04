using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Domain.Campaigns.Entities;
using RoleRollsPocketEdition.Domain.Powers.Entities;

namespace RoleRollsPocketEdition.Domain.Itens;

public class ItemTemplate : Entity
{
    public string Name { get; set; }
    public Guid? PowerId { get; set; }
    public PowerTemplate? Power { get; set; }
    public ItemType Type { get; set; }
    public Guid? CampaignId { get; set; }
    public Campaign? Campaign { get; set; }
}

public enum ItemType
{
    Consumable = 0,
    Equipable = 1
}