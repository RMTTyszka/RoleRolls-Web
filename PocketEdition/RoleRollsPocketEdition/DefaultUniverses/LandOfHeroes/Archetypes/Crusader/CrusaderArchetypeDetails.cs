using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class CrusaderArchetypeDetails
{
    public static Archetype CrusaderArchetype =>
        new()
        {
            Name = "Crusader",
            Description = "",
            Details = "",
            Id = Guid.Parse("8FD3CF61-8BAB-4328-BFAB-89247BF1371D"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("172B2C5D-160B-4760-9D87-0879E265BAD8"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Combat], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Divine Oath" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Lay on Hands 1/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Aura of Courage" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "Smite Evil 1/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 4, Name = "Divine Justice I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 5, Name = "Lay on Hands 2/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 6, Name = "Smite Evil 2/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 7, Name = "Divine Justice II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Lay on Hands 3/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 9, Name = "Smite Evil 3/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 10, Name = "Divine Justice III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 11, Name = "Lay on Hands 4/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 12, Name = "Smite Evil 4/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 13, Name = "Divine Justice IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 14, Name = "Lay on Hands 5/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 15, Name = "Smite Evil 5/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 16, Name = "Divine Justice V" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 20, Name = "Avatar of the Cause" }
            ]
        };
}
