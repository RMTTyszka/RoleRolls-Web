using FluentAssertions;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Templates.Entities;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class AttributeDiceSkillBonusExperimentTests
{
    private const int SamplesPerProfile = 10_000;
    private const int CurrentEvasionPenalty = 1;
    private const int ProposedEvasionPenalty = 1;
    private static readonly WeaponCategory[] WeaponCategories =
        [WeaponCategory.Light, WeaponCategory.Medium, WeaponCategory.Heavy];

    private static readonly CampaignTemplate Template = LandOfHeroesTemplate.Template;
    private static readonly Guid ArmorDefenseId = Template.ItemConfiguration.ArmorDefense1!.Value;
    [Fact]
    public void ProposedAttackUsesLinkedAttributeOnlyForPoolAndWeaponSkillOnlyForRollBonus()
    {
        foreach (var weapon in WeaponCategories)
        {
            var attacker = BuildCreature("attacker", level: 3, weapon, ArmorCategory.Medium, weaponLevel: 4);
            SetWeaponSpecialization(attacker, weapon, attributePoints: 4, skillPoints: 2);
            var defender = BuildCreature("defender", level: 3, weapon, ArmorCategory.Medium, weaponLevel: 1);

            var proposed = DescribeProposedAttack(attacker, defender, weapon);
            var current = DescribeCurrentAttack(attacker, defender, weapon);

            proposed.BaseDice.Should().Be(4);
            proposed.SpecializationBonus.Should().Be(2);
            proposed.PerDieBonus.Should().Be(2 + GripTypeDefinition.Stats[attacker.Equipment.GripType].Hit + 2);
            current.BaseDice.Should().Be(6);
            current.PerDieBonus.Should().Be(6 + GripTypeDefinition.Stats[attacker.Equipment.GripType].Hit + 2);
            proposed.StaticTarget.Should().Be(defender.DefenseValue(ArmorDefenseId));
        }
    }

    [Fact]
    public void ProposedEvasionUsesConfiguredLinkedAttributeForPoolAndAppliesPenaltyToEachDie()
    {
        foreach (var weapon in WeaponCategories)
        {
            var attacker = BuildCreature("attacker", level: 3, weapon, ArmorCategory.Medium, weaponLevel: 4);
            SetWeaponSpecialization(attacker, weapon, attributePoints: 7, skillPoints: 5);
            var defender = BuildCreature("defender", level: 3, weapon, ArmorCategory.Light, weaponLevel: 1);
            SetEvasionSpecialization(defender, attributePoints: 4, skillPoints: 2);

            var proposed = DescribeProposedEvasion(attacker, defender, weapon);
            var current = DescribeCurrentEvasion(attacker, defender, weapon);

            proposed.BaseDice.Should().Be(4);
            proposed.SpecializationBonus.Should().Be(2);
            proposed.PerDieBonus.Should().Be(2 + ArmorDefinition.DefenseBonus1(ArmorCategory.Light) + 1 -
                ProposedEvasionPenalty);
            proposed.StaticTarget.Should().Be(10 + 12 + GripTypeDefinition.Stats[attacker.Equipment.GripType].Hit + 2);
            current.BaseDice.Should().Be(12);
            current.PerDieBonus.Should().Be(6 + ArmorDefinition.DefenseBonus1(ArmorCategory.Light) + 1 -
                CurrentEvasionPenalty);
        }
    }

    [Fact(DisplayName = "Matriz mantém a mesma chance de hit para Ataque e Evasion")]
    public void MatrixKeepsAttackAndEvasionHitChancesBalanced()
    {
        var rows = BuildComparisonMatrix();

        rows.Should().HaveCount(20 * 2);
        rows.Should().OnlyContain(row => row.Weapon == WeaponCategory.Medium && row.Armor == ArmorCategory.Medium);
        rows.Should().OnlyContain(row => row.Current.PerDieSuccessChance >= 0 && row.Current.PerDieSuccessChance <= 1);
        rows.Should().OnlyContain(row => row.Current.FinalHitChance >= 0 && row.Current.FinalHitChance <= 1);
        rows.Should().OnlyContain(row => row.Proposal.PerDieSuccessChance >= 0 && row.Proposal.PerDieSuccessChance <= 1);
        rows.Should().OnlyContain(row => row.Proposal.FinalHitChance >= 0 && row.Proposal.FinalHitChance <= 1);

        foreach (var attackRow in rows.Where(row => row.PlayerAction == "Attack"))
        {
            var evasionRow = rows.Single(row =>
                row.Level == attackRow.Level && row.PlayerAction == "Evasion");

            attackRow.Current.FinalHitChance.Should().BeApproximately(
                evasionRow.Current.FinalHitChance,
                0.03,
                $"Ataque e Evasion devem permanecer equilibrados no nível {attackRow.Level}");
        }
    }

    private static ResolutionProfile DescribeProposedAttack(Creature attacker, Creature defender, WeaponCategory weapon)
    {
        var hit = attacker.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.GetWeaponHitProperty(weapon)));
        var weaponLevelBonus = attacker.Equipment.GetItem(EquipableSlot.MainHand)!.LevelBonus;
        var grip = GripTypeDefinition.Stats[attacker.Equipment.GripType];
        var specializationBonus = GetSpecializationBonus(hit);
        return new ResolutionProfile(hit.GetValue, specializationBonus,
            specializationBonus + grip.Hit + weaponLevelBonus,
            defender.DefenseValue(ArmorDefenseId), grip.AttackDifficult);
    }

    private static ResolutionProfile DescribeCurrentAttack(Creature attacker, Creature defender, WeaponCategory weapon)
    {
        var hit = attacker.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.GetWeaponHitProperty(weapon)));
        var weaponLevelBonus = attacker.Equipment.GetItem(EquipableSlot.MainHand)!.LevelBonus;
        var grip = GripTypeDefinition.Stats[attacker.Equipment.GripType];
        return new ResolutionProfile(hit.Total, GetSpecializationBonus(hit),
            hit.Total + grip.Hit + weaponLevelBonus,
            defender.DefenseValue(ArmorDefenseId), grip.AttackDifficult);
    }

    private static ResolutionProfile DescribeProposedEvasion(Creature attacker, Creature defender, WeaponCategory weapon)
    {
        var evade = defender.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.EvadeProperty));
        var attack = attacker.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.GetWeaponHitProperty(weapon)));
        var attackerWeapon = attacker.Equipment.GetItem(EquipableSlot.MainHand)!;
        var grip = GripTypeDefinition.Stats[attacker.Equipment.GripType];
        var armor = defender.Equipment.Chest!;
        var specializationBonus = GetSpecializationBonus(evade);
        return new ResolutionProfile(evade.GetValue, specializationBonus,
            specializationBonus + armor.GetDefenseBonus1() + armor.LevelBonus - ProposedEvasionPenalty,
            10 + attack.Total + grip.Hit + attackerWeapon.LevelBonus, grip.AttackDifficult);
    }

    private static ResolutionProfile DescribeCurrentEvasion(Creature attacker, Creature defender, WeaponCategory weapon)
    {
        var evade = defender.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.EvadeProperty));
        var attack = attacker.GetPropertyValue(new PropertyInput(Template.ItemConfiguration.GetWeaponHitProperty(weapon)));
        var attackerWeapon = attacker.Equipment.GetItem(EquipableSlot.MainHand)!;
        var grip = GripTypeDefinition.Stats[attacker.Equipment.GripType];
        var armor = defender.Equipment.Chest!;
        return new ResolutionProfile(attack.Total, GetSpecializationBonus(evade),
            evade.Total + armor.GetDefenseBonus1() + armor.LevelBonus - CurrentEvasionPenalty,
            10 + attack.Total + grip.Hit + attackerWeapon.LevelBonus, grip.AttackDifficult);
    }

    private static Creature BuildCreature(string name, int level, WeaponCategory weapon,
        ArmorCategory armor, int weaponLevel) =>
        new BaseCreature(Template, name)
            .WithLevel(level)
            .WithWeapon(weapon, EquipableSlot.MainHand, weaponLevel)
            .WithArmor(armor, level)
            .Creature;

    private static void SetWeaponSpecialization(Creature creature, WeaponCategory weapon,
        int attributePoints, int skillPoints)
    {
        var minorSkill = weapon switch
        {
            WeaponCategory.Light => LandOfHeroesMinorSkill.MeleeLightWeapon,
            WeaponCategory.Medium => LandOfHeroesMinorSkill.MeleeMediumWeapon,
            WeaponCategory.Heavy => LandOfHeroesMinorSkill.MeleeHeavyWeapon,
            _ => throw new ArgumentOutOfRangeException(nameof(weapon), weapon, null)
        };
        var attributeId = LandOfHeroesTemplate.AttributelessMinorSkillsAttributeId[minorSkill]!.Value;
        creature.Attributes.Single(attribute => attribute.AttributeTemplateId == attributeId).Points = attributePoints;
        SetSpecificSkill(creature, LandOfHeroesTemplate.MinorSkillIds[minorSkill], skillPoints);
    }

    private static void SetEvasionSpecialization(Creature creature, int attributePoints, int skillPoints)
    {
        var agilityId = LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility];
        creature.Attributes.Single(attribute => attribute.AttributeTemplateId == agilityId).Points = attributePoints;
        SetSpecificSkill(creature,
            LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Evasion], skillPoints);
    }

    private static void SetSpecificSkill(Creature creature, Guid templateId, int points) =>
        creature.SpecificSkills.Single(skill => skill.SpecificSkillTemplateId == templateId).Points = points;

    private static int GetSpecializationBonus(PropertyValue propertyValue) =>
        propertyValue.Total - propertyValue.GetValue;

    private List<ComparisonRow> BuildComparisonMatrix()
    {
        var rows = new List<ComparisonRow>();
        foreach (var level in Enumerable.Range(1, 20))
        {
            var attacker = BuildCreature($"attacker L{level} Medium", level,
                WeaponCategory.Medium, ArmorCategory.Medium, level);
            var defender = BuildCreature($"defender L{level} Medium", level,
                WeaponCategory.Medium, ArmorCategory.Medium, level);
            rows.Add(new ComparisonRow(level, WeaponCategory.Medium, ArmorCategory.Medium, "Attack",
                Measure(DescribeCurrentAttack(attacker, defender, WeaponCategory.Medium), true,
                    HashCode.Combine(level, WeaponCategory.Medium, ArmorCategory.Medium, 1)),
                Measure(DescribeProposedAttack(attacker, defender, WeaponCategory.Medium), true,
                    HashCode.Combine(level, WeaponCategory.Medium, ArmorCategory.Medium, 3))));
            rows.Add(new ComparisonRow(level, WeaponCategory.Medium, ArmorCategory.Medium, "Evasion",
                Measure(DescribeCurrentEvasion(attacker, defender, WeaponCategory.Medium), false,
                    HashCode.Combine(level, WeaponCategory.Medium, ArmorCategory.Medium, 2)),
                Measure(DescribeProposedEvasion(attacker, defender, WeaponCategory.Medium), false,
                    HashCode.Combine(level, WeaponCategory.Medium, ArmorCategory.Medium, 4))));
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
        int SpecializationBonus,
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
        MeasuredProfile Current,
        MeasuredProfile Proposal)
    {
        public string Format()
        {
            var dieLabel = PlayerAction == "Evasion" ? "defender die" : "die";
            var hitLabel = PlayerAction == "Evasion" ? "NPC hit" : "hit";
            return $"{PlayerAction} | L{Level:00} | {Weapon}/{Armor} | " +
                   $"current {Current.Formula.BaseDice}d20+{Current.Formula.PerDieBonus} vs {Current.Formula.StaticTarget} " +
                   $"({dieLabel} {Current.PerDieSuccessChance:P1}, {hitLabel} {Current.FinalHitChance:P1}) | " +
                   $"proposal {Proposal.Formula.BaseDice}d20+{Proposal.Formula.PerDieBonus} vs {Proposal.Formula.StaticTarget} " +
                   $"({dieLabel} {Proposal.PerDieSuccessChance:P1}, {hitLabel} {Proposal.FinalHitChance:P1})";
        }
    }
}
