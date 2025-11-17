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

            case PropertyType.Defense:
                ProcessDefenseProperty(property.Id, result);
                break;

            case PropertyType.Vitality:
                ProcessVitalityProperty(property.Id, result);
                break;

            default:
                ProcessUnknownPropertyType(property, result, input);
                break;
        }

        ApplyBonuses(result, property);
        return result;
    }

    private int GetAttributeValue(Guid? attributeId, PropertyInput input)
    {
        if (input.OverriderAttribute != null)
        {
            var overrideAttr = Attributes.FirstOrDefault(at => at.AttributeTemplateId == input.OverriderAttribute.Id);
            return overrideAttr?.Points ?? 0;
        }
        var attribute = Attributes.First(at => at.AttributeTemplateId == attributeId);
        return attribute.Points;
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
        var skill = Skills.First(sk => sk.Id == skillId);
        
        result.Value = GetAttributeValue(null, input);
        
        result.Bonus = 0;
    }

    private void ProcessMinorSkillProperty(Guid minorSkillId, PropertyValue result, PropertyInput input)
    {
        var minorSkill = SpecificSkills.First(ms => ms.SpecificSkillTemplateId == minorSkillId);
        
        if (minorSkill.AttributeId.HasValue)
        {
            var attribute = Attributes.First(at => at.Id == minorSkill.AttributeId.Value);
            result.Value = GetAttributeValue(attribute.AttributeTemplateId, input);
        }
        
        
        result.Bonus = minorSkill.Points;
    }

    private void ProcessDefenseProperty(Guid defenseId, PropertyValue result)
    {
        var defense = Defenses.FirstOrDefault(d => d.Id == defenseId || d.DefenseTemplateId == defenseId);
        if (defense is null)
        {
            return;
        }

        result.Value = ApplyFormula(defense.Formula, defense.FormulaTokens);
        result.Bonus = 0;
    }

    private void ProcessVitalityProperty(Guid vitalityId, PropertyValue result)
    {
        var vitality = Vitalities.FirstOrDefault(v => v.Id == vitalityId || v.VitalityTemplateId == vitalityId);
        if (vitality is null)
        {
            return;
        }

        result.Value = vitality.CalculateMaxValue(this);
        result.Bonus = 0;
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
            ? FindAttributeOrDefault(input.OverriderAttribute.Id)?.Points ?? attr.Points
            : attr.Points;
        
        return true;
    }

    private bool TryProcessAsSkill(Property property, PropertyValue result, PropertyInput input)
    {
        var skill = Skills.FirstOrDefault(sk => sk.Id == property.Id);
        
        result.Value = GetAttributeValue(null, input);
        result.Bonus = 0;
        return true;
    }

    private void TryProcessAsMinorSkill(Property property, PropertyValue result, PropertyInput input)
    {
        var minorSkill = Skills.SelectMany(skill => skill.SpecificSkills)
            .FirstOrDefault(ms => ms.SpecificSkillTemplateId == property.Id);

        if (minorSkill?.Attribute != null)
        {
            result.Value = GetAttributeValue(minorSkill.Attribute.AttributeTemplateId, input);
            result.Bonus = minorSkill.Points;
        }
    }

    private void ApplyBonuses(PropertyValue result, Property property)
    {
        result.Value += GetTotalBonus(BonusApplication.Property, BonusType.Advantage, property);
        result.Bonus += GetTotalBonus(BonusApplication.Property, BonusType.Buff, property);
    }

    private int MaxAttributePoints => 4 + Level / 6;
}


