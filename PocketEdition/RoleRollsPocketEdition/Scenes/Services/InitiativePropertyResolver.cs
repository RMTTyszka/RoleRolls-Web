using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Scenes.Services;

public static class InitiativePropertyResolver
{
    public static int GetValue(Creature creature, Guid propertyId)
    {
        var propertyType = ResolveType(creature, propertyId);
        return creature.GetPropertyValue(new PropertyInput(new Property(propertyId, propertyType))).Total;
    }

    private static PropertyType ResolveType(Creature creature, Guid propertyId)
    {
        if (creature.Attributes.Any(attribute => attribute.AttributeTemplateId == propertyId))
            return PropertyType.Attribute;

        if (creature.Skills.Any(skill => skill.SkillTemplateId == propertyId))
            return PropertyType.Skill;

        if (creature.SpecificSkills.Any(skill => skill.SpecificSkillTemplateId == propertyId))
            return PropertyType.MinorSkill;

        if (creature.Defenses.Any(defense => defense.DefenseTemplateId == propertyId || defense.Id == propertyId))
            return PropertyType.Defense;

        if (creature.Vitalities.Any(vitality => vitality.VitalityTemplateId == propertyId || vitality.Id == propertyId))
            return PropertyType.Vitality;

        throw new InvalidOperationException(
            $"Initiative property {propertyId} is not available for creature {creature.Id}.");
    }
}
