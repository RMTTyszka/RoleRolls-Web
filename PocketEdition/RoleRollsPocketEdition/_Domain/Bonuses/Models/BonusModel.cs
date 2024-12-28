using RoleRollsPocketEdition._Domain.Global;

namespace RoleRollsPocketEdition._Domain.Bonuses.Models;

public class BonusModel
{
    public Guid Id { get; set; }
    public int Value { get; set; }
    public BonusValueType ValueType { get; set; }
    public Property Property { get; set; }
    public BonusType Type { get; set; }

}