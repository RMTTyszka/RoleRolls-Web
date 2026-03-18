using RoleRollsPocketEdition.CreatureTypes.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Bestiary;

public static class LandOfHeroesMonsterTypes
{
    public static List<CreatureType> Monsters =>
    [
        new()
        {
            Id = Guid.Parse("BB561114-F686-4ABE-A530-A1D741354B66"),
            Name = "Skeleton",
            Description = "Animated bones held together by necromantic force.",
            CanBeEnemy = true,
            Bonuses = []
        },
        new()
        {
            Id = Guid.Parse("A859A436-2EEF-493E-9A2E-84560A420A72"),
            Name = "Zombie",
            Description = "A shambling corpse that keeps advancing after heavy punishment.",
            CanBeEnemy = true,
            Bonuses = []
        },
        new()
        {
            Id = Guid.Parse("B2D63565-7F5D-48D6-91E9-3A06885EE52F"),
            Name = "Wolf",
            Description = "A fast hunter that relies on pack tactics and instinct.",
            CanBeEnemy = true,
            Bonuses = []
        },
        new()
        {
            Id = Guid.Parse("CEE9BB6E-9762-484B-88B9-E7CCD332EA86"),
            Name = "Giant Spider",
            Description = "A lurking ambusher that strikes with poison and webs.",
            CanBeEnemy = true,
            Bonuses = []
        },
        new()
        {
            Id = Guid.Parse("5BEF5DF8-387B-4662-A520-AAEDB11230CD"),
            Name = "Ogre",
            Description = "A brutal powerhouse that overwhelms foes with raw strength.",
            CanBeEnemy = true,
            Bonuses = []
        }
    ];
}
