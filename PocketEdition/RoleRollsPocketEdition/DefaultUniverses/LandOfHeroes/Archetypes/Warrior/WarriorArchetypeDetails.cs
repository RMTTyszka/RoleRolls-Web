using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class WarriorArchetypeDetails
{
    public static Archetype  WarriorArchetype =>

        new()
        {
            Name = "Warrior",
            Description = "",
            Details = "",
            Id = Guid.Parse("d3d9b663-7486-40f7-87e6-8c873cedcdde"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("b082a02d-1718-4b8f-bbbb-0bc37cce4e93"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Combat], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("C79A18A2-3383-4199-86CE-C6AD76C08FAE"),
                    RequiredLevel = 1,
                    Name = "Fighting Style",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("1A2B3C4D-5E6F-7A8B-9C0D-1E2F3A4B5C6D"),
                    RequiredLevel = 1,
                    Name = "Threatening Area",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("2B3C4D5E-6F7A-8B9C-0D1E-2F3A4B5C6D7E"),
                    RequiredLevel = 2,
                    Name = "Military Grade",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("3C4D5E6F-7A8B-9C0D-1E2F-3A4B5C6D7E8F"),
                    RequiredLevel = 3,
                    Name = "War Veteran",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("4D5E6F7A-8B9C-0D1E-2F3A-4B5C6D7E8F9A"),
                    RequiredLevel = 3,
                    Name = "Power Attack",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("5E6F7A8B-9C0D-1E2F-3A4B-5C6D7E8F9A0B"),
                    RequiredLevel = 4,
                    Name = "Technical Attack",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("6F7A8B9C-0D1E-2F3A-4B5C-6D7E8F9A0B1C"),
                    RequiredLevel = 5,
                    Name = "Armor Mastery",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("7A8B9C0D-1E2F-3A4B-5C6D-7E8F9A0B1C2D"),
                    RequiredLevel = 6,
                    Name = "Combat Stance",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("8B9C0D1E-2F3A-4B5C-6D7E-8F9A0B1C2D3E"),
                    RequiredLevel = 7,
                    Name = "Energy Surge",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("9C0D1E2F-3A4B-5C6D-7E8F-9A0B1C2D3E4F"),
                    RequiredLevel = 8,
                    Name = "Weapon Mastery",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("0D1E2F3A-4B5C-6D7E-8F9A-0B1C2D3E4F5A"),
                    RequiredLevel = 9,
                    Name = "War Champion",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("1E2F3A4B-5C6D-7E8F-9A0B-1C2D3E4F5A6B"),
                    RequiredLevel = 10,
                    Name = "Epic Defender",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("2F3A4B5C-6D7E-8F9A-0B1C-2D3E4F5A6B7C"),
                    RequiredLevel = 11,
                    Name = "Threatening Area",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("3A4B5C6D-7E8F-9A0B-1C2D-3E4F5A6B7C8D"),
                    RequiredLevel = 12,
                    Name = "Power Attack II",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("4B5C6D7E-8F9A-0B1C-2D3E-4F5A6B7C8D9E"),
                    RequiredLevel = 13,
                    Name = "Technical Attack II",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("5C6D7E8F-9A0B-1C2D-3E4F-5A6B7C8D9E0F"),
                    RequiredLevel = 14,
                    Name = "Combat Stance II",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("6D7E8F9A-0B1C-2D3E-4F5A-6B7C8D9E0F1A"),
                    RequiredLevel = 15,
                    Name = "Energy Surge II",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("7E8F9A0B-1C2D-3E4F-5A6B-7C8D9E0F1A2B"),
                    RequiredLevel = 16,
                    Name = "War Champion II",
                    Description = "",
                    GameDescription = ""
                },

                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("8F9A0B1C-2D3E-4F5A-6B7C-8D9E0F1A2B3C"),
                    RequiredLevel = 17,
                    Name = "Epic Defender II",
                    Description = "",
                    GameDescription = ""
                }
            ]
        };
}