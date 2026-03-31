using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Templates.Entities;

public class BasicAttackVitalityRule
{
    public Property? Vitality { get; set; }
    public BasicAttackConditionRule? ConditionAtThirtyPercent { get; set; }
    public BasicAttackConditionRule? ConditionAtZero { get; set; }

    public BasicAttackVitalityRule Clone()
    {
        return new BasicAttackVitalityRule
        {
            Vitality = Vitality is null ? null : new Property(Vitality.Id, Vitality.Type),
            ConditionAtThirtyPercent = ConditionAtThirtyPercent?.Clone(),
            ConditionAtZero = ConditionAtZero?.Clone()
        };
    }
}

public class BasicAttackConditionRule
{
    public Property? Condition { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public BasicAttackConditionRule Clone()
    {
        return new BasicAttackConditionRule
        {
            Condition = Condition is null ? null : new Property(Condition.Id, Condition.Type),
            Name = Name,
            Description = Description
        };
    }
}
