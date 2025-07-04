using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.CombatManeuvers;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Races;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;

public class LandOfHeroesTemplate
{
    public static CampaignTemplate Template
    {
        get
        {
            var template = new CampaignTemplate
            {
                Id = Guid.Parse("985C54E0-C742-49BC-A3E0-8DD2D6CE2632"),
                Name = "Land Of Heroes",
                ArchetypeTitle = "Archetype",
                CreatureTypeTitle = "Races",
                Default = true,
                Attributes = BuildAttributes(),
                AttributelessSkills = BuildAttributelessSkills(),
                Vitalities = BuildVitalities(),
                DamageTypes = BuildDamageTypes(),
                CreatureTypes = LandOfHeroesRaces.Races,
                Archetypes = Roles,
                CombatManeuvers = LandOfHeroesCombatManeuvers.Maneuvers,
            };
            template.ItemConfiguration = BuildItemConfiguration(template);
            return template;
        }
    }

    private static List<VitalityTemplate> BuildVitalities()
    {
        return
        [
            new()
            {
                Id = VitalityIds[LandOfHeroesVitality.Life],
                Name = "Life",
                Formula = "10 + 4 * Vigor",
                CampaignTemplateId = Guid.Parse("985C54E0-C742-49BC-A3E0-8DD2D6CE2632"),
                CampaignTemplate = null,
            },

            new()
            {
                Id = VitalityIds[LandOfHeroesVitality.Moral],
                Name = "Moral",
                Formula = "15 + 2 * Intuition",
                CampaignTemplateId = Guid.Parse("985C54E0-C742-49BC-A3E0-8DD2D6CE2632"),
                CampaignTemplate = null,
            }
        ];
    }

    private static List<SkillTemplate> BuildAttributelessSkills()
    {
        var skills = Enum.GetValues<LandOfHeroesSkill>()
            .Where(s => !AttributeSkills.SelectMany(e => e.Value).Contains(s));

        return skills.Select(skill =>
        {
            return new SkillTemplate
            {
                Name = skill.ToString(),
                Id = LandOfHeroesSkills.SkillIds[skill],
                AttributeTemplateId = null,
                SpecificSkillTemplates = GetMinorSkills(skill, LandOfHeroesSkills.SkillIds[skill], null)
            };
        }).ToList();
    }

    private static ItemConfiguration BuildItemConfiguration(CampaignTemplate template)
    {
        return new ItemConfiguration(template, new ItemConfigurationModel
        {
            ArmorProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.Evasion], PropertyType.MinorSkill),
            BlockProperty = new Property(LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor]),
            MeleeLightWeaponHitProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
                PropertyType.MinorSkill),
            MeleeMediumWeaponHitProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon],
                PropertyType.MinorSkill),
            MeleeHeavyWeaponHitProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon],
                PropertyType.MinorSkill),

            MeleeLightWeaponDamageProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
                PropertyType.MinorSkill),
            MeleeMediumWeaponDamageProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon],
                PropertyType.MinorSkill),
            MeleeHeavyWeaponDamageProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon],
                PropertyType.MinorSkill),

            RangedLightWeaponHitProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
                PropertyType.MinorSkill),
            RangedMediumWeaponHitProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
                PropertyType.MinorSkill),
            RangedHeavyWeaponHitProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
                PropertyType.MinorSkill),

            RangedLightWeaponDamageProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.RangedLightWeapon],
                PropertyType.MinorSkill),
            RangedMediumWeaponDamageProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.RangedMediumWeapon],
                PropertyType.MinorSkill),
            RangedHeavyWeaponDamageProperty = new Property(MinorSkillIds[LandOfHeroesMinorSkill.RangedHeavyWeapon],
                PropertyType.MinorSkill),

            BasicAttackTargetFirstVitality =
                new Property(VitalityIds[LandOfHeroesVitality.Moral], PropertyType.Vitality),
            BasicAttackTargetSecondVitality =
                new Property(VitalityIds[LandOfHeroesVitality.Life], PropertyType.Vitality)
        })
        {
            Id = Guid.Parse("985C54E0-C742-49BC-A3E0-8DD2D6CE2632"),
        };
    }

    private static List<Archetype> Roles =>
    [
        WarriorArchetypeDetails.WarriorArchetype,
        BarbarianArchetypeDetails.BarbarianArchetype,
        BardArchetypeDetails.BardArchetype,
        CrusaderArchetypeDetails.CrusaderArchetype,
        DruidArchetypeDetails.DruidArchetype,
        HunterArchetypeDetails.HunterArchetype,
        MartialistArchetypeDetails.MartialistArchetype,
        RogueArchetypeDetails.RogueArchetype,
        SpellcasterArchetypeDetails.SpellcasterArchetype,
        SpiritualistArchetypeDetails.SpiritualistArchetype,
        WarlockArchetypeDetails.WarlockArchetype,
    ];


    public static List<DamageType> BuildDamageTypes()
    {
        return Enum.GetValues<DamageTypeEnum>()
            .Select(dt => new DamageType
            {
                Id = DamageTypeIds[dt],
                Name = dt.ToString()
            })
            .ToList();
    }

    private static List<AttributeTemplate> BuildAttributes()
    {
        return LandOfHeroesAttributes.AttributeIds.Select(attributeEntry => new AttributeTemplate
        {
            Name = attributeEntry.Key.ToString(),
            Id = attributeEntry.Value,
            SkillTemplates = GetSkills(attributeEntry.Key, attributeEntry.Value)
        }).ToList();
    }

    private static ICollection<SkillTemplate> GetSkills(LandOfHeroesAttribute attribute, Guid attributeId)
    {
        var skills = AttributeSkills.ContainsKey(attribute) ? AttributeSkills[attribute] : [];

        return skills.Select(skill => new SkillTemplate
        {
            Name = skill.ToString(),
            Id = LandOfHeroesSkills.SkillIds[skill],
            AttributeTemplateId = attributeId,
            SpecificSkillTemplates = GetMinorSkills(skill, LandOfHeroesSkills.SkillIds[skill], attributeId)
        }).ToList();
    }

    private static List<SpecificSkillTemplate> GetMinorSkills(LandOfHeroesSkill skill, Guid skillId, Guid? attributeId)
    {
        var minorSkills = SkillMinorSkills.ContainsKey(skill) ? SkillMinorSkills[skill] : [];

        return minorSkills.Select(minorSkill => new SpecificSkillTemplate
        {
            Name = minorSkill.ToString(),
            Id = MinorSkillIds[minorSkill],
            SkillTemplateId = skillId,
            SkillTemplate = null,
            AttributeTemplateId = attributeId ?? AttributelessMinorSkillsAttributeId[minorSkill]
        }).ToList();
    }

    private static readonly Dictionary<DamageTypeEnum, Guid> DamageTypeIds = new()
    {
        { DamageTypeEnum.Martial, new Guid("c7ddf02b-262f-4099-8f3c-3826677a3237") },
        { DamageTypeEnum.Arcane, new Guid("3789e696-7b1e-4242-9462-f00c13079e6b") },
        { DamageTypeEnum.Fire, new Guid("751ac265-e302-422f-9c96-a66938b2cba5") },
        { DamageTypeEnum.Ice, new Guid("21977699-500e-4a63-9873-0db508c96a40") },
        { DamageTypeEnum.Lightning, new Guid("855c6281-36ca-4b19-93ca-5f1d63093d70") },
        { DamageTypeEnum.Acid, new Guid("42255ef3-c4c1-4b36-9298-d3d16a5f6568") },
        { DamageTypeEnum.Necrotic, new Guid("c1d25d0e-634c-4e5e-b367-6647dc79675e") },
        { DamageTypeEnum.Radiant, new Guid("9dd8562c-83c6-4b1f-8c54-78b64a85951a") }
    };

    public static Dictionary<LandOfHeroesVitality, Guid> VitalityIds = new()
    {
        { LandOfHeroesVitality.Life, Guid.Parse("F3B7E2A9-8D41-4A6D-9C50-1E6BFD5A4D23") },
        { LandOfHeroesVitality.Moral, Guid.Parse("7A92C5E3-4F8E-4C2E-98F7-5D3A1B8A6C91") },
    };


    public static Dictionary<LandOfHeroesMinorSkill, Guid?> AttributelessMinorSkillsAttributeId = new()
    {
        { LandOfHeroesMinorSkill.MeleeLightWeapon, LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility] },
        {
            LandOfHeroesMinorSkill.MeleeMediumWeapon,
            LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]
        },
        {
            LandOfHeroesMinorSkill.MeleeHeavyWeapon, LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]
        },
        {
            LandOfHeroesMinorSkill.RangedLightWeapon, LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility]
        },
        {
            LandOfHeroesMinorSkill.RangedMediumWeapon,
            LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]
        },
        {
            LandOfHeroesMinorSkill.RangedHeavyWeapon,
            LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength]
        },
        { LandOfHeroesMinorSkill.ResistInjury, LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor] },
        {
            LandOfHeroesMinorSkill.ResistPoisonAndDisease,
            LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor]
        },
        { LandOfHeroesMinorSkill.ResistCurse, LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Vigor] },
        { LandOfHeroesMinorSkill.Evasion, LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility] },

        { LandOfHeroesMinorSkill.CauseInjury, null },
        { LandOfHeroesMinorSkill.CausePoisonOrDisease, null },
        { LandOfHeroesMinorSkill.CauseCurse, null },
        { LandOfHeroesMinorSkill.Concentrate, null }
    };


    public static Dictionary<LandOfHeroesMinorSkill, Guid> MinorSkillIds = new()
    {
        { LandOfHeroesMinorSkill.Acrobacy, Guid.Parse("12345678-9ABC-DEF0-1234-56789ABCDEF0") },
        { LandOfHeroesMinorSkill.Arcane, Guid.Parse("9D4E1C5A-B476-4832-8B52-1E7A5D6F3C5D") },
        { LandOfHeroesMinorSkill.Bluff, Guid.Parse("F4D7C8A3-E9B5-4A1E-87C3-5D9A6B2E7F1A") },
        { LandOfHeroesMinorSkill.CauseInjury, Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF1") },
        { LandOfHeroesMinorSkill.CausePoisonOrDisease, Guid.Parse("B2C3D4E5-F6A7-8901-2345-6789ABCDEF12") },
        { LandOfHeroesMinorSkill.CauseCurse, Guid.Parse("C3D4E5F6-A7B8-9012-3456-789ABCDEF123") },
        { LandOfHeroesMinorSkill.Climb, Guid.Parse("A3F2E7B1-B547-4C28-AB53-7D5F4C8E32D6") },
        { LandOfHeroesMinorSkill.ColdWeather, Guid.Parse("37C78661-29FB-4332-BE37-EF2162FA1D5C") },
        { LandOfHeroesMinorSkill.Concentrate, Guid.Parse("D4E5F6A7-B890-1234-5678-9ABCDEF12345") },
        { LandOfHeroesMinorSkill.Curse, Guid.Parse("F233497C-D56E-4210-B7FC-3AF84EDB7B62") },
        { LandOfHeroesMinorSkill.Diplomacy, Guid.Parse("76B4E8A3-C9F5-478D-97A3-6D1F2C8B7A4E") },
        { LandOfHeroesMinorSkill.Disease, Guid.Parse("91CE4E24-AEAA-4B71-8E2D-F990C6C7B053") },
        { LandOfHeroesMinorSkill.Dungeons, Guid.Parse("89C7F4A2-BD5A-45E9-A7F3-1234567890AB") },
        { LandOfHeroesMinorSkill.Evasion, Guid.Parse("CD04047D-6180-47FD-9397-8B8C80DDF882") },
        { LandOfHeroesMinorSkill.Feeling, Guid.Parse("6A5B9D4C-E81F-41A7-93E8-4D5C9A7F6B32") },
        { LandOfHeroesMinorSkill.History, Guid.Parse("BCDEF8A1-7C6A-47A2-9D7A-2E6C9D1E4B5A") },
        { LandOfHeroesMinorSkill.Hide, Guid.Parse("AEF6A7C4-B9D3-43E2-8B6F-7C5D4A2E6F9D") },
        { LandOfHeroesMinorSkill.Hunger, Guid.Parse("596376CF-C313-4834-9B53-8772BC33983B") },
        { LandOfHeroesMinorSkill.Injury, Guid.Parse("834A6BBA-86BA-43F4-A80E-DA67EB5867C5") },
        { LandOfHeroesMinorSkill.Jump, Guid.Parse("9A7B5F6E-C4D8-42E9-81A3-7C5D8B6A2F4E") },
        { LandOfHeroesMinorSkill.Listen, Guid.Parse("F6C7D5A3-EB41-48A7-92F3-4E7C8B5D1A9E") },
        { LandOfHeroesMinorSkill.MeleeHeavyWeapon, Guid.Parse("4A7E5F9B-C6D3-42A1-92E7-5C9B8F6A3D2E") },
        { LandOfHeroesMinorSkill.MeleeLightWeapon, Guid.Parse("8E9B6A5F-C7D4-41E8-92A3-7C5F8B6D3A2E") },
        { LandOfHeroesMinorSkill.MeleeMediumWeapon, Guid.Parse("F6C7A5D8-B9E3-48A1-87F2-3E9B8D5C6A7F") },
        { LandOfHeroesMinorSkill.Nature, Guid.Parse("1234F5E6-A7C8-492D-BF1A-8D4C7A3E5F92") },
        { LandOfHeroesMinorSkill.Observe, Guid.Parse("8A6B7C9D-E5F4-43A2-92D1-7E6C9B5D3F2A") },
        { LandOfHeroesMinorSkill.Performance, Guid.Parse("7F5E9A6B-C8D4-432E-A7B3-5D9C8A1F7E62") },
        { LandOfHeroesMinorSkill.Poison, Guid.Parse("D87F7849-074D-42DF-A028-648CCABDE329") },
        { LandOfHeroesMinorSkill.RangedHeavyWeapon, Guid.Parse("8D5C7A9F-B4E6-4A3D-92E1-7C9A6B5F3D8E") },
        { LandOfHeroesMinorSkill.RangedLightWeapon, Guid.Parse("9C5A7B6E-F4D8-42A3-81E9-8B6F2D7C3A5E") },
        { LandOfHeroesMinorSkill.RangedMediumWeapon, Guid.Parse("F4A7B6E9-C8D3-41E2-97A3-5D9B6C8A2F7E") },
        { LandOfHeroesMinorSkill.Religion, Guid.Parse("F7D9E8A5-B6C3-4A29-91E7-C5D4A2F6B93E") },
        { LandOfHeroesMinorSkill.ResistInjury, Guid.Parse("5A4B3C2D-E1F0-9876-5432-10FEDCBA9876") },
        { LandOfHeroesMinorSkill.ResistPoisonAndDisease, Guid.Parse("6B5C4D3E-F201-0987-6543-21FEDCBA0987") },
        { LandOfHeroesMinorSkill.ResistCurse, Guid.Parse("7C6D5E4F-0312-1098-7654-32FEDCBA1098") },
        { LandOfHeroesMinorSkill.Run, Guid.Parse("6A5F4E9B-C3D7-4A1E-87A3-5D9C8B6F2E7A") },
        { LandOfHeroesMinorSkill.Search, Guid.Parse("4E9A6B5F-C7D3-42E9-81A7-9C5D8B6F3A2E") },
        { LandOfHeroesMinorSkill.Stealth, Guid.Parse("1234A7B6-E5F9-4C3D-8A2E-9B5F7C6D1E8A") },
        { LandOfHeroesMinorSkill.Swim, Guid.Parse("4A7F5E9B-C3D2-41E8-91A9-8B5C6D7A3F2E") },
        { LandOfHeroesMinorSkill.WarmWeather, Guid.Parse("D08DF7D0-F477-4472-884D-F10B09806FF7") }
    };

    public static Dictionary<LandOfHeroesAttribute, List<LandOfHeroesSkill>> AttributeSkills =>
        new()
        {
            { LandOfHeroesAttribute.Agility, [LandOfHeroesSkill.Nimbleness] },
            { LandOfHeroesAttribute.Charisma, [LandOfHeroesSkill.Empathy] },
            { LandOfHeroesAttribute.Intelligence, [LandOfHeroesSkill.Knowledge, LandOfHeroesSkill.Treatment] },
            { LandOfHeroesAttribute.Intuition, [LandOfHeroesSkill.Awareness] },
            { LandOfHeroesAttribute.Strength, [LandOfHeroesSkill.Athletics] },
            { LandOfHeroesAttribute.Vigor, [LandOfHeroesSkill.Survival] }
        };

    public static Dictionary<LandOfHeroesSkill, List<LandOfHeroesMinorSkill>> SkillMinorSkills =>
        new()
        {
            {
                LandOfHeroesSkill.Awareness,
                [
                    LandOfHeroesMinorSkill.Observe, LandOfHeroesMinorSkill.Listen, LandOfHeroesMinorSkill.Search,
                    LandOfHeroesMinorSkill.Feeling
                ]
            },
            { LandOfHeroesSkill.Empathy, [LandOfHeroesMinorSkill.Diplomacy, LandOfHeroesMinorSkill.Bluff] },
            {
                LandOfHeroesSkill.Knowledge,
                [
                    LandOfHeroesMinorSkill.History, LandOfHeroesMinorSkill.Arcane, LandOfHeroesMinorSkill.Religion,
                    LandOfHeroesMinorSkill.Nature, LandOfHeroesMinorSkill.Dungeons
                ]
            },
            {
                LandOfHeroesSkill.Nimbleness,
                [LandOfHeroesMinorSkill.Acrobacy, LandOfHeroesMinorSkill.Hide, LandOfHeroesMinorSkill.Stealth]
            },
            {
                LandOfHeroesSkill.Survival,
                [LandOfHeroesMinorSkill.Hunger, LandOfHeroesMinorSkill.ColdWeather, LandOfHeroesMinorSkill.WarmWeather]
            },
            {
                LandOfHeroesSkill.Treatment,
                [
                    LandOfHeroesMinorSkill.Injury, LandOfHeroesMinorSkill.Poison, LandOfHeroesMinorSkill.Curse,
                    LandOfHeroesMinorSkill.Disease
                ]
            },
            {
                LandOfHeroesSkill.Athletics,
                [
                    LandOfHeroesMinorSkill.Swim, LandOfHeroesMinorSkill.Run, LandOfHeroesMinorSkill.Climb,
                    LandOfHeroesMinorSkill.Jump
                ]
            },
            {
                LandOfHeroesSkill.Combat, [
                    LandOfHeroesMinorSkill.MeleeLightWeapon,
                    LandOfHeroesMinorSkill.MeleeMediumWeapon,
                    LandOfHeroesMinorSkill.MeleeHeavyWeapon,
                    LandOfHeroesMinorSkill.RangedLightWeapon,
                    LandOfHeroesMinorSkill.RangedMediumWeapon,
                    LandOfHeroesMinorSkill.RangedHeavyWeapon,
                    LandOfHeroesMinorSkill.Evasion,
                    LandOfHeroesMinorSkill.Concentrate,
                    LandOfHeroesMinorSkill.CauseCurse,
                    LandOfHeroesMinorSkill.CauseInjury,
                    LandOfHeroesMinorSkill.CausePoisonOrDisease,
                ]
            },
            {
                LandOfHeroesSkill.Resistance, [
                    LandOfHeroesMinorSkill.ResistInjury,
                    LandOfHeroesMinorSkill.ResistCurse,
                    LandOfHeroesMinorSkill.ResistPoisonAndDisease,
                ]
            }
        };
}