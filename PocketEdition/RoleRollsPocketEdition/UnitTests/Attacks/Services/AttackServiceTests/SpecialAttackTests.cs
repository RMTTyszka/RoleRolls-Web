using FluentAssertions;
using NSubstitute;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class SpecialAttackTests
{
    [Fact]
    public void SpecialAttackShouldResolveMinorSkillAgainstExplicitDefense()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "Caster").Creature;
        var defender = new BaseCreature(template, "Target").Creature;
        var defenseId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion];
        var command = new SpecialAttackCommand
        {
            SpecialSkillId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Bluff],
            DefenseId = defenseId,
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(20);

        var result = attacker.SpecialAttack(defender, command, dice);

        result.Success.Should().BeTrue();
        result.SpecialSkillId.Should().Be(command.SpecialSkillId);
        result.SpecialSkillName.Should().Be("Bluff");
        result.DefenseId.Should().Be(command.DefenseId);
        result.DefenseName.Should().Be("Evasion");
        result.Difficulty.Should().Be(1);
        result.Complexity.Should().Be(defender.DefenseValue(defenseId));
        result.RolledDices.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void SpecialAttackShouldNotUseWeaponOrDamageTarget()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "Caster").Creature;
        var defender = new BaseCreature(template, "Target").Creature;
        var before = defender.Vitalities.Select(vitality => (vitality.VitalityTemplateId, vitality.Value)).ToList();

        var command = new SpecialAttackCommand
        {
            SpecialSkillId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Arcane],
            DefenseId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion],
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(1);

        var result = attacker.SpecialAttack(defender, command, dice);

        result.Success.Should().BeFalse();
        defender.Vitalities.Select(vitality => (vitality.VitalityTemplateId, vitality.Value))
            .Should()
            .BeEquivalentTo(before);
    }
}
