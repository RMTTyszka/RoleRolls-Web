using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class SpiritualistArchetypeDetails
{
    public static Archetype SpiritualistArchetype =>
        new()
        {
            Name = "Spiritualist",
            Description = "",
            Details = "",
            Id = Guid.Parse("7fb26adb-21e7-405e-a1a7-06d5eeb01da3"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("85de058e-64fe-4f7f-b49d-df5cfbb84d19"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Treatment], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Spiritual Connection" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Divine Path I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Turn Undead I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Divine Magic" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "Aura of Protection" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 4, Name = "Divine Path II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 5, Name = "Turn Undead II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 6, Name = "Divine Intervention I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 7, Name = "Divine Path III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Turn Undead III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 9, Name = "Divine Intervention II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 10, Name = "Divine Path IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 11, Name = "Turn Undead IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 12, Name = "Divine Intervention III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 13, Name = "Divine Path V" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 14, Name = "Turn Undead V" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 15, Name = "Divine Intervention IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 16, Name = "Greater Aura of Protection" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 20, Name = "Saint's Legacy" }
            ]
        };
}


