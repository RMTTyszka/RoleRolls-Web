using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class RogueArchetypeDetails
{
    public static Archetype RogueArchetype =>
        new()
        {
            Name = "Rogue",
            Description = "",
            Details = "",
            Id = Guid.Parse("f44b25b9-2e87-495e-83eb-de9b8802df1f"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("dcd0ab9d-0e95-448b-ba42-8aead55b6c62"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Nimbleness], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Rogue Path I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Backstab" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Sneak" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Disarm Trap" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "Combat Reflexes I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 4, Name = "Poison Use" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 5, Name = "Combat Reflexes II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 6, Name = "Rogue Path II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 7, Name = "Evasion" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Combat Reflexes III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 9, Name = "Rogue Path III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 10, Name = "Uncanny Dodge" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 11, Name = "Combat Reflexes IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 12, Name = "Master of Traps" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 13, Name = "Rogue Path IV" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 14, Name = "Combat Reflexes V" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 15, Name = "Greater Backstab" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 16, Name = "Rogue Path V" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 20, Name = "Master of Shadows" }
            ]
        };
}
