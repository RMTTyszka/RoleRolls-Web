using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities;

public partial class Creature
{
    private static IReadOnlyList<BasicAttackVitalityRule> ResolveBasicAttackVitalityRules(AttackCommand input)
    {
        var vitalityRules = input.GetBasicAttackVitalityRules
            .Where(rule => rule.Vitality != null)
            .Select(rule => rule.Clone())
            .ToList();

        if (vitalityRules.Count == 0)
        {
            throw new InvalidOperationException("Basic attack vitality order is not configured for this campaign.");
        }

        return vitalityRules;
    }

    private static List<VitalityStatusChange> ApplyBasicAttackDamage(
        Creature target,
        int damage,
        IReadOnlyList<BasicAttackVitalityRule> vitalityRules)
    {
        var remainingDamage = damage;
        var triggeredStatuses = new List<VitalityStatusChange>();

        foreach (var vitalityRule in vitalityRules)
        {
            if (remainingDamage <= 0)
            {
                break;
            }

            if (vitalityRule.Vitality == null)
            {
                continue;
            }

            var takeDamageResult = target.TakeDamage(vitalityRule.Vitality.Id, remainingDamage, vitalityRule);
            remainingDamage = takeDamageResult.ExcessDamage;

            if (takeDamageResult.TriggeredStatuses.Count > 0)
            {
                triggeredStatuses.AddRange(takeDamageResult.TriggeredStatuses);
            }
        }

        return triggeredStatuses;
    }
}
