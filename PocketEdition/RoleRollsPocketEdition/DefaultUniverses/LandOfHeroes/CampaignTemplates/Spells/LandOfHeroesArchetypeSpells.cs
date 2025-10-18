using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;
using RoleRollsPocketEdition.Spells.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Spells;

public static class LandOfHeroesArchetypeSpells
{
    // Registry: ArchetypeId -> Spells
    public static readonly Dictionary<Guid, List<Spell>> ArchetypeSpells = new()
    {
        { WarlockArchetypeDetails.WarlockArchetype.Id, LandOfHeroesWarlockSpells.Build() },
        { WarriorArchetypeDetails.WarriorArchetype.Id, new List<Spell>() },
        { BarbarianArchetypeDetails.BarbarianArchetype.Id, new List<Spell>() },
        { BardArchetypeDetails.BardArchetype.Id, new List<Spell>() },
        { CrusaderArchetypeDetails.CrusaderArchetype.Id, new List<Spell>() },
        { DruidArchetypeDetails.DruidArchetype.Id, new List<Spell>() },
        { HunterArchetypeDetails.HunterArchetype.Id, new List<Spell>() },
        { MartialistArchetypeDetails.MartialistArchetype.Id, new List<Spell>() },
        { RogueArchetypeDetails.RogueArchetype.Id, new List<Spell>() },
        { SpellcasterArchetypeDetails.SpellcasterArchetype.Id, new List<Spell>() },
        { SpiritualistArchetypeDetails.SpiritualistArchetype.Id, new List<Spell>() },
    };

    public static List<Spell> GetForArchetype(Guid archetypeId)
    {
        return ArchetypeSpells.TryGetValue(archetypeId, out var spells)
            ? spells
            : new List<Spell>();
    }
}
