using RoleRollsPocketEdition.Archetypes;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.CreatureTypes.Entities;

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
        ValueType = bonusModel.ValueType;
        Type = bonusModel.Type;
    }    
    public int Value { get; set; }
    public Property Property { get; set; }
    public BonusValueType ValueType { get; set; }
    public BonusType Type { get; set; }

    public void Update(BonusModel bonusModel)
    {
        Value = bonusModel.Value;
        Property = bonusModel.Property;
        ValueType = bonusModel.ValueType;
        Type = bonusModel.Type;
    }
}

public enum BonusType
{
    Innate = 0
}

public enum BonusValueType
{
    Dices = 0,
    Roll = 1
}