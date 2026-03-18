using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.Seeds;

public sealed record LandOfHeroesMonsterSeed(
    Guid Id,
    string Name,
    string CreatureTypeName,
    int Level,
    IReadOnlyDictionary<Guid, int> AttributePoints,
    IReadOnlyDictionary<Guid, int> SpecificSkillPoints);

public static class LandOfHeroesMonsterCatalog
{
    public static IReadOnlyCollection<LandOfHeroesMonsterSeed> Monsters =>
    [
        new(
            Guid.Parse("3587EB77-DB45-4053-B876-0FF4EE361D09"),
            "Goblin Skirmisher",
            "Goblin",
            1,
            new Dictionary<Guid, int>
            {
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility]] = 4,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Intuition]] = 3,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor]] = 2
            },
            new Dictionary<Guid, int>
            {
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Stealth]] = 3,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Hide]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Evasion]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.RangedLightWeapon]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Observe]] = 1
            }),
        new(
            Guid.Parse("70EAA2C3-B471-4CB7-BB2D-0FD5670FFB4B"),
            "Orc Brute",
            "Orc",
            1,
            new Dictionary<Guid, int>
            {
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]] = 4,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor]] = 4,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Intuition]] = 2
            },
            new Dictionary<Guid, int>
            {
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon]] = 3,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.CauseInjury]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.ResistInjury]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Run]] = 1
            }),
        new(
            Guid.Parse("EFCA6C3F-9920-461B-8767-1188DC2D6837"),
            "Skeleton Guard",
            "Skeleton",
            1,
            new Dictionary<Guid, int>
            {
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]] = 3,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor]] = 4,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility]] = 2
            },
            new Dictionary<Guid, int>
            {
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.ResistInjury]] = 3,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Observe]] = 1
            }),
        new(
            Guid.Parse("19E303AD-65C6-40E7-A7D7-4851C9F59DA5"),
            "Zombie Shambler",
            "Zombie",
            1,
            new Dictionary<Guid, int>
            {
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor]] = 4,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]] = 3,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Intuition]] = 1
            },
            new Dictionary<Guid, int>
            {
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.ResistInjury]] = 3,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.CauseInjury]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon]] = 1
            }),
        new(
            Guid.Parse("3B11761B-2B1E-4A9F-8B31-0BAF8496D8A3"),
            "Dire Wolf",
            "Wolf",
            1,
            new Dictionary<Guid, int>
            {
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility]] = 4,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Intuition]] = 3,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor]] = 3,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]] = 3
            },
            new Dictionary<Guid, int>
            {
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Run]] = 3,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Observe]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Search]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Evasion]] = 2,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.CauseInjury]] = 2
            }),
        new(
            Guid.Parse("B7A72444-0CA4-4461-88A0-8A40852DA46A"),
            "Giant Spider",
            "Giant Spider",
            1,
            new Dictionary<Guid, int>
            {
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility]] = 4,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Intuition]] = 3,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor]] = 2,
                [LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]] = 2
            },
            new Dictionary<Guid, int>
            {
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Stealth]] = 3,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Climb]] = 3,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.CausePoisonOrDisease]] = 3,
                [LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Evasion]] = 2
            })
    ];
}
