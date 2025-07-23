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

  //  [Fact(DisplayName = "When an attack fail, the defender doesn't take damage")]
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

  //  [Fact(DisplayName = "When an attack succeeded, the defender take damage")]
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
                var totalDamage = 0;
                var hits = 0m;
                var weaponDifficult = 0;
                for (var i = 0; i < TotalAttacks; i++)
                {
                    var result = attacker.Attack(defender, input, new DiceRoller(), _testOutputHelper);
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
    /*foreach (var level in byLevelAndArmor.Keys.OrderBy(x => x))
    {
        _testOutputHelper.WriteLine($"=== LEVEL {level} ===");
        
        foreach (var armorCategory in byLevelAndArmor[level].Keys)
        {
            var totalDamageAgainstArmor = byLevelAndArmor[level][armorCategory].Values.Sum();
            _testOutputHelper.WriteLine($"  {armorCategory}: {totalDamageAgainstArmor} total damage");
        }
        
        _testOutputHelper.WriteLine(""); 
    }*/
    _testOutputHelper.WriteLine(JsonConvert.SerializeObject(byLevelAndWeapon, Formatting.Indented));
}

}