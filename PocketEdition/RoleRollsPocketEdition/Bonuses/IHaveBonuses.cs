using RoleRollsPocketEdition.Archetypes;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Races;

namespace RoleRollsPocketEdition.Bonuses;

public interface IHaveBonuses
{
    public List<Bonus> Bonuses { get; set; }
    public Guid Id { get; set; }
}

public static class BonusExtensions
{
    public static void AddBonus<T>(this T entity, BonusModel bonusModel) where T : IHaveBonuses
    {
        var bonusType = entity.GetType() switch
        {
            var t when t == typeof(ItemInstance) => EntityType.Item,
            var t when t == typeof(Race) => EntityType.Race,
            var t when t == typeof(Archetype) => EntityType.Archetype,
            _ => throw new InvalidOperationException("Unknown entity type")
        };
        var bonus = new Bonus
        {
            EntityId = entity.Id,
            EntityType = bonusType,
            Property = bonusModel.Property,
            Value = bonusModel.Value,
            ValueType = bonusModel.ValueType,
            Type = bonusModel.Type,
        };

        entity.Bonuses.Add(bonus);
    }   
    public static void RemoveBonus<T>(this T entity, Guid id) where T : IHaveBonuses
    {
        entity.Bonuses.RemoveAll(e => e.Id == id);
    }
}