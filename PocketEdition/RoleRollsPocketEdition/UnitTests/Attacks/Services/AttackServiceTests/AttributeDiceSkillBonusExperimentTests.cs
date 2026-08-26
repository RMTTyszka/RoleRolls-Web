using FluentAssertions;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Templates.Entities;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;
using Xunit.Abstractions;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class AttackEvasionComparisonTests
{
    private const int SamplesPerProfile = 10_000;
    private const int EvasionPenalty = 1;

    private static readonly CampaignTemplate Template = LandOfHeroesTemplate.Template;
    private static readonly Guid ArmorDefenseId = Template.ItemConfiguration.ArmorDefense1!.Value;
    private readonly ITestOutputHelper _output;

    public AttackEvasionComparisonTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "Matriz compara as chances atuais de Attack e Evasion")]
    public void MatrixLogsAndKeepsAttackAndEvasionHitChancesBalanced()
    {
        var rows = BuildComparisonMatrix();

        rows.Should().HaveCount(20 * 2);
        rows.Should().OnlyContain(row => row.Weapon == WeaponCategory.Medium && row.Armor == ArmorCategory.Medium);
        rows.Should().OnlyContain(row => row.Profile.PerDieSuccessChance >= 0 && row.Profile.PerDieSuccessChance <= 1);
        rows.Should().OnlyContain(row => row.Profile.FinalHitChance >= 0 && row.Profile.FinalHitChance <= 1);

        foreach (var row in rows)
        {
            _output.WriteLine(row.Format());
        }

        foreach (var attackRow in rows.Where(row => row.PlayerAction == "Attack"))
        {
            var evasionRow = rows.Single(row =>
                row.Level == attackRow.Level && row.PlayerAction == "Evasion");

            attackRow.Profile.FinalHitChance.Should().BeApproximately(
                evasionRow.Profile.FinalHitChance,
                0.03,
                $"Ataque e Evasion devem permanecer equilibrados no nível {attackRow.Level}");
        }
    }

    private static ResolutionProfile DescribeAttack(Creature attacker, Creature defender, WeaponCategory weapon)
    {
        var hit = attacker.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.GetWeaponHitProperty(weapon)));
        var weaponLevelBonus = attacker.Equipment.GetItem(EquipableSlot.MainHand)!.LevelBonus;
        var grip = GripTypeDefinition.Stats[attacker.Equipment.GripType];
        return new ResolutionProfile(hit.Total,
            hit.Total + grip.Hit + weaponLevelBonus,
            defender.DefenseValue(ArmorDefenseId), grip.AttackDifficult);
    }

    private static ResolutionProfile DescribeEvasion(Creature attacker, Creature defender, WeaponCategory weapon)
    {
        var evade = defender.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.EvadeProperty));
        var attack = attacker.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.GetWeaponHitProperty(weapon)));
        var attackerWeapon = attacker.Equipment.GetItem(EquipableSlot.MainHand)!;
        var grip = GripTypeDefinition.Stats[attacker.Equipment.GripType];
        var armor = defender.Equipment.Chest!;
        return new ResolutionProfile(attack.Total,
            evade.Total + armor.GetDefenseBonus1() + armor.LevelBonus - EvasionPenalty,
            10 + attack.Total + grip.Hit + attackerWeapon.LevelBonus, grip.AttackDifficult);
    }

    private static Creature BuildCreature(string name, int level, WeaponCategory weapon,
        ArmorCategory armor, int weaponLevel) =>
        new BaseCreature(Template, name)
            .WithLevel(level)
            .WithWeapon(weapon, EquipableSlot.MainHand, weaponLevel)
            .WithArmor(armor, level)
            .Creature;

    private static List<ComparisonRow> BuildComparisonMatrix()
    {
        var rows = new List<ComparisonRow>();
        foreach (var level in Enumerable.Range(1, 20))
        {
            var attacker = BuildCreature($"attacker L{level} Medium", level,
                WeaponCategory.Medium, ArmorCategory.Medium, level);
            var defender = BuildCreature($"defender L{level} Medium", level,
                WeaponCategory.Medium, ArmorCategory.Medium, level);

            rows.Add(new ComparisonRow(level, WeaponCategory.Medium, ArmorCategory.Medium, "Attack",
                Measure(DescribeAttack(attacker, defender, WeaponCategory.Medium), true,
                    HashCode.Combine(level, WeaponCategory.Medium, ArmorCategory.Medium, 1))));
            rows.Add(new ComparisonRow(level, WeaponCategory.Medium, ArmorCategory.Medium, "Evasion",
                Measure(DescribeEvasion(attacker, defender, WeaponCategory.Medium), false,
                    HashCode.Combine(level, WeaponCategory.Medium, ArmorCategory.Medium, 2))));
        }

        return rows;
    }

    private static MeasuredProfile Measure(ResolutionProfile profile, bool playerIsAttacker, int seed) =>
        new(profile, PerDieSuccessChance(profile, playerIsAttacker),
            SampleFinalHitChance(profile, playerIsAttacker, seed));

    private static double PerDieSuccessChance(ResolutionProfile profile, bool attackerWinsOnTie)
    {
        var threshold = profile.StaticTarget - profile.PerDieBonus;
        if (attackerWinsOnTie)
        {
            return threshold <= 1 ? 1d : threshold > 20 ? 0d : (21 - threshold) / 20d;
        }

        return threshold < 1 ? 1d : threshold >= 20 ? 0d : (20 - threshold) / 20d;
    }

    private static double SampleFinalHitChance(ResolutionProfile profile, bool playerIsAttacker, int seed)
    {
        var random = new Random(seed);
        var hits = 0;
        for (var sample = 0; sample < SamplesPerProfile; sample++)
        {
            var successfulDice = Enumerable.Range(0, profile.BaseDice)
                .Count(_ => playerIsAttacker
                    ? random.Next(1, 21) + profile.PerDieBonus >= profile.StaticTarget
                    : random.Next(1, 21) + profile.PerDieBonus > profile.StaticTarget);
            var winningDice = playerIsAttacker ? successfulDice : profile.BaseDice - successfulDice;
            if (winningDice >= profile.WeaponDifficulty)
            {
                hits++;
            }
        }

        return hits / (double)SamplesPerProfile;
    }

    private readonly record struct ResolutionProfile(
        int BaseDice,
        int PerDieBonus,
        int StaticTarget,
        int WeaponDifficulty);

    private readonly record struct MeasuredProfile(
        ResolutionProfile Formula,
        double PerDieSuccessChance,
        double FinalHitChance);

    private readonly record struct ComparisonRow(
        int Level,
        WeaponCategory Weapon,
        ArmorCategory Armor,
        string PlayerAction,
        MeasuredProfile Profile)
    {
        public string Format()
        {
            var dieLabel = PlayerAction == "Evasion" ? "defender die" : "die";
            var hitLabel = PlayerAction == "Evasion" ? "NPC hit" : "hit";
            return $"{PlayerAction} | L{Level:00} | {Weapon}/{Armor} | " +
                   $"{Profile.Formula.BaseDice}d20+{Profile.Formula.PerDieBonus} vs {Profile.Formula.StaticTarget} " +
                   $"({dieLabel} {Profile.PerDieSuccessChance:P1}, {hitLabel} {Profile.FinalHitChance:P1})";
        }
    }
}
