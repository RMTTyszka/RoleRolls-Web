using RoleRollsPocketEdition.Bonuses.Models;

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
        var bonus = new Bonus
        {
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