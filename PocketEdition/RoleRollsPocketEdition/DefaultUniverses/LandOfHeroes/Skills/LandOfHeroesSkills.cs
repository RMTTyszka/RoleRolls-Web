namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;

public class LandOfHeroesSkills
{
    public static Dictionary<LandOfHeroesSkill, Guid> SkillIds = new()
    {
        { LandOfHeroesSkill.Awareness, Guid.Parse("A8659BFC-33EA-4FE4-9E5F-213D6C8B583F") },
        { LandOfHeroesSkill.Empathy, Guid.Parse("63D26738-AC72-4C13-9D9C-9732AE97C172") },
        { LandOfHeroesSkill.Knowledge, Guid.Parse("F48792C4-B5A6-4BA1-B0AB-FB8F4E35C867") },
        { LandOfHeroesSkill.Nimbleness, Guid.Parse("7689F6A5-B8D7-4D92-99FA-52248B5C9A39") },
        { LandOfHeroesSkill.Survival, Guid.Parse("F6A7A4A2-B5E4-408E-A9A3-8FDD6C6C598A") },
        { LandOfHeroesSkill.Treatment, Guid.Parse("7F8C3E7B-36C4-432E-832F-7152AECD9A8C") },
        { LandOfHeroesSkill.Athletics, Guid.Parse("BCDEF8A1-7C6A-47A2-9D7A-1E6C8D1E3F22") },
        { LandOfHeroesSkill.Combat, Guid.Parse("D4CD2313-7AC5-4FF1-9F23-F534FA1CFBD6") },
        { LandOfHeroesSkill.Resistance, Guid.Parse("C5614352-312C-4318-84D1-4253C97C3D40") },
    };
}