using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class SpellcasterArchetypeDetails
{
    public static Archetype SpellcasterArchetype =>
        new()
        {
            Name = "Spellcaster",
            Description = "",
            Details = "",
            Id = Guid.Parse("8b156640-9b67-43e7-bdfe-b1f3529a30d5"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("d3d9b663-7486-40f7-87e6-8c873cedcdde"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Knowledge], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), Level = 1, Name = "School of Magic I" },
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Spellbook" },
                new() { Id = Guid.NewGuid(), Level = 2, Name = "Magic Affinity I" },
                new() { Id = Guid.NewGuid(), Level = 3, Name = "Counterspell 1/day" },
                new() { Id = Guid.NewGuid(), Level = 4, Name = "Magic Affinity II" },
                new() { Id = Guid.NewGuid(), Level = 5, Name = "Counterspell 2/day" },
                new() { Id = Guid.NewGuid(), Level = 6, Name = "School of Magic II" },
                new() { Id = Guid.NewGuid(), Level = 7, Name = "Magic Affinity III" },
                new() { Id = Guid.NewGuid(), Level = 8, Name = "Counterspell 3/day" },
                new() { Id = Guid.NewGuid(), Level = 9, Name = "Magic Mastery I" },
                new() { Id = Guid.NewGuid(), Level = 10, Name = "Magic Affinity IV" },
                new() { Id = Guid.NewGuid(), Level = 11, Name = "Counterspell 4/day" },
                new() { Id = Guid.NewGuid(), Level = 12, Name = "School of Magic III" },
                new() { Id = Guid.NewGuid(), Level = 13, Name = "Magic Affinity V" },
                new() { Id = Guid.NewGuid(), Level = 14, Name = "Counterspell 5/day" },
                new() { Id = Guid.NewGuid(), Level = 15, Name = "Magic Mastery II" },
                new() { Id = Guid.NewGuid(), Level = 16, Name = "School of Magic IV" },
                new() { Id = Guid.NewGuid(), Level = 20, Name = "Archmage" }
            ]
        };
}
