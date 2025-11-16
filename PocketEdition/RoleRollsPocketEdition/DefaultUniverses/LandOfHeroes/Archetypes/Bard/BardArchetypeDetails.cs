using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class BardArchetypeDetails
{
    public static Archetype BardArchetype =>
        new()
        {
            Name = "Bard",
            Description = "",
            Details = "",
            Id = Guid.Parse("a767180f-cd59-41f4-9b25-2cf83faeca2c"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("396e8751-df67-41db-b7b9-adfc108c978c"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Empathy], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Bard School" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Battle Chant 2" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Bardic Knowledge" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Curiosity" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Spellcaster" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Captivating Songs 2/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "War Tune 1/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "Skill Maneuver I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 4, Name = "Terrifying Melody 1/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 4, Name = "Battle Chant 4" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 5, Name = "Captivating Songs 4/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 6, Name = "War Tune 2/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 6, Name = "Extraordinary Ability I" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 7, Name = "Terrifying Melody 2/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 7, Name = "Skill Maneuver II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Captivating Songs 6/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Greater Bard School" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 9, Name = "Battle Chant 6" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 10, Name = "War Tune 3/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 11, Name = "Skill Maneuver III" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 11, Name = "Captivating Songs 8/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 12, Name = "Extraordinary Ability II" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 12, Name = "Terrifying Melody 3/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 13, Name = "War Tune 4/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 14, Name = "Battle Chant 8" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 15, Name = "Captivating Songs 10/day" },
                new() { Id = Guid.NewGuid(), RequiredLevel = 16, Name = "Terrifying Melody 4/day" }
            ]
        };
}


