namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Attributes;

public static class LandOfHeroesAttributes
{
    public static Dictionary<LandOfHeroesAttribute, Guid> AttributeIds = new()
    {
        { LandOfHeroesAttribute.Agility, Guid.Parse("A94BA9AE-D800-4445-A996-19E6281FC0DD") },
        { LandOfHeroesAttribute.Charisma, Guid.Parse("0E2F1A7A-B39D-4C19-91CE-623A4E75D681") },
        { LandOfHeroesAttribute.Intelligence, Guid.Parse("F6E11C7E-C4F6-414E-8E92-B8C2C06A4F93") },
        { LandOfHeroesAttribute.Intuition, Guid.Parse("D8C6D5C2-6D13-4E92-9C8F-31A7C74EAF52") },
        { LandOfHeroesAttribute.Strength, Guid.Parse("80D7685B-6E9A-44C2-AC99-BA3173B2C41E") },
        { LandOfHeroesAttribute.Vigor, Guid.Parse("1F5B3E5E-C5B8-44A3-A9B3-8B1A3F7444AB") },
    };
}