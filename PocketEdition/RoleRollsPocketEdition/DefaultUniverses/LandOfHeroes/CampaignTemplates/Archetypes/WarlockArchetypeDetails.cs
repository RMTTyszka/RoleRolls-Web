using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class WarlockArchetypeDetails
{
    public static Archetype WarlockArchetype =>
        new()
        {
            Name = "Warlock",
            Description = "",
            Details = "",
            Id = Guid.Parse("0e20b28f-8b53-446e-a0e1-cd0b546e1e39"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("4e150329-37b6-4263-9dc9-caa0d4723388"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Knowledge], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Dark Pact I" },
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Hex 2/day" },
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Dark Knowledge" },
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Spellcaster" },
                new() { Id = Guid.NewGuid(), Level = 2, Name = "Dark Power" },
                new() { Id = Guid.NewGuid(), Level = 3, Name = "Curse Channel" },
                new() { Id = Guid.NewGuid(), Level = 4, Name = "Hex 4/day" },
                new() { Id = Guid.NewGuid(), Level = 5, Name = "Dark Pact II" },
                new() { Id = Guid.NewGuid(), Level = 6, Name = "Hex Range +1" },
                new() { Id = Guid.NewGuid(), Level = 7, Name = "Hex 6/day" },
                new() { Id = Guid.NewGuid(), Level = 8, Name = "Dark Pact III" },
                new() { Id = Guid.NewGuid(), Level = 9, Name = "Corrupted Ritual" },
                new() { Id = Guid.NewGuid(), Level = 10, Name = "Hex 8/day" },
                new() { Id = Guid.NewGuid(), Level = 11, Name = "Dark Pact IV" },
                new() { Id = Guid.NewGuid(), Level = 12, Name = "Hex Master" },
                new() { Id = Guid.NewGuid(), Level = 13, Name = "Dark Pact V" },
                new() { Id = Guid.NewGuid(), Level = 14, Name = "Hex 10/day" },
                new() { Id = Guid.NewGuid(), Level = 15, Name = "Greater Curse Channel" },
                new() { Id = Guid.NewGuid(), Level = 16, Name = "Dark Pact Master" },
                new() { Id = Guid.NewGuid(), Level = 20, Name = "Harbinger of Darkness" }
            ]
        };
}
