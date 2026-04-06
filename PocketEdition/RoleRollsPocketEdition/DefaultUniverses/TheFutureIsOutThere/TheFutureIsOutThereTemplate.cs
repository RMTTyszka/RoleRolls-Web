using RoleRollsPocketEdition.DefaultUniverses.TheFutureIsOutThere.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.DefaultUniverses.TheFutureIsOutThere.CampaignTemplates.Skills;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.TheFutureIsOutThere.CampaignTemplates;

public static class TheFutureIsOutThereTemplate
{
    private static readonly Guid TemplateId = Guid.Parse("D4A71770-F1AF-4DA8-A611-94DA49BAEE9D");

    public static CampaignTemplate Template
    {
        get
        {
            var template = new CampaignTemplate
            {
                Id = TemplateId,
                Name = "The Future is Out There",
                ArchetypeTitle = "Archetype",
                CreatureTypeTitle = "Creature Types",
                Default = true,
                Attributes = BuildAttributes(),
                Skills = BuildSkills(),
                CreatureConditions = [],
                Vitalities = [],
                Defenses = [],
                DamageTypes = [],
                CreatureTypes = [],
                Archetypes = [],
                CombatManeuvers = [],
            };

            template.ItemConfiguration = BuildItemConfiguration(template);
            return template;
        }
    }

    private static ItemConfiguration BuildItemConfiguration(CampaignTemplate template)
    {
        return new ItemConfiguration(template, new ItemConfigurationModel())
        {
            Id = TemplateId
        };
    }

    private static List<AttributeTemplate> BuildAttributes()
    {
        return TheFutureIsOutThereAttributes.AttributeIds.Select(attributeEntry => new AttributeTemplate
        {
            Id = attributeEntry.Value,
            Name = attributeEntry.Key.ToString()
        }).ToList();
    }

    private static List<SkillTemplate> BuildSkills()
    {
        return TheFutureIsOutThereSkills.SkillIds.Select(skillEntry => new SkillTemplate
        {
            Id = skillEntry.Value,
            Name = skillEntry.Key.ToString(),
            AttributeTemplateId = null,
            SpecificSkillTemplates = GetMinorSkills(skillEntry.Key, skillEntry.Value)
        }).ToList();
    }

    private static List<SpecificSkillTemplate> GetMinorSkills(TheFutureIsOutThereSkill skill, Guid skillId)
    {
        var configuredMinorSkills = TheFutureIsOutThereSkills.SkillMinorSkills.ContainsKey(skill)
            ? TheFutureIsOutThereSkills.SkillMinorSkills[skill]
            : [];

        return configuredMinorSkills.Select(minorSkill => new SpecificSkillTemplate
        {
            Id = TheFutureIsOutThereSkills.MinorSkillIds[minorSkill],
            Name = TheFutureIsOutThereSkills.MinorSkillNames[minorSkill],
            SkillTemplateId = skillId,
            SkillTemplate = null,
            AttributeTemplateId = TheFutureIsOutThereSkills.MinorSkillAttributeIds[minorSkill]
        }).ToList();
    }
}
