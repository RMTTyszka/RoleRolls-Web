using RoleRollsPocketEdition._Application.Itens.Dtos;

namespace RoleRollsPocketEdition._Domain.Itens.Templates.Models;

public class ItemTemplateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? PowerId { get; set; }
    public ItemType Type { get; set; }
    public Guid? CampaignId { get; set; }


    public static T FromTemplate<T>(ItemTemplate entity) where T : ItemTemplateModel, new()
    {
        return new T
        {
            Id = entity.Id,
            Name = entity.Name,
            PowerId = entity.PowerId,
            Type = entity.Type,
            CampaignId = entity.CampaignId,
        };
    }
}