using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;

namespace RoleRollsPocketEdition.Bonuses.Models;

public class BonusModel : IEntityDto
{
    public Guid Id { get; set; }
    public int Value { get; set; }
    public BonusValueType ValueType { get; set; }
    public Property Property { get; set; }
    public BonusType Type { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public BonusModel() { }

    public BonusModel(Bonus bonus)
    {
        Id = bonus.Id;
        Value = bonus.Value;
        ValueType = bonus.ValueType;
        Property = bonus.Property;
        Type = bonus.Type;
        Name = bonus.Name;
    }
}