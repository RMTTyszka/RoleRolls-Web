using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities;

public record PropertyInput(
    Property? Property,
    Property? OverriderAttribute = null
);

public partial class Creature
{
    public PropertyValue GetPropertyValue(PropertyInput input)
    {
        if (input.Property == null)
            return new PropertyValue();

        var result = new PropertyValue();
        var property = input.Property;

        switch (property.Type)
        {
            case PropertyType.Attribute:
                result.Value = GetAttributeValue(property.Id, input);
                break;

            case PropertyType.Skill:
                ProcessSkillProperty(property.Id, result, input);
                break;

            case PropertyType.MinorSkill:
                ProcessMinorSkillProperty(property.Id, result, input);
                break;

            default:
                ProcessUnknownPropertyType(property, result, input);
                break;
        }

        ApplyBonuses(result, property);
        return result;
    }

    private int GetAttributeValue( Guid attributeId, PropertyInput input)
    {
        var attribute = Attributes.First(at => at.AttributeTemplateId == attributeId);
        
        if (input.OverriderAttribute != null)
        {
            var overrideAttr = Attributes.FirstOrDefault(at => at.AttributeTemplateId == input.OverriderAttribute.Id);
            return overrideAttr?.Value ?? attribute.Value;
        }
        
        return attribute.Value;
    }

    private Attribute FindAttribute(Guid id)
    {
        return Attributes.First(at => at.AttributeTemplateId == id);
    }

    private Attribute? FindAttributeOrDefault(Guid id)
    {
        return Attributes.FirstOrDefault(at => at.AttributeTemplateId == id);
    }

    private void ProcessSkillProperty(Guid skillId, PropertyValue result, PropertyInput input)
    {
        var skill = Skills.Concat(AttributelessSkills).First(sk => sk.Id == skillId);
        
        if (skill.AttributeId.HasValue)
        {
            result.Value = GetAttributeValue(skill.AttributeId.Value, input);
        }
        
        result.Bonus = 0;
    }

    private void ProcessMinorSkillProperty(Guid minorSkillId, PropertyValue result, PropertyInput input)
    {
        var minorSkill = SpecificSkills.First(ms => ms.SpecificSkillTemplateId == minorSkillId);
        var parentSkill = Skills.First(sk => sk.Id == minorSkill.SkillId);
        
        if (parentSkill.AttributeId.HasValue)
        {
            var attribute = Attributes.First(at => at.Id == parentSkill.AttributeId.Value);
            result.Value = GetAttributeValue(attribute.AttributeTemplateId, input);
        }
        else if (minorSkill.AttributeId.HasValue)
        {
            var attribute = Attributes.First(at => at.Id == minorSkill.AttributeId.Value);
            result.Value = GetAttributeValue(attribute.AttributeTemplateId, input);
        }
        
        
        result.Bonus = minorSkill.Points;
    }

    private void ProcessUnknownPropertyType(Property property, PropertyValue result, PropertyInput input)
    {
        if (TryProcessAsAttribute(property, result, input))
            return;
        
        if (TryProcessAsSkill(property, result, input))
            return;
        
        TryProcessAsMinorSkill(property, result, input);
    }

    private bool TryProcessAsAttribute(Property property, PropertyValue result, PropertyInput input)
    {
        var attr = FindAttributeOrDefault(property.Id);
        
        if (attr == null)
            return false;
        
        result.Value = input.OverriderAttribute != null
            ? FindAttributeOrDefault(input.OverriderAttribute.Id)?.Value ?? attr.Value
            : attr.Value;
        
        return true;
    }

    private bool TryProcessAsSkill(Property property, PropertyValue result, PropertyInput input)
    {
        var skill = Skills.FirstOrDefault(sk => sk.Id == property.Id);
        
        if (skill is not { AttributeId: not null })
            return false;
        
        result.Value = GetAttributeValue(skill.AttributeId.Value, input);
        result.Bonus = 0;
        return true;
    }

    private void TryProcessAsMinorSkill(Property property, PropertyValue result, PropertyInput input)
    {
        var minorSkill = Skills.SelectMany(skill => skill.SpecificSkills)
            .Concat(AttributelessSkills.SelectMany(s => s.SpecificSkills))
            .FirstOrDefault(ms => ms.Id == property.Id);
        
        if (minorSkill == null)
            return;
        
        var parentSkill = Skills.First(sk => sk.Id == minorSkill.SkillId);
        
        if (parentSkill.AttributeId.HasValue)
        {
            result.Value = GetAttributeValue(parentSkill.AttributeId.Value, input);
            result.Bonus = minorSkill.Points;
        }
    }

    private void ApplyBonuses(PropertyValue result, Property property)
    {
        result.Value += GetTotalBonus(BonusApplication.Property, BonusType.Advantage, property);
        result.Bonus += GetTotalBonus(BonusApplication.Property, BonusType.Buff, property);
    }
}