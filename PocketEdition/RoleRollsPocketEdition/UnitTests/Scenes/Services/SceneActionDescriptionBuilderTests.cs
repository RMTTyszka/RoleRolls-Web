using FluentAssertions;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Scenes.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Scenes.Services;

public class SceneActionDescriptionBuilderTests
{
    [Fact]
    public void SpecialAttackDescriptionShouldNotMentionWeaponOrDamage()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "Mage").Creature;
        var target = new BaseCreature(template, "Guard").Creature;

        var result = new SpecialAttackResult
        {
            Attacker = attacker,
            Target = target,
            SpecialSkillId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Bluff],
            SpecialSkillName = "Bluff",
            DefenseId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion],
            DefenseName = "Evasion",
            Success = true
        };

        var description = SceneActionDescriptionBuilder.BuildSpecialAttackDescription(result);

        description.Should().Contain("Mage");
        description.Should().Contain("Guard");
        description.Should().Contain("Bluff");
        description.Should().Contain("Evasion");
        description.Should().NotContain("damage");
        description.Should().NotContain("with ");
    }
}
