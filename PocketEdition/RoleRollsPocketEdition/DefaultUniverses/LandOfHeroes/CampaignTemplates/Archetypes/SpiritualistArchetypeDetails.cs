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
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Spiritual Connection" },
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Divine Path I" },
                new() { Id = Guid.NewGuid(), Level = 2, Name = "Turn Undead I" },
                new() { Id = Guid.NewGuid(), Level = 2, Name = "Divine Magic" },
                new() { Id = Guid.NewGuid(), Level = 3, Name = "Aura of Protection" },
                new() { Id = Guid.NewGuid(), Level = 4, Name = "Divine Path II" },
                new() { Id = Guid.NewGuid(), Level = 5, Name = "Turn Undead II" },
                new() { Id = Guid.NewGuid(), Level = 6, Name = "Divine Intervention I" },
                new() { Id = Guid.NewGuid(), Level = 7, Name = "Divine Path III" },
                new() { Id = Guid.NewGuid(), Level = 8, Name = "Turn Undead III" },
                new() { Id = Guid.NewGuid(), Level = 9, Name = "Divine Intervention II" },
                new() { Id = Guid.NewGuid(), Level = 10, Name = "Divine Path IV" },
                new() { Id = Guid.NewGuid(), Level = 11, Name = "Turn Undead IV" },
                new() { Id = Guid.NewGuid(), Level = 12, Name = "Divine Intervention III" },
                new() { Id = Guid.NewGuid(), Level = 13, Name = "Divine Path V" },
                new() { Id = Guid.NewGuid(), Level = 14, Name = "Turn Undead V" },
                new() { Id = Guid.NewGuid(), Level = 15, Name = "Divine Intervention IV" },
                new() { Id = Guid.NewGuid(), Level = 16, Name = "Greater Aura of Protection" },
                new() { Id = Guid.NewGuid(), Level = 20, Name = "Saint's Legacy" }
            ]
        };
}
