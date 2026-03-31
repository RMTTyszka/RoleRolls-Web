using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Templates.Entities;

public class BasicAttackVitalityRule
{
    public Property? Vitality { get; set; }
    public string? StatusAtThirtyPercent { get; set; }
    public string? StatusAtZero { get; set; }

    public BasicAttackVitalityRule Clone()
    {
        return new BasicAttackVitalityRule
        {
            Vitality = Vitality is null ? null : new Property(Vitality.Id, Vitality.Type),
            StatusAtThirtyPercent = StatusAtThirtyPercent,
            StatusAtZero = StatusAtZero
        };
    }
}
