using System.Text;
using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;

public static partial class WarlockArchetypeDetails
{
    private static string LoadPowerDescription(string powerName)
    {
        var basePath = Path.Combine(AppContext.BaseDirectory, "DefaultUniverses", "LandOfHeroes", "Archetypes", "Warlock", "Powers");
        var fileName = $"{powerName.Replace(" ", "")}.md";
        var path = Path.Combine(basePath, fileName);
        
        if (File.Exists(path))
        {
            return File.ReadAllText(path, Encoding.UTF8);
        }
        
        return string.Empty;
    }

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
                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("f9a8b7c6-d5e4-4f3a-2b1c-0a9b8c7d6e5f"), RequiredLevel = 0, Name = "Hex",
                    GameDescription = LoadPowerDescription("Hex")
                },
                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("e8d7c6b5-a4f3-2e1d-0c9b-8a7d6e5f4c3b"), RequiredLevel = 1, Name = "Hex 1/Combat"
                },
                new ArchertypePowerDescription
                {
                    Id = Guid.Parse("d7c6b5a4-f3e2-1d0c-9b8a-7d6e5f4c3b2a"), RequiredLevel = 1, Name = "Linhagem" 
                        
                },
                new ArchertypePowerDescription { Id = Guid.Parse("c6b5a4f3-e2d1-0c9b-8a7d-6e5f4c3b2a1d"), RequiredLevel = 1, Name = "Spellcaster" },
                new ArchertypePowerDescription { Id = Guid.Parse("b5a4f3e2-d10c-9b8a-7d6e-5f4c3b2a1d0e"), RequiredLevel = 2, Name = "Dark Power" },
                new ArchertypePowerDescription { Id = Guid.Parse("a4f3e2d1-0c9b-8a7d-6e5f-4c3b2a1d0e9f"), RequiredLevel = 3, Name = "Curse Channel" },
                new ArchertypePowerDescription { Id = Guid.Parse("93e2d10c-9b8a-7d6e-5f4c-3b2a1d0e9f8a"), RequiredLevel = 4, Name = "Hex 4/day" },
                new ArchertypePowerDescription { Id = Guid.Parse("82d10c9b-8a7d-6e5f-4c3b-2a1d0e9f8a7b"), RequiredLevel = 5, Name = "Dark Pact II" },
                new ArchertypePowerDescription { Id = Guid.Parse("71c09b8a-7d6e-5f4c-3b2a-1d0e9f8a7b6c"), RequiredLevel = 6, Name = "Hex Range +1" },
                new ArchertypePowerDescription { Id = Guid.Parse("60b98a7d-6e5f-4c3b-2a1d-0e9f8a7b6c5d"), RequiredLevel = 7, Name = "Hex 6/day" },
                new ArchertypePowerDescription { Id = Guid.Parse("5a87d6e5-f4c3-b2a1-d0e9-f8a7b6c5d4e3"), RequiredLevel = 8, Name = "Dark Pact III" },
                new ArchertypePowerDescription { Id = Guid.Parse("49d6e5f4-c3b2-a1d0-e9f8-a7b6c5d4e3f2"), RequiredLevel = 9, Name = "Corrupted Ritual" },
                new ArchertypePowerDescription { Id = Guid.Parse("38e5f4c3-b2a1-d0e9-f8a7-b6c5d4e3f2a1"), RequiredLevel = 10, Name = "Hex 8/day" },
                new ArchertypePowerDescription { Id = Guid.Parse("27f4c3b2-a1d0-e9f8-a7b6-c5d4e3f2a190"), RequiredLevel = 11, Name = "Dark Pact IV" },
                new ArchertypePowerDescription { Id = Guid.Parse("16c3b2a1-d0e9-f8a7-b6c5-d4e3f2a19089"), RequiredLevel = 12, Name = "Hex Master" },
                new ArchertypePowerDescription { Id = Guid.Parse("05b2a1d0-e9f8-a7b6-c5d4-e3f2a1908978"), RequiredLevel = 13, Name = "Dark Pact V" },
                new ArchertypePowerDescription { Id = Guid.Parse("f4a1d0e9-f8a7-b6c5-d4e3-f2a190897867"), RequiredLevel = 14, Name = "Hex 10/day" },
                new ArchertypePowerDescription { Id = Guid.Parse("e390d0e9-f8a7-b6c5-d4e3-f2a190897856"), RequiredLevel = 15, Name = "Greater Curse Channel" },
                new ArchertypePowerDescription { Id = Guid.Parse("d280c0e9-f8a7-b6c5-d4e3-f2a190897845"), RequiredLevel = 16, Name = "Dark Pact Master" },
                new ArchertypePowerDescription { Id = Guid.Parse("c170b0e9-f8a7-b6c5-d4e3-f2a190897834"), RequiredLevel = 20, Name = "Harbinger of Darkness" }
            ]
        };
}
