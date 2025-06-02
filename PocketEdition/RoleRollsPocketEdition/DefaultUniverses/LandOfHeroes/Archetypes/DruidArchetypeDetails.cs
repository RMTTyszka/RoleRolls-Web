using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class DruidArchetypeDetails
{
    public static Archetype DruidArchetype =>
        new()
        {
            Name = "Druid",
            Description = "",
            Details = "",
            Id = Guid.Parse("3f15b9de-0bd5-43ce-b2af-4aad91b5b9a4"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("fd3b88be-b55d-4c0b-8010-9c1b56eef367"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Awareness], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Natural Magic" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Wild Bond I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Nature’s Call" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "Natural Healing" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 4, Name = "Wild Bond II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 5, Name = "Beast Form I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 6, Name = "Greater Nature’s Call" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 7, Name = "Wild Bond III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Beast Form II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 9, Name = "Nature’s Barrier" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 10, Name = "Wild Bond IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 11, Name = "Beast Form III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 12, Name = "Nature’s Wrath" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 13, Name = "Wild Bond V" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 14, Name = "Beast Form IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 15, Name = "Call of the Great Spirit" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 16, Name = "Avatar of Nature" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 20, Name = "Perfect Druid" }
            ]
        };
}
