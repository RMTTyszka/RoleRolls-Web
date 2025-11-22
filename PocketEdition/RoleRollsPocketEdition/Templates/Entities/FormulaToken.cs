using System.Text.Json.Serialization;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Templates.Entities.Json;

namespace RoleRollsPocketEdition.Templates.Entities;

[System.Text.Json.Serialization.JsonConverter(typeof(FormulaTokenTypeJsonConverter))]
public enum FormulaTokenType
{
    Property = 0,
    Creature = 3,
    Manual = 4,
    Equipment = 5
}

public enum FormulaCreatureValue
{
    ArmorDefenseBonus = 0,
    Level = 1,
    DefenseBonus1 = 2,
    DefenseBonus2 = 3,
    ArmorBonus = 4
}

public enum FormulaEquipmentValue
{
    LevelBonus = 0,
    DefenseBonus1 = 1,
    DefenseBonus2 = 2
}

public class FormulaToken
{
    public int Order { get; set; }
    public FormulaTokenType Type { get; set; }
    public Property? Property { get; set; }
    public string? Operator { get; set; }
    public decimal? Value { get; set; }

    [JsonPropertyName("customValue")]
    public FormulaCreatureValue? CreatureValue { get; set; }

    public EquipableSlot? EquipmentSlot { get; set; }
    public FormulaEquipmentValue? EquipmentValue { get; set; }
    public string? ManualValue { get; set; }

    public FormulaToken Clone()
    {
        return new FormulaToken
        {
            Order = Order,
            Type = Type,
            Property = Property is null ? null : new Property(Property.Id, Property.Type),
            Operator = Operator,
            Value = Value,
            CreatureValue = CreatureValue,
            EquipmentSlot = EquipmentSlot,
            EquipmentValue = EquipmentValue,
            ManualValue = ManualValue
        };
    }
}
