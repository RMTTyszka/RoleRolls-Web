using FluentAssertions;
using Newtonsoft.Json;
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
        var defensePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility];
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];

        var attacker = new BaseCreature(campaignTemplate, "").Creature;
        var defender = new BaseCreature(campaignTemplate, "").Creature;

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
            Advantage = 10
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
        var hitPropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var defensePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility];
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var config = new ItemConfiguration
        {
            MeleeMediumWeaponHitProperty = new Property(hitPropertyId, PropertyType.Attribute),
            MeleeMediumWeaponDamageProperty = new Property(damagePropertyId, PropertyType.Attribute)
        };

        var attacker = new BaseCreature(campaignTemplate, "").Creature;;
        var defender = new BaseCreature(campaignTemplate, "").Creature;;

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
        dice.Roll(20).Returns(11);
        dice.Roll(8).Returns(8);
        // Act
        var result = attacker.Attack(defender, input, dice);

        // Assert
        result.Success.Should().BeTrue();
        result.TotalDamage.Should().Be(4);
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

                    // var diceRoller = new DiceRoller();
                    var diceRoller = Substitute.For<IDiceRoller>();
                    diceRoller.Roll(20).Returns(19);
                    diceRoller.Roll(6).Returns(6);
                    diceRoller.Roll(8).Returns(8);
                    diceRoller.Roll(12).Returns(12);
                    var newDiceRoller = new DiceRoller();
                    var totalDamage = 0;
                    var hits = 0m;
                    var weaponDifficult = 0;
                    for (var i = 0; i < TotalAttacks; i++)
                    {
                        var result = attacker.Attack(defender, input, newDiceRoller, _testOutputHelper);
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
        Assert.Equal(25, byLevelAndWeapon[1][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(16, byLevelAndWeapon[1][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(9, byLevelAndWeapon[1][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(24, byLevelAndWeapon[1][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(22, byLevelAndWeapon[1][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(20, byLevelAndWeapon[1][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(19, byLevelAndWeapon[1][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(18, byLevelAndWeapon[1][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(17, byLevelAndWeapon[1][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(30, byLevelAndWeapon[2][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(16, byLevelAndWeapon[2][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(6, byLevelAndWeapon[2][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(28, byLevelAndWeapon[2][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(24, byLevelAndWeapon[2][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(20, byLevelAndWeapon[2][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(22, byLevelAndWeapon[2][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(20, byLevelAndWeapon[2][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(18, byLevelAndWeapon[2][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(30, byLevelAndWeapon[3][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(16, byLevelAndWeapon[3][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(6, byLevelAndWeapon[3][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(28, byLevelAndWeapon[3][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(24, byLevelAndWeapon[3][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(20, byLevelAndWeapon[3][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(22, byLevelAndWeapon[3][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(20, byLevelAndWeapon[3][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(18, byLevelAndWeapon[3][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(35, byLevelAndWeapon[4][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(16, byLevelAndWeapon[4][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(3, byLevelAndWeapon[4][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(32, byLevelAndWeapon[4][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(26, byLevelAndWeapon[4][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(20, byLevelAndWeapon[4][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(25, byLevelAndWeapon[4][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(22, byLevelAndWeapon[4][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(19, byLevelAndWeapon[4][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(35, byLevelAndWeapon[5][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(16, byLevelAndWeapon[5][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(3, byLevelAndWeapon[5][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(32, byLevelAndWeapon[5][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(26, byLevelAndWeapon[5][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(20, byLevelAndWeapon[5][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(25, byLevelAndWeapon[5][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(22, byLevelAndWeapon[5][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(19, byLevelAndWeapon[5][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(48, byLevelAndWeapon[6][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(20, byLevelAndWeapon[6][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(4, byLevelAndWeapon[6][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(38, byLevelAndWeapon[6][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(30, byLevelAndWeapon[6][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(22, byLevelAndWeapon[6][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(30, byLevelAndWeapon[6][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(26, byLevelAndWeapon[6][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(44, byLevelAndWeapon[6][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(48, byLevelAndWeapon[7][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(20, byLevelAndWeapon[7][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(4, byLevelAndWeapon[7][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(38, byLevelAndWeapon[7][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(30, byLevelAndWeapon[7][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(22, byLevelAndWeapon[7][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(30, byLevelAndWeapon[7][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(26, byLevelAndWeapon[7][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(44, byLevelAndWeapon[7][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(54, byLevelAndWeapon[8][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(20, byLevelAndWeapon[8][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(4, byLevelAndWeapon[8][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(42, byLevelAndWeapon[8][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(32, byLevelAndWeapon[8][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(22, byLevelAndWeapon[8][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(33, byLevelAndWeapon[8][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(28, byLevelAndWeapon[8][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(46, byLevelAndWeapon[8][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(54, byLevelAndWeapon[9][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(20, byLevelAndWeapon[9][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(4, byLevelAndWeapon[9][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(42, byLevelAndWeapon[9][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(32, byLevelAndWeapon[9][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(22, byLevelAndWeapon[9][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(33, byLevelAndWeapon[9][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(28, byLevelAndWeapon[9][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(46, byLevelAndWeapon[9][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(60, byLevelAndWeapon[10][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(20, byLevelAndWeapon[10][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(4, byLevelAndWeapon[10][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(46, byLevelAndWeapon[10][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(34, byLevelAndWeapon[10][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(22, byLevelAndWeapon[10][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(36, byLevelAndWeapon[10][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(30, byLevelAndWeapon[10][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(48, byLevelAndWeapon[10][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(60, byLevelAndWeapon[11][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(20, byLevelAndWeapon[11][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(4, byLevelAndWeapon[11][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(46, byLevelAndWeapon[11][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(34, byLevelAndWeapon[11][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(22, byLevelAndWeapon[11][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(36, byLevelAndWeapon[11][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(30, byLevelAndWeapon[11][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(48, byLevelAndWeapon[11][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(77, byLevelAndWeapon[12][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(24, byLevelAndWeapon[12][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(5, byLevelAndWeapon[12][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(78, byLevelAndWeapon[12][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(57, byLevelAndWeapon[12][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(36, byLevelAndWeapon[12][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(41, byLevelAndWeapon[12][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(68, byLevelAndWeapon[12][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(54, byLevelAndWeapon[12][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(77, byLevelAndWeapon[13][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(24, byLevelAndWeapon[13][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(5, byLevelAndWeapon[13][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(78, byLevelAndWeapon[13][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(57, byLevelAndWeapon[13][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(36, byLevelAndWeapon[13][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(41, byLevelAndWeapon[13][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(68, byLevelAndWeapon[13][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(54, byLevelAndWeapon[13][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(84, byLevelAndWeapon[14][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(24, byLevelAndWeapon[14][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(5, byLevelAndWeapon[14][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(84, byLevelAndWeapon[14][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(60, byLevelAndWeapon[14][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(36, byLevelAndWeapon[14][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(44, byLevelAndWeapon[14][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(72, byLevelAndWeapon[14][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(56, byLevelAndWeapon[14][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(84, byLevelAndWeapon[15][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(24, byLevelAndWeapon[15][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(5, byLevelAndWeapon[15][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(84, byLevelAndWeapon[15][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(60, byLevelAndWeapon[15][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(36, byLevelAndWeapon[15][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(44, byLevelAndWeapon[15][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(72, byLevelAndWeapon[15][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(56, byLevelAndWeapon[15][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(91, byLevelAndWeapon[16][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(24, byLevelAndWeapon[16][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(5, byLevelAndWeapon[16][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(90, byLevelAndWeapon[16][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(63, byLevelAndWeapon[16][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(36, byLevelAndWeapon[16][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(47, byLevelAndWeapon[16][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(76, byLevelAndWeapon[16][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(58, byLevelAndWeapon[16][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(91, byLevelAndWeapon[17][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(24, byLevelAndWeapon[17][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(5, byLevelAndWeapon[17][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(90, byLevelAndWeapon[17][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(63, byLevelAndWeapon[17][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(36, byLevelAndWeapon[17][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(47, byLevelAndWeapon[17][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(76, byLevelAndWeapon[17][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(58, byLevelAndWeapon[17][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(112, byLevelAndWeapon[18][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(28, byLevelAndWeapon[18][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(6, byLevelAndWeapon[18][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(99, byLevelAndWeapon[18][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(69, byLevelAndWeapon[18][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(39, byLevelAndWeapon[18][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(104, byLevelAndWeapon[18][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(84, byLevelAndWeapon[18][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(64, byLevelAndWeapon[18][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(112, byLevelAndWeapon[19][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(28, byLevelAndWeapon[19][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(6, byLevelAndWeapon[19][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(99, byLevelAndWeapon[19][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(69, byLevelAndWeapon[19][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(39, byLevelAndWeapon[19][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(104, byLevelAndWeapon[19][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(84, byLevelAndWeapon[19][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(64, byLevelAndWeapon[19][WeaponCategory.Heavy][ArmorCategory.Heavy]);
        Assert.Equal(120, byLevelAndWeapon[20][WeaponCategory.Light][ArmorCategory.Light]);
        Assert.Equal(28, byLevelAndWeapon[20][WeaponCategory.Light][ArmorCategory.Medium]);
        Assert.Equal(6, byLevelAndWeapon[20][WeaponCategory.Light][ArmorCategory.Heavy]);
        Assert.Equal(105, byLevelAndWeapon[20][WeaponCategory.Medium][ArmorCategory.Light]);
        Assert.Equal(72, byLevelAndWeapon[20][WeaponCategory.Medium][ArmorCategory.Medium]);
        Assert.Equal(39, byLevelAndWeapon[20][WeaponCategory.Medium][ArmorCategory.Heavy]);
        Assert.Equal(110, byLevelAndWeapon[20][WeaponCategory.Heavy][ArmorCategory.Light]);
        Assert.Equal(88, byLevelAndWeapon[20][WeaponCategory.Heavy][ArmorCategory.Medium]);
        Assert.Equal(66, byLevelAndWeapon[20][WeaponCategory.Heavy][ArmorCategory.Heavy]);
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