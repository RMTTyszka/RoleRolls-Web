using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Bonuses;

public class Bonus : Entity
{
    public int Value { get; set; }
    public Property Property { get; set; }
    public Guid EntityId { get; set; }
    public BonusValueType ValueType { get; set; }
    public EntityType EntityType { get; set; }
    public BonusType Type { get; set; }

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