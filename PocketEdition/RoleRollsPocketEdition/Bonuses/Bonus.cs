using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Bonuses;

public class Bonus : Entity
{
    public Bonus()
    {
        
    }
    public Bonus(BonusModel bonusModel)
    {
        Id = bonusModel.Id;
        Value = bonusModel.Value;
        Property = bonusModel.Property;
        Application = bonusModel.Application;
        Origin = bonusModel.Origin;
        Name = bonusModel.Name;
        Description = bonusModel.Description;
    }

    public string Description { get; set; } = "";

    public int Value { get; set; }
    public bool Active { get; set; }
    public Property? Property { get; set; }
    public string Name { get; set; } = "";
    public BonusApplication Application { get; set; }
    public BonusOrigin Origin { get; set; }
    public BonusType Type { get; set; }

    public void Update(BonusModel bonusModel)
    {
        Value = bonusModel.Value;
        Property = bonusModel.Property;
        Application = bonusModel.Application;
        Origin = bonusModel.Origin;
        Description = bonusModel.Description;
        Name = bonusModel.Name;
    }
}

public enum BonusOrigin   
{
    Innate = 0,
    Magical = 1,
    Equipment = 2,
    Moral = 3
}

public enum BonusApplication
{
    Property = 0,
    Critical = 1,
    Hit = 2,
    Defense = 3
}

public enum BonusType
{
    Advantage = 0,
    Value = 1,
}