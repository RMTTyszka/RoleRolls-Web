using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class MartialistArchetypeDetails
{
    public static Archetype MartialistArchetype =>
        new()
        {
            Name = "Martialist",
            Description = "",
            Details = "",
            Id = Guid.Parse("8036e058-bc2c-4c77-a2ae-aa20d27dfe3a"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("913ffad2-33d3-4ef7-9621-effbfcecf82f"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Combat], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Martial School I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Martial Strike" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Deflect Projectile" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Stunning Fist 1/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "Ki Infusion I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 4, Name = "Martial School II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 5, Name = "Stunning Fist 2/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 6, Name = "Ki Infusion II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 7, Name = "Martial School III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Stunning Fist 3/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 9, Name = "Ki Infusion III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 10, Name = "Martial School IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 11, Name = "Stunning Fist 4/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 12, Name = "Ki Infusion IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 13, Name = "Martial School V" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 14, Name = "Stunning Fist 5/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 15, Name = "Ki Mastery" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 16, Name = "Perfect Body" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 20, Name = "Avatar of Discipline" }
            ]
        };
}
