using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class BarbarianArchetypeDetails
{
    public static Archetype BarbarianArchetype =>
        new()
        {
            Name = "Barbarian",
            Description = "",
            Details = "",
            Id = Guid.Parse("b082a02d-1718-4b8f-bbbb-0bc37cce4e93"),
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("a767180f-cd59-41f4-9b25-2cf83faeca2c"),
                    Value = 1,
                    Property = new Property(LandOfHeroesSkills.SkillIds[LandOfHeroesSkill.Combat], PropertyType.Skill),
                    Application = BonusApplication.Property,
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff
                }
            ],
            PowerDescriptions =
            [
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Rage 1/day" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 1, Name = "Tribal Order I" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Rage 2/day" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Unshakable" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 2, Name = "Urgent Strength 1/day" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "Execute 1" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 3, Name = "Primal" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 4, Name = "Battle Frenzy 1" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 5, Name = "Battle Cry 1" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 6, Name = "Rage 3/day" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 7, Name = "Execute 2" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Battle Frenzy 2" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 8, Name = "Urgent Strength 2/day" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 9, Name = "Battle Cry 2" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 10, Name = "Rage 4/day" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 11, Name = "Tribal Order II" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 12, Name = "Execute 3" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 13, Name = "Battle Frenzy 3" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 14, Name = "Battle Cry 3" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 14, Name = "Urgent Strength 3/day" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 15, Name = "Rage 5/day" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 16, Name = "Tribal Order Leader" },
                new ArchertypePowerDescription { Id = Guid.NewGuid(), RequiredLevel = 20, Name = "Urgent Strength 4/day" }
            ]
        };
}
