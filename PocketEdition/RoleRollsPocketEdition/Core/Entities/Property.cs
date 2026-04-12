using RoleRollsPocketEdition.Creatures.Models;

namespace RoleRollsPocketEdition.Core.Entities;

public record Property(Guid Id, PropertyType? Type = null);

public enum PropertyType
{
    All = 0,
    Attribute = 1,
    Skill = 2,
    MinorSkill = 3,
    Defense = 4,
    Vitality = 5,
    CreatureCondition = 6,
}
public enum PropertyValueOrigin
{
    CreatureProperty = 0,
    TemplateProperty = 1
}
public class PropertyValue
{
    public int Value { private get; set; }
    public int GetValue => Value;
    public int Bonus { private get; set; }

    public void AddBonusValue(int bonus)
    {
        Value += bonus;
    }   
    public void AddBonusBonus(int bonus)
    {
        Bonus += bonus;
    }

    public int Total => Value + Bonus;
}


