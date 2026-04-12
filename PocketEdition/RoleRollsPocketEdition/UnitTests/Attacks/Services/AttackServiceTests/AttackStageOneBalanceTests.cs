using FluentAssertions;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class AttackStageOneBalanceTests
{
    private const int SamplesPerLevel = 3000;
    private const int Seed = 7301;

    private readonly ITestOutputHelper _output;

    public AttackStageOneBalanceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "Medium vs Medium reference hit rate stays near half")]
    public void MediumVsMediumReferenceHitRateStaysNearHalf()
    {
        var campaignTemplate = LandOfHeroesTemplate.Template;
        var rates = new List<double>();

        foreach (var level in Enumerable.Range(1, 20))
        {
            var attacker = new BaseCreature(campaignTemplate, $"Attacker L{level}")
                .WithLevel(level)
                .WithWeapon(WeaponCategory.Medium, EquipableSlot.MainHand, level)
                .Creature;

            var defender = new BaseCreature(campaignTemplate, $"Defender L{level}")
                .WithLevel(level)
                .WithArmor(ArmorCategory.Medium, level)
                .Creature;

            var command = new AttackCommand
            {
                WeaponSlot = EquipableSlot.MainHand,
                ItemConfiguration = campaignTemplate.ItemConfiguration,
                Luck = 0,
                Advantage = 0
            };

            var roller = new RandomDiceRoller(Seed + level * 37);
            var successes = 0;
            for (var i = 0; i < SamplesPerLevel; i++)
            {
                var result = attacker.Attack(defender, command, roller);
                if (result.Success)
                {
                    successes++;
                }

                defender.FullRestore();
            }

            var rate = successes / (double)SamplesPerLevel;
            rates.Add(rate);
            rate.Should().BeInRange(0.40, 0.60,
                $"nivel {level} deve manter chance proxima do centro de balanceamento para medium vs medium");
            _output.WriteLine($"Level {level:00} medium vs medium hit rate: {rate:P2}");
        }

        var average = rates.Average();
        _output.WriteLine($"Average medium vs medium hit rate: {average:P2}");
        average.Should().BeInRange(0.45, 0.55,
            "a media dos niveis deve ficar proxima de 50% no cenario de referencia");
    }

    [Fact(DisplayName = "Hit value is used as dice count and not added to hit bonus")]
    public void HitValueIsNotAddedToHitBonus()
    {
        var campaignTemplate = LandOfHeroesTemplate.Template;

        var attacker = new BaseCreature(campaignTemplate, "attacker")
            .WithWeapon(WeaponCategory.Medium, EquipableSlot.MainHand, 1)
            .Creature;

        var defender = new BaseCreature(campaignTemplate, "defender")
            .WithArmor(ArmorCategory.Medium, 1)
            .Creature;

        var command = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = campaignTemplate.ItemConfiguration,
            Luck = 0,
            Advantage = 0
        };

        var roller = new ScriptedDiceRoller([13, 13, 13, 13]);

        var result = attacker.Attack(defender, command, roller);

        result.Success.Should().BeFalse();
        result.NumberOfRollSuccesses.Should().Be(0);
        result.TotalDamage.Should().Be(0);
    }

    [Fact(DisplayName = "Damage still depends on excess when hit count is the same")]
    public void DamageStillDependsOnExcessWhenHitCountIsTheSame()
    {
        var campaignTemplate = LandOfHeroesTemplate.Template;

        var lowAttacker = new BaseCreature(campaignTemplate, "low attacker")
            .WithWeapon(WeaponCategory.Medium, EquipableSlot.MainHand, 1)
            .Creature;

        var highAttacker = new BaseCreature(campaignTemplate, "high attacker")
            .WithWeapon(WeaponCategory.Medium, EquipableSlot.MainHand, 1)
            .Creature;

        var lowDefender = new BaseCreature(campaignTemplate, "low defender")
            .WithArmor(ArmorCategory.Medium, 1)
            .Creature;

        var highDefender = new BaseCreature(campaignTemplate, "high defender")
            .WithArmor(ArmorCategory.Medium, 1)
            .Creature;

        var command = new AttackCommand
        {
            WeaponSlot = EquipableSlot.MainHand,
            ItemConfiguration = campaignTemplate.ItemConfiguration,
            Luck = 0,
            Advantage = 0
        };

        var lowRoller = new ScriptedDiceRoller([14, 14, 1, 1]);
        var highRoller = new ScriptedDiceRoller([20, 20, 1, 1]);

        var lowResult = lowAttacker.Attack(lowDefender, command, lowRoller);
        var highResult = highAttacker.Attack(highDefender, command, highRoller);

        lowResult.Success.Should().BeTrue();
        highResult.Success.Should().BeTrue();
        lowResult.NumberOfRollSuccesses.Should().Be(1);
        highResult.NumberOfRollSuccesses.Should().Be(1);
        lowResult.TotalDamage.Should().Be(1);
        highResult.TotalDamage.Should().BeGreaterThan(lowResult.TotalDamage);
    }

    private sealed class ScriptedDiceRoller : IDiceRoller
    {
        private readonly Queue<int> _d20Rolls;

        public ScriptedDiceRoller(IEnumerable<int> d20Rolls)
        {
            _d20Rolls = new Queue<int>(d20Rolls);
        }

        public int Roll(int sides)
        {
            if (sides <= 0)
            {
                return 0;
            }

            if (sides == 20 && _d20Rolls.Count > 0)
            {
                return _d20Rolls.Dequeue();
            }

            return 1;
        }

        public int[] RollMany(int sides, int times)
        {
            var rolls = new int[Math.Max(times, 0)];
            for (var i = 0; i < rolls.Length; i++)
            {
                rolls[i] = Roll(sides);
            }

            return rolls;
        }
    }

    private sealed class RandomDiceRoller : IDiceRoller
    {
        private readonly Random _random;

        public RandomDiceRoller(int seed)
        {
            _random = new Random(seed);
        }

        public int Roll(int sides)
        {
            if (sides <= 0)
            {
                return 0;
            }

            return _random.Next(1, sides + 1);
        }

        public int[] RollMany(int sides, int times)
        {
            var rolls = new int[Math.Max(times, 0)];
            for (var i = 0; i < rolls.Length; i++)
            {
                rolls[i] = Roll(sides);
            }

            return rolls;
        }
    }
}
