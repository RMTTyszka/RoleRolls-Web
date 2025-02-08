using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplate;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Templates.Entities;
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
                Lifes = BuildLifes(),
                DamageTypes = BuildDamageTypes(),
                CreatureTypes = Races
            };
            template.ItemConfiguration = BuildItemConfiguration(template);
            return template;
        }
    }

    private static List<LifeTemplate> BuildLifes()
    {
        return new List<LifeTemplate>
        {
            new()
            {
                Id = LifeIds[LandOfHeroesLife.Life],
                Name = "Life",
                Formula = "10 + 4 * Vitality",
                CreatureTemplateId = Guid.Parse("985C54E0-C742-49BC-A3E0-8DD2D6CE2632"),
                CampaignTemplate = null,
            },   
            new()
            {
                Id = LifeIds[LandOfHeroesLife.Moral],
                Name = "Moral",
                Formula = "15 + 2 * Intuition",
                CreatureTemplateId = Guid.Parse("985C54E0-C742-49BC-A3E0-8DD2D6CE2632"),
                CampaignTemplate = null,
            },
        };
    }

    private static List<SkillTemplate> BuildAttributelessSkills()
    {
        var skills = Enum.GetValues<LandOfHeroesSkill>().Where(s => !AttributeSkills.SelectMany(e => e.Value).Contains(s));

        return skills.Select(skill => new SkillTemplate
        {
            Name = skill.ToString(),
            Id = SkillIds[skill],
            AttributeTemplateId = null,
            MinorSkills = GetMinorSkills(skill, SkillIds[skill], null)
        }).ToList();
    }

    private static ItemConfiguration BuildItemConfiguration(CampaignTemplate template)
    {
        return new ItemConfiguration(template, new ItemConfigurationModel
        {
            ArmorPropertyId = MinorSkillIds[LandOfHeroesMinorSkill.Evasion],
            MeleeLightWeaponHitPropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
            MeleeMediumWeaponHitPropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeMediumWeapon],
            MeleeHeavyWeaponHitPropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeHeavyWeapon],
            
            MeleeLightWeaponDamagePropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
            MeleeMediumWeaponDamagePropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
            MeleeHeavyWeaponDamagePropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
            
            RangedLightWeaponHitPropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
            RangedMediumWeaponHitPropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
            RangedHeavyWeaponHitPropertyId = MinorSkillIds[LandOfHeroesMinorSkill.MeleeLightWeapon],
            
            RangedLightWeaponDamagePropertyId = MinorSkillIds[LandOfHeroesMinorSkill.RangedLightWeapon],
            RangedMediumWeaponDamagePropertyId = MinorSkillIds[LandOfHeroesMinorSkill.RangedMediumWeapon],
            RangedHeavyWeaponDamagePropertyId = MinorSkillIds[LandOfHeroesMinorSkill.RangedHeavyWeapon],
            
            BasicAttackTargetFirstLifeId = LifeIds[LandOfHeroesLife.Moral],
            BasicAttackTargetSecondLifeId = LifeIds[LandOfHeroesLife.Life],
        })
        {
            Id = Guid.Parse("985C54E0-C742-49BC-A3E0-8DD2D6CE2632"),
        };
    }

    private static List<CreatureType> Races =>
   [
    new()
    {
        Id = Guid.Parse("7DA646BA-637E-4CFF-99BC-5C3A934A1A63"),
        Name = "Elf",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("F9C10D96-CD38-4176-B770-7618E18AD0E3"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 2,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("D8E510F0-D49F-4623-95C6-FCF2CA8C24E2"),
        Name = "Human",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("108C8338-9168-428F-9C2F-DBAB52A846AC"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Intelligence], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 1,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("E9F2C583-167E-423D-97B8-6C0C3F92EEFF"),
        Name = "Dwarf",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("D613B3CB-3C19-44E9-B3CA-3EE41FB3816D"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Strength], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 2,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("D84B7197-A3EC-46C8-9B59-7E066D8FF8EF"),
        Name = "Halfling",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("3B5A9542-AAE1-4CAF-9FFD-7F7E963F6814"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 2,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("6E4E0713-CBD4-442E-9569-CF5073D80764"),
        Name = "Orc",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("6DF7D32C-E690-40C0-8EC4-D6BA087FDE89"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Strength], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 3,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("4E1D1FDC-9193-4FF2-B76F-8790B830798A"),
        Name = "Goblin",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("DA8B2C93-7F01-4BFB-809A-22B99B49EB9A"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 1,
                ValueType = BonusValueType.Roll,
            }
        ]
    },new()
    {
        Id = Guid.Parse("7DA646BA-637E-4CFF-99BC-5C3A934A1A63"),
        Name = "Elf",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("F9C10D96-CD38-4176-B770-7618E18AD0E3"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 2,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("D8E510F0-D49F-4623-95C6-FCF2CA8C24E2"),
        Name = "Human",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("108C8338-9168-428F-9C2F-DBAB52A846AC"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Intelligence], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 1,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("E9F2C583-167E-423D-97B8-6C0C3F92EEFF"),
        Name = "Dwarf",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("D613B3CB-3C19-44E9-B3CA-3EE41FB3816D"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Strength], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 2,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("D84B7197-A3EC-46C8-9B59-7E066D8FF8EF"),
        Name = "Halfling",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("3B5A9542-AAE1-4CAF-9FFD-7F7E963F6814"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 2,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("6E4E0713-CBD4-442E-9569-CF5073D80764"),
        Name = "Orc",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("6DF7D32C-E690-40C0-8EC4-D6BA087FDE89"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Strength], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 3,
                ValueType = BonusValueType.Roll,
            }
        ]
    },
    new()
    {
        Id = Guid.Parse("4E1D1FDC-9193-4FF2-B76F-8790B830798A"),
        Name = "Goblin",
        Description = "",
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("DA8B2C93-7F01-4BFB-809A-22B99B49EB9A"),
                Property = new Property(AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                Type = BonusType.Innate,
                Value = 1,
                ValueType = BonusValueType.Roll,
            }
        ]
    }
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
        return AttributeIds.Select(attributeEntry => new AttributeTemplate
        {
            Name = attributeEntry.Key.ToString(),
            Id = attributeEntry.Value,
            SkillTemplates = GetSkills(attributeEntry.Key, attributeEntry.Value)
        }).ToList();
    }

    private static ICollection<SkillTemplate> GetSkills(LandOfHeroesAttribute attribute, Guid attributeId)
    {
        var skills = AttributeSkills.ContainsKey(attribute) ? AttributeSkills[attribute] : new List<LandOfHeroesSkill>();

        return skills.Select(skill => new SkillTemplate
        {
            Name = skill.ToString(),
            Id = SkillIds[skill],
            AttributeTemplateId = attributeId,
            MinorSkills = GetMinorSkills(skill, SkillIds[skill], attributeId)
        }).ToList();
    }

    private static List<MinorSkillTemplate> GetMinorSkills(LandOfHeroesSkill skill, Guid skillId, Guid? attributeId)
    {
        var minorSkills = SkillMinorSkills.ContainsKey(skill) ? SkillMinorSkills[skill] : new List<LandOfHeroesMinorSkill>();

        return minorSkills.Select(minorSkill => new MinorSkillTemplate
        {
            Name = minorSkill.ToString(),
            Id = MinorSkillIds[minorSkill],
            SkillTemplateId = skillId,
            SkillTemplate = null,
            AttributeId = attributeId ?? AttributelessMinorSkillsAttributeId[minorSkill]
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
    public static Dictionary<LandOfHeroesAttribute, Guid> AttributeIds = new()
    {
        { LandOfHeroesAttribute.Agility, Guid.Parse("A94BA9AE-D800-4445-A996-19E6281FC0DD") },
        { LandOfHeroesAttribute.Charisma, Guid.Parse("0E2F1A7A-B39D-4C19-91CE-623A4E75D681") },
        { LandOfHeroesAttribute.Intelligence, Guid.Parse("F6E11C7E-C4F6-414E-8E92-B8C2C06A4F93") },
        { LandOfHeroesAttribute.Perception, Guid.Parse("D8C6D5C2-6D13-4E92-9C8F-31A7C74EAF52") },
        { LandOfHeroesAttribute.Strength, Guid.Parse("80D7685B-6E9A-44C2-AC99-BA3173B2C41E") },
        { LandOfHeroesAttribute.Vigor, Guid.Parse("1F5B3E5E-C5B8-44A3-A9B3-8B1A3F7444AB") },
    };
    public static Dictionary<LandOfHeroesLife, Guid> LifeIds = new()
    {
        { LandOfHeroesLife.Life, Guid.Parse("F3B7E2A9-8D41-4A6D-9C50-1E6BFD5A4D23") },
        { LandOfHeroesLife.Moral, Guid.Parse("7A92C5E3-4F8E-4C2E-98F7-5D3A1B8A6C91") },
    };

    public static Dictionary<LandOfHeroesSkill, Guid> SkillIds = new()
    {
        { LandOfHeroesSkill.Awareness, Guid.Parse("A8659BFC-33EA-4FE4-9E5F-213D6C8B583F") },
        { LandOfHeroesSkill.Empathy, Guid.Parse("63D26738-AC72-4C13-9D9C-9732AE97C172") },
        { LandOfHeroesSkill.Knowledge, Guid.Parse("F48792C4-B5A6-4BA1-B0AB-FB8F4E35C867") },
        { LandOfHeroesSkill.Nimbleness, Guid.Parse("7689F6A5-B8D7-4D92-99FA-52248B5C9A39") },
        { LandOfHeroesSkill.Survival, Guid.Parse("F6A7A4A2-B5E4-408E-A9A3-8FDD6C6C598A") },
        { LandOfHeroesSkill.Treatment, Guid.Parse("7F8C3E7B-36C4-432E-832F-7152AECD9A8C") },
        { LandOfHeroesSkill.Athletics, Guid.Parse("BCDEF8A1-7C6A-47A2-9D7A-1E6C8D1E3F22") },
        { LandOfHeroesSkill.Combat, Guid.Parse("D4CD2313-7AC5-4FF1-9F23-F534FA1CFBD6") },
        { LandOfHeroesSkill.Defense, Guid.Parse("C5614352-312C-4318-84D1-4253C97C3D40") },
    };
    
    public static Dictionary<LandOfHeroesMinorSkill, Guid> AttributelessMinorSkillsAttributeId = new()
    {
        { LandOfHeroesMinorSkill.MeleeLightWeapon, AttributeIds[LandOfHeroesAttribute.Agility]},
        { LandOfHeroesMinorSkill.MeleeMediumWeapon, AttributeIds[LandOfHeroesAttribute.Strength]},
        { LandOfHeroesMinorSkill.MeleeHeavyWeapon, AttributeIds[LandOfHeroesAttribute.Strength]},
        { LandOfHeroesMinorSkill.RangedLightWeapon, AttributeIds[LandOfHeroesAttribute.Agility]},
        { LandOfHeroesMinorSkill.RangedMediumWeapon, AttributeIds[LandOfHeroesAttribute.Strength]},
        { LandOfHeroesMinorSkill.RangedHeavyWeapon, AttributeIds[LandOfHeroesAttribute.Strength]},
        { LandOfHeroesMinorSkill.ResitInjury, AttributeIds[LandOfHeroesAttribute.Vigor]},
        { LandOfHeroesMinorSkill.ResistPoison, AttributeIds[LandOfHeroesAttribute.Vigor]},
        { LandOfHeroesMinorSkill.ResistCurse, AttributeIds[LandOfHeroesAttribute.Vigor]},
        { LandOfHeroesMinorSkill.ResistDisease, AttributeIds[LandOfHeroesAttribute.Vigor]},
        { LandOfHeroesMinorSkill.Evasion, AttributeIds[LandOfHeroesAttribute.Agility]},
    };

    public static Dictionary<LandOfHeroesMinorSkill, Guid> MinorSkillIds = new()
    {
        { LandOfHeroesMinorSkill.Acrobacy, Guid.Parse("12345678-9ABC-DEF0-1234-56789ABCDEF0") },
        { LandOfHeroesMinorSkill.Arcane, Guid.Parse("9D4E1C5A-B476-4832-8B52-1E7A5D6F3C5D") },
        { LandOfHeroesMinorSkill.Climb, Guid.Parse("A3F2E7B1-B547-4C28-AB53-7D5F4C8E32D6") },
        { LandOfHeroesMinorSkill.Diplomacy, Guid.Parse("76B4E8A3-C9F5-478D-97A3-6D1F2C8B7A4E") },
        { LandOfHeroesMinorSkill.Dungeons, Guid.Parse("89C7F4A2-BD5A-45E9-A7F3-1234567890AB") },
        { LandOfHeroesMinorSkill.Feeling, Guid.Parse("6A5B9D4C-E81F-41A7-93E8-4D5C9A7F6B32") },
        { LandOfHeroesMinorSkill.History, Guid.Parse("BCDEF8A1-7C6A-47A2-9D7A-2E6C9D1E4B5A") },
        { LandOfHeroesMinorSkill.Hide, Guid.Parse("AEF6A7C4-B9D3-43E2-8B6F-7C5D4A2E6F9D") },
        { LandOfHeroesMinorSkill.Listen, Guid.Parse("F6C7D5A3-EB41-48A7-92F3-4E7C8B5D1A9E") },
        { LandOfHeroesMinorSkill.Nature, Guid.Parse("1234F5E6-A7C8-492D-BF1A-8D4C7A3E5F92") },
        { LandOfHeroesMinorSkill.Observe, Guid.Parse("8A6B7C9D-E5F4-43A2-92D1-7E6C9B5D3F2A") },
        { LandOfHeroesMinorSkill.Performance, Guid.Parse("7F5E9A6B-C8D4-432E-A7B3-5D9C8A1F7E62") },
        { LandOfHeroesMinorSkill.Religion, Guid.Parse("F7D9E8A5-B6C3-4A29-91E7-C5D4A2F6B93E") },
        { LandOfHeroesMinorSkill.Run, Guid.Parse("6A5F4E9B-C3D7-4A1E-87A3-5D9C8B6F2E7A") },
        { LandOfHeroesMinorSkill.Search, Guid.Parse("4E9A6B5F-C7D3-42E9-81A7-9C5D8B6F3A2E") },
        { LandOfHeroesMinorSkill.Stealth, Guid.Parse("1234A7B6-E5F9-4C3D-8A2E-9B5F7C6D1E8A") },
        { LandOfHeroesMinorSkill.Swim, Guid.Parse("4A7F5E9B-C3D2-41E8-91A9-8B5C6D7A3F2E") },
        { LandOfHeroesMinorSkill.Jump, Guid.Parse("9A7B5F6E-C4D8-42E9-81A3-7C5D8B6A2F4E") },
        { LandOfHeroesMinorSkill.Bluff, Guid.Parse("F4D7C8A3-E9B5-4A1E-87C3-5D9A6B2E7F1A") },
        { LandOfHeroesMinorSkill.Evasion, Guid.Parse("CD04047D-6180-47FD-9397-8B8C80DDF882") },
        { LandOfHeroesMinorSkill.MeleeLightWeapon, Guid.Parse("8E9B6A5F-C7D4-41E8-92A3-7C5F8B6D3A2E") },
        { LandOfHeroesMinorSkill.MeleeMediumWeapon, Guid.Parse("F6C7A5D8-B9E3-48A1-87F2-3E9B8D5C6A7F") },
        { LandOfHeroesMinorSkill.MeleeHeavyWeapon, Guid.Parse("4A7E5F9B-C6D3-42A1-92E7-5C9B8F6A3D2E") },
        { LandOfHeroesMinorSkill.RangedLightWeapon, Guid.Parse("9C5A7B6E-F4D8-42A3-81E9-8B6F2D7C3A5E") },
        { LandOfHeroesMinorSkill.RangedMediumWeapon, Guid.Parse("F4A7B6E9-C8D3-41E2-97A3-5D9B6C8A2F7E") },
        { LandOfHeroesMinorSkill.RangedHeavyWeapon, Guid.Parse("8D5C7A9F-B4E6-4A3D-92E1-7C9A6B5F3D8E") },
        { LandOfHeroesMinorSkill.ResitInjury, Guid.Parse("5A4B3C2D-E1F0-9876-5432-10FEDCBA9876") },
        { LandOfHeroesMinorSkill.ResistPoison, Guid.Parse("6B5C4D3E-F201-0987-6543-21FEDCBA0987") },
        { LandOfHeroesMinorSkill.ResistCurse, Guid.Parse("7C6D5E4F-0312-1098-7654-32FEDCBA1098") },
        { LandOfHeroesMinorSkill.ResistDisease, Guid.Parse("8D7E6F50-1423-2109-8765-43FEDCBA2109") },
        
        { LandOfHeroesMinorSkill.Injury, Guid.Parse("834A6BBA-86BA-43F4-A80E-DA67EB5867C5") },
        { LandOfHeroesMinorSkill.Poison, Guid.Parse("D87F7849-074D-42DF-A028-648CCABDE329") },
        { LandOfHeroesMinorSkill.Curse, Guid.Parse("F233497C-D56E-4210-B7FC-3AF84EDB7B62") },
        { LandOfHeroesMinorSkill.Disease, Guid.Parse("91CE4E24-AEAA-4B71-8E2D-F990C6C7B053") },
        
        { LandOfHeroesMinorSkill.Hunger, Guid.Parse("596376CF-C313-4834-9B53-8772BC33983B") },
        { LandOfHeroesMinorSkill.ColdWeather, Guid.Parse("37C78661-29FB-4332-BE37-EF2162FA1D5C") },
        { LandOfHeroesMinorSkill.WarmWeather, Guid.Parse("D08DF7D0-F477-4472-884D-F10B09806FF7") },
    };
    public static Dictionary<LandOfHeroesAttribute, List<LandOfHeroesSkill>> AttributeSkills =>
        new()
        {
            { LandOfHeroesAttribute.Agility, new List<LandOfHeroesSkill> { LandOfHeroesSkill.Nimbleness } },
            { LandOfHeroesAttribute.Charisma, new List<LandOfHeroesSkill> { LandOfHeroesSkill.Empathy } },
            { LandOfHeroesAttribute.Intelligence, new List<LandOfHeroesSkill> { LandOfHeroesSkill.Knowledge, LandOfHeroesSkill.Treatment } },
            { LandOfHeroesAttribute.Perception, new List<LandOfHeroesSkill> { LandOfHeroesSkill.Awareness } },
            { LandOfHeroesAttribute.Strength, new List<LandOfHeroesSkill> { LandOfHeroesSkill.Athletics } },
            { LandOfHeroesAttribute.Vigor, new List<LandOfHeroesSkill> { LandOfHeroesSkill.Survival } }
        };
    public static Dictionary<LandOfHeroesSkill, List<LandOfHeroesMinorSkill>> SkillMinorSkills =>
        new()
        {
            { LandOfHeroesSkill.Awareness, new List<LandOfHeroesMinorSkill> { LandOfHeroesMinorSkill.Observe, LandOfHeroesMinorSkill.Listen, LandOfHeroesMinorSkill.Search, LandOfHeroesMinorSkill.Feeling } },
            { LandOfHeroesSkill.Empathy, new List<LandOfHeroesMinorSkill> { LandOfHeroesMinorSkill.Diplomacy, LandOfHeroesMinorSkill.Bluff } },
            { LandOfHeroesSkill.Knowledge, new List<LandOfHeroesMinorSkill> { LandOfHeroesMinorSkill.History, LandOfHeroesMinorSkill.Arcane, LandOfHeroesMinorSkill.Religion, LandOfHeroesMinorSkill.Nature, LandOfHeroesMinorSkill.Dungeons } },
            { LandOfHeroesSkill.Nimbleness, new List<LandOfHeroesMinorSkill> { LandOfHeroesMinorSkill.Acrobacy, LandOfHeroesMinorSkill.Hide, LandOfHeroesMinorSkill.Stealth } },
            { LandOfHeroesSkill.Survival, new List<LandOfHeroesMinorSkill> { LandOfHeroesMinorSkill.Hunger, LandOfHeroesMinorSkill.ColdWeather, LandOfHeroesMinorSkill.WarmWeather, } },
            { LandOfHeroesSkill.Treatment, new List<LandOfHeroesMinorSkill> { LandOfHeroesMinorSkill.Injury, LandOfHeroesMinorSkill.Poison, LandOfHeroesMinorSkill.Curse, LandOfHeroesMinorSkill.Disease } },
            { LandOfHeroesSkill.Athletics, new List<LandOfHeroesMinorSkill> { LandOfHeroesMinorSkill.Swim, LandOfHeroesMinorSkill.Run, LandOfHeroesMinorSkill.Climb, LandOfHeroesMinorSkill.Jump } },
            { LandOfHeroesSkill.Combat, new List<LandOfHeroesMinorSkill> 
                { 
                    LandOfHeroesMinorSkill.MeleeLightWeapon, 
                    LandOfHeroesMinorSkill.MeleeMediumWeapon, 
                    LandOfHeroesMinorSkill.MeleeHeavyWeapon, 
                    LandOfHeroesMinorSkill.RangedLightWeapon, 
                    LandOfHeroesMinorSkill.RangedMediumWeapon, 
                    LandOfHeroesMinorSkill.RangedHeavyWeapon,
                } 
            },       
            { LandOfHeroesSkill.Defense, new List<LandOfHeroesMinorSkill> 
                { 
                    LandOfHeroesMinorSkill.ResitInjury, 
                    LandOfHeroesMinorSkill.ResistPoison, 
                    LandOfHeroesMinorSkill.ResistCurse, 
                    LandOfHeroesMinorSkill.ResistDisease, 
                    LandOfHeroesMinorSkill.Evasion, 
                } 
            }
        };

}