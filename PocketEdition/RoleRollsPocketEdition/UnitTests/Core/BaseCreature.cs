using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;
using Attribute = RoleRollsPocketEdition.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition.UnitTests.Core;

public static class BaseCreature
{
    public static Creature CreateCreature(List<Guid> ids, List<int> values)
    {
        var creatureTemplate = LandOfHeroesTemplate.Template;

        var creature = Creature.FromTemplate(creatureTemplate, Guid.Empty, CreatureCategory.Hero, false);

        return creature;
    }

    private static CampaignTemplate CreateCreatureTemplate(List<Guid> ids)
    {
        var creatureTemplate = new CampaignTemplate();
        foreach (var id in ids)
        {
            creatureTemplate.Attributes.Add(CreateAttributeTemplate(id));
        }

        foreach (var id in ids)
        {
            creatureTemplate.AddAttributelessSkill(CreateSkillTemplate(id, null));
        }

        return creatureTemplate;
    }

    private static AttributeTemplate CreateAttributeTemplate(Guid id)
    {
        var attributeTemplate = new AttributeTemplate
        {
            Id = id,
        };
        attributeTemplate.AddSkill(CreateSkillTemplate(id, attributeTemplate.Id));
        return attributeTemplate;
    }

    private static SkillTemplateModel CreateSkillTemplate(Guid id,
        Guid? attributeTemplateId)
    {
        var skillTemplate = new SkillTemplateModel
        {
            Id = id,
            Name = "Test",
            AttributeId = attributeTemplateId
        };
        skillTemplate.SpecificSkillTemplates.Add(CreateSpecificSkillTemplate(id, id, attributeTemplateId));
        return skillTemplate;
    }

    private static SpecificSkillTemplateModel CreateSpecificSkillTemplate(Guid id, Guid skillTemplateId,
        Guid? attributeTemplateId)
    {
        var skillTemplate = new SpecificSkillTemplateModel
        {
            Id = id,
            Name = "Test",
            AttributeTemplateId = attributeTemplateId,
            SkillTemplateId = skillTemplateId
        };
        return skillTemplate;
    }
}