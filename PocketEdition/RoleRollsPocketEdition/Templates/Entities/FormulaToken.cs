using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Templates.Entities;

public enum FormulaTokenType
{
    Property = 0,
    Number = 1,
    Operator = 2,
    CustomValue = 3
}

public enum FormulaCustomValue
{
    ArmorDefenseBonus = 0,
    Level = 1
}

public class FormulaToken
{
    public int Order { get; set; }
    public FormulaTokenType Type { get; set; }
    public Property? Property { get; set; }
    public string? Operator { get; set; }
    public decimal? Value { get; set; }
    public FormulaCustomValue? CustomValue { get; set; }

    public FormulaToken Clone()
    {
        return new FormulaToken
        {
            Order = Order,
            Type = Type,
            Property = Property is null ? null : new Property(Property.Id, Property.Type),
            Operator = Operator,
            Value = Value,
            CustomValue = CustomValue
        };
    }
}
