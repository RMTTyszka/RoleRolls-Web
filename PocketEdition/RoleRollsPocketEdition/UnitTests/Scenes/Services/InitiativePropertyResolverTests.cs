using FluentAssertions;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;
using RoleRollsPocketEdition.Scenes.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Scenes.Services;

public class InitiativePropertyResolverTests
{
    [Fact]
    public void ResolvesConfiguredAttributeUsingItsTemplateId()
    {
        var creature = new BaseCreature(LandOfHeroesTemplate.Template, "Scout").Creature;

        var result = InitiativePropertyResolver.GetValue(
            creature,
            LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility]);

        result.Should().Be(3);
    }

    [Fact]
    public void ResolvesConfiguredSkillUsingItsTemplateId()
    {
        var creature = new BaseCreature(LandOfHeroesTemplate.Template, "Scout").Creature;
        var skillId = LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Nimbleness];
        creature.Skills.Single(skill => skill.SkillTemplateId == skillId).Points = 4;

        var result = InitiativePropertyResolver.GetValue(creature, skillId);

        result.Should().Be(4);
    }

    [Fact]
    public void LandOfHeroesConfiguresAgilityAsInitiativeProperty()
    {
        LandOfHeroesTemplate.Template.IniciativePropertyId.Should()
            .Be(LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility]);
    }
}
