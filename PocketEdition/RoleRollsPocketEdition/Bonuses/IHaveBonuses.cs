using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Bonuses;

public interface IHaveBonuses
{
    public List<Bonus> Bonuses { get; set; }
    public Guid Id { get; set; }
}

public static class BonusExtensions
{
    public static List<Bonus> GetBonus(this IHaveBonuses iHaveBonuses, BonusApplication application, BonusType type,
        Property? property)
    {
        return iHaveBonuses.Bonuses
            .Where(bonus => bonus.Application == application &&
                            bonus.Type == type &&
                            (property == null || bonus.Property == property))
            .ToList();
    }

    public static int SumBonus(this IList<Bonus> bonuses, BonusApplication application, Property? property)
    {
        var innate = bonuses
            .Where(bonus => bonus.Application == application && 
                   bonus.Origin == BonusOrigin.Innate &&
                   (property == null || bonus.Property == property))
            .Sum(bonus => bonus.Value);
         
        var magical = bonuses
            .Where(bonus => bonus.Application == application && 
                   bonus.Origin == BonusOrigin.Magical &&
                   (property == null || bonus.Property == property))
            .DefaultIfEmpty(new Bonus { Value = 0 })
            .Max(bonus => bonus.Value);
         
        var equipment = bonuses
            .Where(bonus => bonus.Application == application && 
                   bonus.Origin == BonusOrigin.Equipment &&
                   (property == null || bonus.Property == property))
            .Sum(bonus => bonus.Value);
         
        var moral = bonuses
            .Where(bonus => bonus.Application == application && 
                   bonus.Origin == BonusOrigin.Moral &&
                   (property == null || bonus.Property == property))
            .DefaultIfEmpty(new Bonus { Value = 0 })
            .Max(bonus => bonus.Value);
         
        return innate + magical + equipment + moral;
    }

    public static async Task AddBonus(this IHaveBonuses iHaveBonuses, BonusModel bonusModel, RoleRollsDbContext dbContext)
    {
        var bonus = new Bonus(bonusModel);
        iHaveBonuses.Bonuses.Add(bonus);
        await dbContext.Bonus.AddAsync(bonus);
    }    
    public static void UpdateBonus(this IHaveBonuses iHaveBonuses, BonusModel bonusModel)
    {
        var bonus = iHaveBonuses.Bonuses.First(e => e.Id == bonusModel.Id);
        bonus.Update(bonusModel);
    }

    public static void RemoveBonus(this IHaveBonuses iHaveBonuses, Guid bonusId)
    {
        iHaveBonuses.Bonuses.RemoveAll(b => b.Id == bonusId);
    }
}