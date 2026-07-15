using FluentAssertions;
using NSubstitute;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class EvadeTests
{
    [Fact]
    public void EvadeUsesAttackersHitValueForBaseDiceAndDifficulty()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "attacker").Creature;
        var defender = new BaseCreature(template, "defender").Creature;
        attacker.Level = 3;
        attacker.Equipment.GetItem(EquipableSlot.MainHand)!.Level = 4;
        attacker.SpecificSkills.Single(skill =>
                skill.SpecificSkillTemplateId == LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon])
            .Points = 2;

        var command = new EvadeCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = template.ItemConfiguration
        };

        var diceRoller = Substitute.For<IDiceRoller>();
        diceRoller.Roll(20).Returns(20, 20, 20, 20, 20);

        var result = defender.Evade(attacker, command, diceRoller);

        result.BaseDice.Should().Be(5);
        result.Difficulty.Should().Be(19);
        result.Success.Should().BeTrue();
        result.Excesses.Should().BeEmpty();
    }

    [Fact]
    public void EvadeTreatsTieAsZeroExcessAndBuildsMediumWeaponDamageFromTwoFailures()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "attacker").Creature;
        var defender = new BaseCreature(template, "defender").Creature;
        var command = new EvadeCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = template.ItemConfiguration
        };
        var moral = defender.Vitalities.Single(vitality =>
            vitality.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral]);
        var life = defender.Vitalities.Single(vitality =>
            vitality.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life]);
        moral.Value = 1;
        var diceRoller = Substitute.For<IDiceRoller>();
        diceRoller.Roll(20).Returns(20, 9, 2, 1);

        var result = defender.Evade(attacker, command, diceRoller);

        result.KeptResults.Should().Equal(25, 14, 7, 6);
        result.Excesses.Should().Equal(8, 7);
        result.NumberOfHits.Should().Be(1);
        result.Block.Should().Be(9);
        result.DamageBonus.Should().Be(5);
        result.TotalDamage.Should().Be(11);
        result.Success.Should().BeFalse();
        result.VitalityDamage.Select(damage => (damage.Vitality, damage.Value)).Should().Equal(("Moral", 1), ("Life", 10));
        moral.Value.Should().Be(0);
        life.Value.Should().Be(life.MaxValue - 10);
    }

    [Theory]
    [InlineData(WeaponCategory.Light, 3)]
    [InlineData(WeaponCategory.Medium, 1)]
    [InlineData(WeaponCategory.Heavy, 1)]
    public void EvadeGroupsExcessesByTheAttackersWeaponComplexity(WeaponCategory weaponCategory, int expectedHits)
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "attacker")
            .WithWeapon(weaponCategory, EquipableSlot.MainHand, level: 1)
            .Creature;
        var defender = new BaseCreature(template, "defender").Creature;
        var diceRoller = Substitute.For<IDiceRoller>();
        diceRoller.Roll(20).Returns(1, 2, 3, 20);

        var result = defender.Evade(attacker, new EvadeCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = template.ItemConfiguration
        }, diceRoller);

        result.Excesses.Should().HaveCount(3);
        result.NumberOfHits.Should().Be(expectedHits);
    }

    [Fact]
    public void EvadeAdvantageAddsDiceButKeepsOnlyTheBestBaseDiceResults()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "attacker").Creature;
        var defender = new BaseCreature(template, "defender").Creature;
        var command = new EvadeCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = template.ItemConfiguration,
            Advantage = 2
        };
        var diceRoller = Substitute.For<IDiceRoller>();
        diceRoller.Roll(20).Returns(20, 1, 2, 3, 4, 5);

        var result = defender.Evade(attacker, command, diceRoller);

        result.BaseDice.Should().Be(4);
        result.Advantage.Should().Be(2);
        result.KeptResults.Should().Equal(25, 10, 9, 8);
    }

    [Fact]
    public void EvadeLuckKeepsTheUsualDirectionOfHighResults()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "attacker").Creature;
        var positiveLuckDefender = new BaseCreature(template, "positive").Creature;
        var negativeLuckDefender = new BaseCreature(template, "negative").Creature;
        var positiveLuckDice = Substitute.For<IDiceRoller>();
        positiveLuckDice.Roll(20).Returns(1, 10, 11, 12, 20);
        var negativeLuckDice = Substitute.For<IDiceRoller>();
        negativeLuckDice.Roll(20).Returns(20, 10, 11, 12, 1);

        var positiveLuckResult = positiveLuckDefender.Evade(attacker, new EvadeCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = template.ItemConfiguration,
            Luck = 1
        }, positiveLuckDice);
        var negativeLuckResult = negativeLuckDefender.Evade(attacker, new EvadeCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = template.ItemConfiguration,
            Luck = -1
        }, negativeLuckDice);

        positiveLuckResult.KeptResults.Should().Equal(25, 17, 16, 15);
        negativeLuckResult.KeptResults.Should().Equal(17, 16, 15, 6);
    }
}
