using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Races;

public static class LandOfHeroesRaces
{
    public static List<CreatureType> Races =>
    [
        new()
        {
            Id = Guid.Parse("7DA646BA-637E-4CFF-99BC-5C3A934A1A63"),
            Name = "Elf",
            Description = "",
            CanBeAlly = true,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("F9C10D96-CD38-4176-B770-7618E18AD0E3"),
                    Property = new Property(LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff,
                    Value = 2,
                    Application = BonusApplication.Property,
                }
            ]
        },
        new()
        {
            Id = Guid.Parse("D8E510F0-D49F-4623-95C6-FCF2CA8C24E2"),
            Name = "Human",
            Description = "",
            CanBeAlly = true,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("108C8338-9168-428F-9C2F-DBAB52A846AC"),
                    Property = new Property(LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Intelligence], PropertyType.Attribute),
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff,
                    Value = 1,
                    Application = BonusApplication.Property,
                }
            ]
        },
        new()
        {
            Id = Guid.Parse("E9F2C583-167E-423D-97B8-6C0C3F92EEFF"),
            Name = "Dwarf",
            Description = "",
            CanBeAlly = true,
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("D613B3CB-3C19-44E9-B3CA-3EE41FB3816D"),
                    Property = new Property(LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength], PropertyType.Attribute),
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff,
                    Value = 2,
                    Application = BonusApplication.Property,
                }
            ]
        },
        new()
        {
            Id = Guid.Parse("D84B7197-A3EC-46C8-9B59-7E066D8FF8EF"),
            Name = "Halfling",
            CanBeAlly = true,
            Description = "",
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("3B5A9542-AAE1-4CAF-9FFD-7F7E963F6814"),
                    Property = new Property(LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff,
                    Value = 2,
                    Application = BonusApplication.Property,
                }
            ]
        },
        new()
        {
            Id = Guid.Parse("6E4E0713-CBD4-442E-9569-CF5073D80764"),
            Name = "Orc",
            CanBeAlly = true,
            Description = "",
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("6DF7D32C-E690-40C0-8EC4-D6BA087FDE89"),
                    Property = new Property(LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Strength], PropertyType.Attribute),
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff,
                    Value = 3,
                    Application = BonusApplication.Property,
                }
            ]
        },
        new()
        {
            Id = Guid.Parse("4E1D1FDC-9193-4FF2-B76F-8790B830798A"),
            Name = "Goblin",
            CanBeAlly = true,
            Description = "",
            Bonuses =
            [
                new Bonus
                {
                    Id = Guid.Parse("DA8B2C93-7F01-4BFB-809A-22B99B49EB9A"),
                    Property = new Property(LandOfHeroesAttributes.AttributeIds[LandOfHeroesAttribute.Agility], PropertyType.Attribute),
                    Origin = BonusOrigin.Innate,
                    Type = BonusType.Buff,
                    Value = 1,
                    Application = BonusApplication.Property,
                }
            ]
        }
    ];
}