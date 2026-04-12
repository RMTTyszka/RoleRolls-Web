using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;
using Xunit.Abstractions;
using Attribute = RoleRollsPocketEdition.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class AttackTests
{
    private const int TotalAttacks = 100;
    private ITestOutputHelper _testOutputHelper;

    public AttackTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact(DisplayName = "When an attack fail, the defender doesn't take damage")]
    public void T1()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var hitPropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var defensePropertyId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion];
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];

        var attacker = new BaseCreature(campaignTemplate, "").Creature;
        var defender = new BaseCreature(campaignTemplate, "").Creature;

        var input = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = campaignTemplate.ItemConfiguration,
            HitAttribute = new Property(hitPropertyId, PropertyType.Attribute),
            DamageAttribute = new Property(damagePropertyId, PropertyType.Attribute),
            DefenseId = defensePropertyId,
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral],
                PropertyType.Vitality),
            SecondVitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life],
                PropertyType.Vitality),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(10);

        // Act
        var result = attacker.Attack(defender, input, dice);

        // Assert
        result.Success.Should().BeFalse();
        result.TotalDamage.Should().Be(0);
        result.Attacker.Should().Be(attacker);
        result.Target.Should().Be(defender);
        result.Weapon.Should().NotBeNull();
    }

    [Fact(DisplayName = "When an attack succeeded, the defender take damage")]
    public void T2()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon];
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var config = campaignTemplate.ItemConfiguration;

        var attacker = new BaseCreature(campaignTemplate, "").Creature;
        ;
        var defender = new BaseCreature(campaignTemplate, "").Creature;
        ;

        var input = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config,
            HitProperty = new Property(hitPropertyId, PropertyType.MinorSkill),
            DamageAttribute = new Property(damagePropertyId, PropertyType.Attribute),
            DefenseId = null,
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral],
                PropertyType.Vitality),
            SecondVitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life],
                PropertyType.Vitality),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(15);
        // Act
        var result = attacker.Attack(defender, input, dice);

        // Assert
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(2);
    }

    [Fact(DisplayName = "Basic attack should cascade through configured vitality order")]
    public void T14()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        campaignTemplate.Vitalities.First(v => v.Id == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral])
            .BasicAttackOrder = 1;
        campaignTemplate.Vitalities.First(v => v.Id == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life])
            .BasicAttackOrder = 2;
        campaignTemplate.Vitalities.First(v => v.Id == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Mana])
            .BasicAttackOrder = 3;

        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon];
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];

        var attacker = new BaseCreature(campaignTemplate, "").Creature;
        var defender = new BaseCreature(campaignTemplate, "").Creature;

        var moral = defender.Vitalities.First(v => v.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral]);
        var life = defender.Vitalities.First(v => v.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life]);
        var mana = defender.Vitalities.First(v => v.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Mana]);
        moral.Value = 1;
        life.Value = 1;
        var manaBeforeAttack = mana.Value;

        var input = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = campaignTemplate.ItemConfiguration,
            BasicAttackVitalityRules = [],
            HitProperty = new Property(hitPropertyId, PropertyType.MinorSkill),
            DamageAttribute = new Property(damagePropertyId, PropertyType.Attribute),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(Arg.Any<int>()).Returns(14);

        // Act
        var result = attacker.Attack(defender, input, dice);

        // Assert
        result.Success.Should().BeTrue();
        moral.Value.Should().Be(0);
        life.Value.Should().Be(0);
        mana.Value.Should().Be(manaBeforeAttack);
    }

    [Fact(DisplayName = "Vitality should expose a list of current conditions based on current value")]
    public void T15()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var creature = new BaseCreature(campaignTemplate, "").Creature;
        var moralVitalityId = LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral];
        var moral = creature.Vitalities.First(v => v.VitalityTemplateId == moralVitalityId);

        // Act/Assert - <= 30%
        moral.Value = (int)Math.Floor(moral.MaxValue * 0.3m);
        moral.CurrentConditions.Select(condition => condition.Name)
            .Should()
            .BeEquivalentTo(["Shaken"]);
        moral.CurrentStatus.Should().Be("Shaken");

        // Act/Assert - 0%
        moral.Value = 0;
        moral.CurrentConditions.Select(condition => condition.Name)
            .Should()
            .BeEquivalentTo(["Bleeding", "Shaken"]);
        moral.CurrentStatus.Should().Be("Bleeding");
    }

    [Fact(DisplayName = "Light weapon attacking light armor")]
    public void T5()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon];
        var result = PerformBasicAttack(WeaponCategory.Light, ArmorCategory.Light, hitPropertyId);
        result.Success.Should().BeTrue(
            $"damage={result.TotalDamage}, rollSuccesses={result.NumberOfRollSuccesses}, difficulty={result.Difficulty}, block={result.Block}, damageBonus={result.DamageBonus}");
        result.TotalDamage.Should().Be(16);
    }

    [Fact(DisplayName = "Light weapon attacking medium armor")]
    public void T6()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon];
        var result = PerformBasicAttack(WeaponCategory.Light, ArmorCategory.Medium, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(8);
    }

    [Fact(DisplayName = "Light weapon attacking heavy armor")]
    public void T7()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon];
        var result = PerformBasicAttack(WeaponCategory.Light, ArmorCategory.Heavy, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(12);
    }

    [Fact(DisplayName = "Medium weapon attacking light armor")]
    public void T8()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon];
        var result = PerformBasicAttack(WeaponCategory.Medium, ArmorCategory.Light, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(20);
    }

    [Fact(DisplayName = "Medium weapon attacking medium armor")]
    public void T9()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon];
        var result = PerformBasicAttack(WeaponCategory.Medium, ArmorCategory.Medium, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(18);
    }

    [Fact(DisplayName = "Medium weapon attacking heavy armor")]
    public void T10()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon];
        var result = PerformBasicAttack(WeaponCategory.Medium, ArmorCategory.Heavy, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(24);
    }

    [Fact(DisplayName = "Heavy weapon attacking light armor")]
    public void T11()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon];
        var result = PerformBasicAttack(WeaponCategory.Heavy, ArmorCategory.Light, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(18);
    }

    [Fact(DisplayName = "Heavy weapon attacking medium armor")]
    public void T12()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon];
        var result = PerformBasicAttack(WeaponCategory.Heavy, ArmorCategory.Medium, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(18);
    }

    [Fact(DisplayName = "Heavy weapon attacking heavy armor")]
    public void T13()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon];
        var result = PerformBasicAttack(WeaponCategory.Heavy, ArmorCategory.Heavy, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(23);
    }

    private AttackResult PerformBasicAttack(WeaponCategory weaponCategory, ArmorCategory armorCategory,
        Guid hitPropertyId)
    {
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var config = campaignTemplate.ItemConfiguration;

        var attacker = new BaseCreature(campaignTemplate, $"{weaponCategory} Weapon")
            .WithWeapon(weaponCategory, EquipableSlot.MainHand, 1)
            .Creature;
        var defender = new BaseCreature(campaignTemplate, $"{armorCategory} Armor")
            .WithArmor(armorCategory, 1)
            .Creature;

        var input = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config,
            HitProperty = new Property(hitPropertyId, PropertyType.MinorSkill),
            DamageAttribute = new Property(damagePropertyId, PropertyType.Attribute),
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral],
                PropertyType.Vitality),
            SecondVitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life],
                PropertyType.Vitality),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(Arg.Any<int>()).Returns(callInfo => callInfo.Arg<int>());

        return attacker.Attack(defender, input, dice);
    }

    private static IDiceRoller CreateDeterministicDiceRoller()
    {
        var dice = Substitute.For<IDiceRoller>();
        var countersBySides = new Dictionary<int, int>();

        dice.Roll(Arg.Any<int>()).Returns(callInfo =>
        {
            var sides = callInfo.Arg<int>();
            if (sides <= 0)
            {
                return 0;
            }

            countersBySides.TryGetValue(sides, out var currentCount);
            countersBySides[sides] = currentCount + 1;
            return (currentCount % sides) + 1;
        });

        dice.RollMany(Arg.Any<int>(), Arg.Any<int>()).Returns(callInfo =>
        {
            var sides = callInfo.ArgAt<int>(0);
            var times = callInfo.ArgAt<int>(1);
            var rolls = new int[Math.Max(times, 0)];
            for (var i = 0; i < rolls.Length; i++)
            {
                rolls[i] = dice.Roll(sides);
            }

            return rolls;
        });

        return dice;
    }

    [Fact(DisplayName = "Full Level Test")]
    public void T3()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var byLevelAndWeapon = new Dictionary<int, Dictionary<WeaponCategory, Dictionary<ArmorCategory, int>>>();
        var byLevelAndArmor = new Dictionary<int, Dictionary<ArmorCategory, Dictionary<WeaponCategory, int>>>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            //        if (level != 5) continue;
            var byWeaponAndArmor = new Dictionary<WeaponCategory, Dictionary<ArmorCategory, int>>();
            var byArmorAndWeapon = new Dictionary<ArmorCategory, Dictionary<WeaponCategory, int>>();
            foreach (var weaponCategory in Enum.GetValues<WeaponCategory>())
            {
                if (weaponCategory is WeaponCategory.None or WeaponCategory.LightShield or WeaponCategory.MediumShield
                    or WeaponCategory.HeavyShield
                    //     or WeaponCategory.Medium 
                    //    or WeaponCategory.Light 
                    //    or WeaponCategory.Heavy
                   )
                {
                    continue;
                }

                var attacker = new BaseCreature(campaignTemplate, $"{weaponCategory.ToString()} Level {level}")
                    .WithLevel(level)
                    .WithWeapon(weaponCategory, EquipableSlot.MainHand, level)
                    .Creature;

                var byArmor = new Dictionary<ArmorCategory, int>();

                foreach (var armorCategory in Enum.GetValues<ArmorCategory>()
                             .Where(e =>
                                     e is not ArmorCategory.None
                                 //     and not ArmorCategory.Medium
                                 //     and not ArmorCategory.Heavy
                                 //      and not ArmorCategory.Light
                             )
                        )
                {
                    var defender = new BaseCreature(campaignTemplate, $"{armorCategory.ToString()} Level {level}")
                        .WithLevel(level)
                        .WithArmor(armorCategory, level)
                        .Creature;

                    var input = new AttackCommand
                    {
                        WeaponSlot = EquipableSlot.MainHand,
                        ItemConfiguration = campaignTemplate.ItemConfiguration,
                        Luck = 0,
                        Advantage = 0
                    };

                    var deterministicDiceRoller = CreateDeterministicDiceRoller();
                    var totalDamage = 0;
                    for (var i = 0; i < TotalAttacks; i++)
                    {
                        var result = attacker.Attack(defender, input, deterministicDiceRoller, _testOutputHelper);
                        totalDamage += result.TotalDamage;
                    }

                    totalDamage /= TotalAttacks;

                    byArmor.Add(armorCategory, totalDamage);
                    if (!byArmorAndWeapon.ContainsKey(armorCategory))
                    {
                        byArmorAndWeapon[armorCategory] = new Dictionary<WeaponCategory, int>();
                    }

                    byArmorAndWeapon[armorCategory].Add(weaponCategory, totalDamage);
                }

                byWeaponAndArmor.Add(weaponCategory, byArmor);
            }

            byLevelAndWeapon.Add(level, byWeaponAndArmor);
            byLevelAndArmor.Add(level, byArmorAndWeapon);
        }

        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(byLevelAndWeapon, Formatting.Indented));
        Assert.Equal(3, byLevelAndWeapon[1][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(1, byLevelAndWeapon[1][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[1][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(3, byLevelAndWeapon[1][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[1][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(4, byLevelAndWeapon[1][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(3, byLevelAndWeapon[1][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[1][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(6, byLevelAndWeapon[1][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(3, byLevelAndWeapon[2][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(1, byLevelAndWeapon[2][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[2][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(3, byLevelAndWeapon[2][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[2][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(4, byLevelAndWeapon[2][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(3, byLevelAndWeapon[2][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[2][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(6, byLevelAndWeapon[2][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(5, byLevelAndWeapon[3][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[3][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[3][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(5, byLevelAndWeapon[3][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(4, byLevelAndWeapon[3][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(5, byLevelAndWeapon[3][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(4, byLevelAndWeapon[3][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(5, byLevelAndWeapon[3][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(7, byLevelAndWeapon[3][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(6, byLevelAndWeapon[4][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[4][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[4][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(5, byLevelAndWeapon[4][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(4, byLevelAndWeapon[4][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(6, byLevelAndWeapon[4][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(4, byLevelAndWeapon[4][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(4, byLevelAndWeapon[4][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(7, byLevelAndWeapon[4][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(10, byLevelAndWeapon[5][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[5][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[5][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(7, byLevelAndWeapon[5][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(7, byLevelAndWeapon[5][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(8, byLevelAndWeapon[5][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(6, byLevelAndWeapon[5][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(5, byLevelAndWeapon[5][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(9, byLevelAndWeapon[5][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(7, byLevelAndWeapon[6][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[6][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[6][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(7, byLevelAndWeapon[6][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(6, byLevelAndWeapon[6][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(7, byLevelAndWeapon[6][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(4, byLevelAndWeapon[6][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(6, byLevelAndWeapon[6][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(10, byLevelAndWeapon[6][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(11, byLevelAndWeapon[7][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[7][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[7][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(9, byLevelAndWeapon[7][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(9, byLevelAndWeapon[7][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(9, byLevelAndWeapon[7][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(5, byLevelAndWeapon[7][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(7, byLevelAndWeapon[7][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(13, byLevelAndWeapon[7][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(10, byLevelAndWeapon[8][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[8][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[8][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(8, byLevelAndWeapon[8][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(6, byLevelAndWeapon[8][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(7, byLevelAndWeapon[8][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(8, byLevelAndWeapon[8][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(6, byLevelAndWeapon[8][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(13, byLevelAndWeapon[8][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(14, byLevelAndWeapon[9][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[9][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[9][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(10, byLevelAndWeapon[9][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(8, byLevelAndWeapon[9][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(9, byLevelAndWeapon[9][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(10, byLevelAndWeapon[9][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(7, byLevelAndWeapon[9][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(15, byLevelAndWeapon[9][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(14, byLevelAndWeapon[10][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[10][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[10][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(10, byLevelAndWeapon[10][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(8, byLevelAndWeapon[10][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(9, byLevelAndWeapon[10][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(10, byLevelAndWeapon[10][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(7, byLevelAndWeapon[10][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(15, byLevelAndWeapon[10][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(13, byLevelAndWeapon[11][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[11][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[11][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(8, byLevelAndWeapon[11][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(10, byLevelAndWeapon[11][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(10, byLevelAndWeapon[11][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[11][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(9, byLevelAndWeapon[11][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(14, byLevelAndWeapon[11][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(15, byLevelAndWeapon[12][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[12][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(3, byLevelAndWeapon[12][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(9, byLevelAndWeapon[12][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(10, byLevelAndWeapon[12][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(11, byLevelAndWeapon[12][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[12][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(9, byLevelAndWeapon[12][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(19, byLevelAndWeapon[12][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(19, byLevelAndWeapon[13][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[13][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(3, byLevelAndWeapon[13][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(11, byLevelAndWeapon[13][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(12, byLevelAndWeapon[13][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(13, byLevelAndWeapon[13][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[13][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(11, byLevelAndWeapon[13][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(22, byLevelAndWeapon[13][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(19, byLevelAndWeapon[14][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[14][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(3, byLevelAndWeapon[14][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(11, byLevelAndWeapon[14][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(12, byLevelAndWeapon[14][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(13, byLevelAndWeapon[14][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[14][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(11, byLevelAndWeapon[14][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(22, byLevelAndWeapon[14][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(23, byLevelAndWeapon[15][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(4, byLevelAndWeapon[15][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(3, byLevelAndWeapon[15][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(12, byLevelAndWeapon[15][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(15, byLevelAndWeapon[15][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(15, byLevelAndWeapon[15][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[15][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(12, byLevelAndWeapon[15][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(25, byLevelAndWeapon[15][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(16, byLevelAndWeapon[16][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(2, byLevelAndWeapon[16][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[16][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(13, byLevelAndWeapon[16][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(9, byLevelAndWeapon[16][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(12, byLevelAndWeapon[16][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[16][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(14, byLevelAndWeapon[16][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(13, byLevelAndWeapon[16][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(19, byLevelAndWeapon[17][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[17][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[17][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(15, byLevelAndWeapon[17][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(10, byLevelAndWeapon[17][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(14, byLevelAndWeapon[17][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[17][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(16, byLevelAndWeapon[17][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(15, byLevelAndWeapon[17][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(19, byLevelAndWeapon[18][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(3, byLevelAndWeapon[18][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[18][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(15, byLevelAndWeapon[18][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(10, byLevelAndWeapon[18][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(14, byLevelAndWeapon[18][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[18][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(16, byLevelAndWeapon[18][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(15, byLevelAndWeapon[18][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(23, byLevelAndWeapon[19][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(5, byLevelAndWeapon[19][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[19][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(17, byLevelAndWeapon[19][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(12, byLevelAndWeapon[19][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(16, byLevelAndWeapon[19][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[19][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(18, byLevelAndWeapon[19][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(17, byLevelAndWeapon[19][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(23, byLevelAndWeapon[20][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(5, byLevelAndWeapon[20][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(2, byLevelAndWeapon[20][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(17, byLevelAndWeapon[20][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(12, byLevelAndWeapon[20][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(16, byLevelAndWeapon[20][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(0, byLevelAndWeapon[20][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(18, byLevelAndWeapon[20][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(17, byLevelAndWeapon[20][WeaponCategory.Heavy][ArmorCategory.Heavy]);

    }

    [Fact(DisplayName = "Attack and Evade test")]
    public void T4()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var byLevelAndWeapon = new Dictionary<int, Dictionary<WeaponCategory, Dictionary<ArmorCategory, (int, int)>>>();
        var byLevelAndArmor = new Dictionary<int, Dictionary<ArmorCategory, Dictionary<WeaponCategory, int>>>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            //        if (level != 5) continue;
            var byWeaponAndArmor = new Dictionary<WeaponCategory, Dictionary<ArmorCategory, (int, int)>>();
            var byArmorAndWeapon = new Dictionary<ArmorCategory, Dictionary<WeaponCategory, int>>();
            foreach (var weaponCategory in Enum.GetValues<WeaponCategory>())
            {
                if (weaponCategory is WeaponCategory.None or WeaponCategory.LightShield or WeaponCategory.MediumShield
                    or WeaponCategory.HeavyShield
                    //     or WeaponCategory.Medium 
                    //    or WeaponCategory.Light 
                    //    or WeaponCategory.Heavy
                   )
                {
                    continue;
                }

                var attacker = new BaseCreature(campaignTemplate, $"{weaponCategory.ToString()} Level {level}")
                    .WithLevel(level)
                    .WithWeapon(weaponCategory, EquipableSlot.MainHand, level)
                    .Creature;

                var byArmor = new Dictionary<ArmorCategory, (int, int)>();

                foreach (var armorCategory in Enum.GetValues<ArmorCategory>()
                             .Where(e =>
                                     e is not ArmorCategory.None
                                 //     and not ArmorCategory.Medium
                                 //     and not ArmorCategory.Heavy
                                 //      and not ArmorCategory.Light
                             )
                        )
                {
                    var defender = new BaseCreature(campaignTemplate, $"{armorCategory.ToString()} Level {level}")
                        .WithLevel(level)
                        .WithArmor(armorCategory, level)
                        .Creature;

                    var input = new AttackCommand
                    {
                        WeaponSlot = EquipableSlot.MainHand,
                        ItemConfiguration = campaignTemplate.ItemConfiguration,
                        Luck = 0,
                        Advantage = 0
                    };

                    // var diceRoller = new DiceRoller();
                    var diceRoller = Substitute.For<IDiceRoller>();
                    diceRoller.Roll(20).Returns(19);
                    diceRoller.Roll(6).Returns(6);
                    diceRoller.Roll(8).Returns(8);
                    diceRoller.Roll(12).Returns(12);
                    var newDiceRoller = new DiceRoller();
                    var totalDamage = 0;
                    var totalDamageEvasion = 0;
                    var hits = 0m;
                    for (var i = 0; i < TotalAttacks; i++)
                    {
                        var result = attacker.Attack(defender, input, newDiceRoller, _testOutputHelper);
                        totalDamage += result.TotalDamage;
                        hits += result.Success ? 1 : 0;
                    }

                    for (var i = 0; i < TotalAttacks; i++)
                    {
                        diceRoller.Roll(20).Returns(2);
                        var evasionResult = defender.Evade(attacker, input, newDiceRoller);
                        totalDamageEvasion += evasionResult.TotalDamage;
                    }

                    totalDamage /= TotalAttacks;
                    totalDamageEvasion /= TotalAttacks;

                    byArmor.Add(armorCategory, (totalDamage, totalDamageEvasion));
                    if (!byArmorAndWeapon.ContainsKey(armorCategory))
                    {
                        byArmorAndWeapon[armorCategory] = new Dictionary<WeaponCategory, int>();
                    }

                    byArmorAndWeapon[armorCategory].Add(weaponCategory, totalDamage);
                }

                byWeaponAndArmor.Add(weaponCategory, byArmor);
            }

            byLevelAndWeapon.Add(level, byWeaponAndArmor);
            byLevelAndArmor.Add(level, byArmorAndWeapon);
        }

        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(byLevelAndWeapon, Formatting.Indented));
    }
}

