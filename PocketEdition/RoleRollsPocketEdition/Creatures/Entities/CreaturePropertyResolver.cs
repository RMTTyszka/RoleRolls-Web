using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities;

public record PropertyInput(
    Property? Property,
    Property? OverriderAttribute = null
);

public static class CreatureExtensions
{
    public static PropertyValue GetPropertyValue(this Creature creature, PropertyInput input)
    {
        if (input.Property == null)
            return new PropertyValue();

        var result = new PropertyValue();
        var property = input.Property;

        switch (property.Type)
        {
            case PropertyType.Attribute:
                result.Value = creature.GetAttributeValue(property.Id, input);
                break;

            case PropertyType.Skill:
                creature.ProcessSkillProperty(property.Id, result, input);
                break;

            case PropertyType.MinorSkill:
                creature.ProcessMinorSkillProperty(property.Id, result, input);
                break;

            default:
                creature.ProcessUnknownPropertyType(property, result, input);
                break;
        }

        creature.ApplyBonuses(result, property);
        return result;
    }

    private static int GetAttributeValue(this Creature creature, Guid attributeId, PropertyInput input)
    {
        var attribute = creature.Attributes.First(at => at.AttributeTemplateId == attributeId);
        
        if (input.OverriderAttribute != null)
        {
            var overrideAttr = creature.Attributes.FirstOrDefault(at => at.AttributeTemplateId == input.OverriderAttribute.Id);
            return overrideAttr?.Value ?? attribute.Value;
        }
        
        return attribute.Value;
    }

    private static Attribute FindAttribute(this Creature creature, Guid id)
    {
        return creature.Attributes.First(at => at.AttributeTemplateId == id);
    }

    private static Attribute? FindAttributeOrDefault(this Creature creature, Guid id)
    {
        return creature.Attributes.FirstOrDefault(at => at.AttributeTemplateId == id);
    }

    private static void ProcessSkillProperty(this Creature creature, Guid skillId, PropertyValue result, PropertyInput input)
    {
        var skill = creature.Skills.Concat(creature.AttributelessSkills).First(sk => sk.Id == skillId);
        
        if (skill.AttributeId.HasValue)
        {
            result.Value = creature.GetAttributeValue(skill.AttributeId.Value, input);
        }
        
        result.Bonus = 0;
    }

    private static void ProcessMinorSkillProperty(this Creature creature, Guid minorSkillId, PropertyValue result, PropertyInput input)
    {
        var minorSkill = creature.SpecificSkills.First(ms => ms.SpecificSkillTemplateId == minorSkillId);
        var parentSkill = creature.Skills.First(sk => sk.Id == minorSkill.SkillId);
        
        if (parentSkill.AttributeId.HasValue)
        {
            var attribute = creature.Attributes.First(at => at.Id == parentSkill.AttributeId.Value);
            result.Value = creature.GetAttributeValue(attribute.AttributeTemplateId, input);
        }
        else if (minorSkill.AttributeId.HasValue)
        {
            var attribute = creature.Attributes.First(at => at.Id == minorSkill.AttributeId.Value);
            result.Value = creature.GetAttributeValue(attribute.AttributeTemplateId, input);
        }
        
        
        result.Bonus = minorSkill.Points;
    }

    private static void ProcessUnknownPropertyType(this Creature creature, Property property, PropertyValue result, PropertyInput input)
    {
        if (creature.TryProcessAsAttribute(property, result, input))
            return;
        
        if (creature.TryProcessAsSkill(property, result, input))
            return;
        
        creature.TryProcessAsMinorSkill(property, result, input);
    }

    private static bool TryProcessAsAttribute(this Creature creature, Property property, PropertyValue result, PropertyInput input)
    {
        var attr = creature.FindAttributeOrDefault(property.Id);
        
        if (attr == null)
            return false;
        
        result.Value = input.OverriderAttribute != null
            ? creature.FindAttributeOrDefault(input.OverriderAttribute.Id)?.Value ?? attr.Value
            : attr.Value;
        
        return true;
    }

    private static bool TryProcessAsSkill(this Creature creature, Property property, PropertyValue result, PropertyInput input)
    {
        var skill = creature.Skills.FirstOrDefault(sk => sk.Id == property.Id);
        
        if (skill is not { AttributeId: not null })
            return false;
        
        result.Value = creature.GetAttributeValue(skill.AttributeId.Value, input);
        result.Bonus = 0;
        return true;
    }

    private static void TryProcessAsMinorSkill(this Creature creature, Property property, PropertyValue result, PropertyInput input)
    {
        var minorSkill = creature.Skills.SelectMany(skill => skill.SpecificSkills)
            .Concat(creature.AttributelessSkills.SelectMany(s => s.SpecificSkills))
            .FirstOrDefault(ms => ms.Id == property.Id);
        
        if (minorSkill == null)
            return;
        
        var parentSkill = creature.Skills.First(sk => sk.Id == minorSkill.SkillId);
        
        if (parentSkill.AttributeId.HasValue)
        {
            result.Value = creature.GetAttributeValue(parentSkill.AttributeId.Value, input);
            result.Bonus = minorSkill.Points;
        }
    }

    private static void ApplyBonuses(this Creature creature, PropertyValue result, Property property)
    {
        result.Value += creature.GetTotalBonus(BonusApplication.Property, BonusType.Advantage, property);
        result.Bonus += creature.GetTotalBonus(BonusApplication.Property, BonusType.Buff, property);
    }
}