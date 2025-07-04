using NSubstitute;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;
using Attribute = RoleRollsPocketEdition.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class EvadeTests
{
    [Fact]
    public void Evade_ShouldCauseDamage_WhenDefenseFailsAllRolls()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var hitPropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var defensePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility];
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];

        

        var attacker = new BaseCreature(campaignTemplate).Creature;
        var defender = new BaseCreature(campaignTemplate).Creature;

        var input = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = campaignTemplate.ItemConfiguration,
            HitAttribute = new Property(hitPropertyId, PropertyType.Attribute),
            DamageAttribute = new Property(damagePropertyId, PropertyType.Attribute),
            DefenseId = new Property(defensePropertyId, PropertyType.Attribute),
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral], PropertyType.Vitality),
            SecondVitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life], PropertyType.Vitality),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(2);
        dice.Roll(8).Returns(8);

        // Act
        var result = defender.Evade(attacker, input, dice);

        // Assert
        Assert.False(result.Success);
        Assert.True(result.TotalDamage > 0);
        Assert.Equal(attacker, result.Attacker);
        Assert.Equal(defender, result.Target);
        Assert.NotNull(result.Weapon);
    }

    [Fact]
    public void Evade_ShouldNegateAllHits_WhenDefenseSucceeds()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var hitPropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var defensePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility];
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var config = new ItemConfiguration
        {
            MeleeMediumWeaponHitProperty = new Property(hitPropertyId, PropertyType.Attribute),
            MeleeMediumWeaponDamageProperty = new Property(damagePropertyId, PropertyType.Attribute)
        };

        var attacker = new BaseCreature(campaignTemplate).Creature;;
        var defender = new BaseCreature(campaignTemplate).Creature;;

        var input = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config,
            HitAttribute = new Property(hitPropertyId, PropertyType.Attribute),
            DamageAttribute = new Property(damagePropertyId, PropertyType.Attribute),
            DefenseId = new Property(defensePropertyId, PropertyType.Attribute),
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral], PropertyType.Vitality),
            SecondVitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life], PropertyType.Vitality),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(18, 17, 19, 20);
        dice.Roll(10).Returns(0);
        // Act
        var result = defender.Evade(attacker, input, dice);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(0, result.TotalDamage);
    }
}