using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class HunterArchetypeDetails
{
    public static Archetype HunterArchetype =>
        new()
        {
            Name = "Hunter",
            Description = "",
            Details = "",
            Id = Guid.Parse("396e8751-df67-41db-b7b9-adfc108c978c"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("655394b6-e1bf-4175-90e8-cdb95da1613c"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Nimbleness], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Mark Prey 1" },
                new() { Id = Guid.NewGuid(), Level = 1, Name = "Hunter’s Focus" },
                new() { Id = Guid.NewGuid(), Level = 2, Name = "Animal Bond I" },
                new() { Id = Guid.NewGuid(), Level = 2, Name = "Hide in Nature I" },
                new() { Id = Guid.NewGuid(), Level = 3, Name = "Combat Style I" },
                new() { Id = Guid.NewGuid(), Level = 4, Name = "Sharpened Senses" },
                new() { Id = Guid.NewGuid(), Level = 5, Name = "Animal Bond II" },
                new() { Id = Guid.NewGuid(), Level = 6, Name = "Hide in Nature II" },
                new() { Id = Guid.NewGuid(), Level = 6, Name = "Combat Style II" },
                new() { Id = Guid.NewGuid(), Level = 7, Name = "Mark Prey 2" },
                new() { Id = Guid.NewGuid(), Level = 8, Name = "Animal Bond III" },
                new() { Id = Guid.NewGuid(), Level = 9, Name = "Hide in Nature III" },
                new() { Id = Guid.NewGuid(), Level = 10, Name = "Combat Style III" },
                new() { Id = Guid.NewGuid(), Level = 11, Name = "Perfect Aim" },
                new() { Id = Guid.NewGuid(), Level = 12, Name = "Animal Bond IV" },
                new() { Id = Guid.NewGuid(), Level = 13, Name = "Hide in Nature IV" },
                new() { Id = Guid.NewGuid(), Level = 14, Name = "Combat Style IV" },
                new() { Id = Guid.NewGuid(), Level = 15, Name = "Mark Prey 3" },
                new() { Id = Guid.NewGuid(), Level = 16, Name = "Animal Bond V" },
                new() { Id = Guid.NewGuid(), Level = 20, Name = "True Hunter" }
            ]
        };
}
