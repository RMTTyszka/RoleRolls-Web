using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Bonuses;

public interface IHaveBonuses
{
    public List<Bonus> Bonuses { get; set; }
    public Guid Id { get; set; }
}

public static class BonusExtensions
{
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