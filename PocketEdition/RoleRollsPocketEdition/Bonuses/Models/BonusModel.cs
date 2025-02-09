using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;

namespace RoleRollsPocketEdition.Bonuses.Models;

public class BonusModel(Bonus bonus) : IEntityDto
{
    public Guid Id { get; set; } = bonus.Id;
    public int Value { get; set; } = bonus.Value;
    public BonusValueType ValueType { get; set; } = bonus.ValueType;
    public Property Property { get; set; } = bonus.Property;
    public BonusType Type { get; set; } = bonus.Type;
}