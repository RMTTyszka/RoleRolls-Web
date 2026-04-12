using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities;

public partial class Creature
{
    private IReadOnlyList<BasicAttackVitalityRule> ResolveBasicAttackVitalityRules(Property? prioritizedVitality = null)
    {
        var conditionsById = ResolveCreatureConditionsById();

        var vitalityRules = Vitalities
            .Where(vitality => vitality.VitalityTemplate?.BasicAttackOrder is > 0)
            .OrderBy(vitality => vitality.VitalityTemplate!.BasicAttackOrder)
            .ThenBy(vitality => vitality.Name)
            .Select(vitality => vitality.VitalityTemplate!.ToBasicAttackVitalityRule(
                ResolveCondition(vitality.VitalityTemplate.ConditionAtThirtyPercent, conditionsById),
                ResolveCondition(vitality.VitalityTemplate.ConditionAtZero, conditionsById)))
            .Where(rule => rule != null)
            .Select(rule => rule!.Clone())
            .ToList();

        if (prioritizedVitality != null)
        {
            var overrideRule = vitalityRules.FirstOrDefault(rule => rule.Vitality?.Id == prioritizedVitality.Id)?.Clone() ??
                               new BasicAttackVitalityRule
                               {
                                   Vitality = new Property(prioritizedVitality.Id,
                                       prioritizedVitality.Type ?? PropertyType.Vitality)
                               };

            var reorderedRules = new List<BasicAttackVitalityRule> { overrideRule };
            reorderedRules.AddRange(vitalityRules
                .Where(rule => rule.Vitality?.Id != overrideRule.Vitality?.Id)
                .Select(rule => rule.Clone()));

            vitalityRules = reorderedRules;
        }

        if (vitalityRules.Count == 0)
        {
            throw new InvalidOperationException("Basic attack vitality order is not configured for this campaign.");
        }

        return vitalityRules;
    }

    private IReadOnlyDictionary<Guid, CreatureCondition> ResolveCreatureConditionsById()
    {
        var campaignConditions = Campaign?.CampaignTemplate?.CreatureConditions;
        if (campaignConditions is { Count: > 0 })
        {
            return campaignConditions.ToDictionary(condition => condition.Id);
        }

        var fallbackConditions = Vitalities
            .Select(vitality => vitality.VitalityTemplate?.CampaignTemplate?.CreatureConditions)
            .FirstOrDefault(conditions => conditions is { Count: > 0 });

        if (fallbackConditions is { Count: > 0 })
        {
            return fallbackConditions.ToDictionary(condition => condition.Id);
        }

        return new Dictionary<Guid, CreatureCondition>();
    }

    private static CreatureCondition? ResolveCondition(
        Property? conditionProperty,
        IReadOnlyDictionary<Guid, CreatureCondition> conditionsById)
    {
        if (conditionProperty == null)
        {
            return null;
        }

        return conditionsById.GetValueOrDefault(conditionProperty.Id);
    }

    private static void ApplyBasicAttackDamage(
        Creature target,
        int damage,
        Property? prioritizedVitality = null)
    {
        var remainingDamage = damage;
        var vitalityRules = target.ResolveBasicAttackVitalityRules(prioritizedVitality);

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

            var takeDamageResult = target.TakeDamage(vitalityRule.Vitality.Id, remainingDamage);
            remainingDamage = takeDamageResult.ExcessDamage;
        }
    }
}
