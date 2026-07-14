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

        var input = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = campaignTemplate.ItemConfiguration,
            DefenseId = defensePropertyId,
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral],
                PropertyType.Vitality),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(10);

        // Act
        var result = attacker.BasicAttack(defender, input, dice);

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

        var input = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config,
            DefenseId = null,
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral],
                PropertyType.Vitality),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(15);
        // Act
        var result = attacker.BasicAttack(defender, input, dice);

        // Assert
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Evasion defense should not increase only because creature level increased")]
    public void EvasionDefenseShouldNotIncreaseOnlyBecauseCreatureLevelChanged()
    {
        var defenseId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion];
        var creature = new BaseCreature(LandOfHeroesTemplate.Template, "").Creature;

        var levelOneDefense = creature.DefenseValue(defenseId);

        creature.Level = 10;

        var levelTenDefense = creature.DefenseValue(defenseId);

        levelTenDefense.Should().Be(levelOneDefense);
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

        var moral = defender.Vitalities.First(v =>
            v.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral]);
        var life = defender.Vitalities.First(v =>
            v.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Life]);
        var mana = defender.Vitalities.First(v =>
            v.VitalityTemplateId == LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Mana]);
        moral.Value = 1;
        life.Value = 1;
        var manaBeforeAttack = mana.Value;

        var input = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = campaignTemplate.ItemConfiguration,
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(Arg.Any<int>()).Returns(callInfo => callInfo.Arg<int>());

        // Act
        var result = attacker.BasicAttack(defender, input, dice);

        // Assert
        result.Success.Should().BeTrue();
        moral.Value.Should().Be(0);
        life.Value.Should().Be(0);
        mana.Value.Should().BeLessThan(manaBeforeAttack);
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
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Light weapon attacking medium armor")]
    public void T6()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon];
        var result = PerformBasicAttack(WeaponCategory.Light, ArmorCategory.Medium, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Light weapon attacking heavy armor")]
    public void T7()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon];
        var result = PerformBasicAttack(WeaponCategory.Light, ArmorCategory.Heavy, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Medium weapon attacking light armor")]
    public void T8()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon];
        var result = PerformBasicAttack(WeaponCategory.Medium, ArmorCategory.Light, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Medium weapon attacking medium armor")]
    public void T9()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon];
        var result = PerformBasicAttack(WeaponCategory.Medium, ArmorCategory.Medium, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Medium weapon attacking heavy armor")]
    public void T10()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon];
        var result = PerformBasicAttack(WeaponCategory.Medium, ArmorCategory.Heavy, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Heavy weapon attacking light armor")]
    public void T11()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon];
        var result = PerformBasicAttack(WeaponCategory.Heavy, ArmorCategory.Light, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Heavy weapon attacking medium armor")]
    public void T12()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon];
        var result = PerformBasicAttack(WeaponCategory.Heavy, ArmorCategory.Medium, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Heavy weapon attacking heavy armor")]
    public void T13()
    {
        var hitPropertyId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon];
        var result = PerformBasicAttack(WeaponCategory.Heavy, ArmorCategory.Heavy, hitPropertyId);
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().BeGreaterThan(0);
    }

    private BasicAttackResult PerformBasicAttack(WeaponCategory weaponCategory, ArmorCategory armorCategory,
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

        var input = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config,
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral],
                PropertyType.Vitality),
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(Arg.Any<int>()).Returns(callInfo => callInfo.Arg<int>());

        return attacker.BasicAttack(defender, input, dice);
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

                    var input = new BasicAttackCommand
                    {
                        WeaponSlot = EquipableSlot.MainHand,
                        ItemConfiguration = campaignTemplate.ItemConfiguration,
                        Luck = 0,
                        Advantage = 0
                    };

                    // var diceRoller = new DiceRoller();
                    var diceRoller = Substitute.For<IDiceRoller>();
                    diceRoller.Roll(20).Returns(19);
                    var newDiceRoller = new DiceRoller();
                    var totalDamage = 0;
                    var hits = 0m;
                    var weaponDifficult = 0;
                    for (var i = 0; i < TotalAttacks; i++)
                    {
                        var result = attacker.BasicAttack(defender, input, diceRoller, _testOutputHelper);
                        totalDamage += result.TotalDamage;
                        hits += result.Success ? 1 : 0;
                        weaponDifficult = result.Difficulty;
                    }

                    totalDamage /= TotalAttacks;
                    var hit = hits / (TotalAttacks * weaponDifficult);
                    //      _testOutputHelper.WriteLine($"LEVEL {level} - Weapon {weaponCategory.ToString()} - Armor {armorCategory.ToString()} - {hit} hits - {totalDamage} damage");

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
        byLevelAndWeapon.Should().HaveCount(20);

        foreach (var byWeapon in byLevelAndWeapon.Values)
        {
            byWeapon.Keys.Should().BeEquivalentTo(new[]
            {
                WeaponCategory.Light,
                WeaponCategory.Medium,
                WeaponCategory.Heavy
            });
            foreach (var byArmor in byWeapon.Values)
            {
                byArmor.Keys.Should().BeEquivalentTo(new[]
                {
                    ArmorCategory.Light,
                    ArmorCategory.Medium,
                    ArmorCategory.Heavy
                });
                byArmor.Values.Should().OnlyContain(damage => damage > 0);
            }
        }

        var levelOneTotal = byLevelAndWeapon[1].Values.SelectMany(byArmor => byArmor.Values).Sum();
        var levelTwentyTotal = byLevelAndWeapon[20].Values.SelectMany(byArmor => byArmor.Values).Sum();
        levelTwentyTotal.Should().BeGreaterThan(levelOneTotal, "dano agregado deve escalar com level");
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

                    var input = new BasicAttackCommand
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
                        var result = attacker.BasicAttack(defender, input, newDiceRoller, _testOutputHelper);
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
