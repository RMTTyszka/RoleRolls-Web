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

public class EvadeTests
{
    private const int TotalAttacks = 100;
    private readonly ITestOutputHelper _testOutputHelper;

    public EvadeTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Evade_ShouldCauseDamage_WhenDefenseFailsAllRolls()
    {
        // Arrange
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var hitPropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var defensePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility];
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
        var defensePropertyId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion];
        var damagePropertyId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength];
        var config = new ItemConfiguration
        {
            MeleeMediumWeaponHitProperty = new Property(hitPropertyId, PropertyType.Attribute),
            MeleeMediumWeaponDamageProperty = new Property(damagePropertyId, PropertyType.Attribute)
        };

        var attacker = new BaseCreature(campaignTemplate, "").Creature;
        ;
        var defender = new BaseCreature(campaignTemplate, "").Creature;
        ;

        var input = new BasicAttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = config,
            DefenseId = defensePropertyId,
            VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral],
                PropertyType.Vitality),
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
                    diceRoller.Roll(20).Returns(2);
                    diceRoller.Roll(6).Returns(6);
                    diceRoller.Roll(8).Returns(8);
                    diceRoller.Roll(12).Returns(12);
                    var totalDamage = 0;
                    var hits = 0m;
                    var weaponDifficult = 0;
                    for (var i = 0; i < TotalAttacks; i++)
                    {
                        var result = defender.Evade(attacker, input, diceRoller);
                        totalDamage += result.TotalDamage;
                        hits += result.Success ? 1 : 0;
                        weaponDifficult = result.Difficulty;
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
        Assert.Equal(20, byLevelAndWeapon.Count);

        var expectedWeapons = new[] { WeaponCategory.Light, WeaponCategory.Medium, WeaponCategory.Heavy };
        var expectedArmors = new[] { ArmorCategory.Light, ArmorCategory.Medium, ArmorCategory.Heavy };

        foreach (var byWeapon in byLevelAndWeapon.Values)
        {
            Assert.Equal(expectedWeapons.OrderBy(w => w), byWeapon.Keys.OrderBy(w => w));
            foreach (var byArmor in byWeapon.Values)
            {
                Assert.Equal(expectedArmors.OrderBy(a => a), byArmor.Keys.OrderBy(a => a));
                Assert.All(byArmor.Values, damage => Assert.True(damage >= 0));
            }
        }
    }
}
