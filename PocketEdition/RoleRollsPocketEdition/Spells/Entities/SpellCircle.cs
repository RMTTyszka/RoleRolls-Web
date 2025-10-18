using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Spells.Entities;

public class SpellCircle : Entity
{
    public Guid SpellId { get; set; }
    public Spell Spell { get; set; }

    public int Circle { get; set; }
    public string Title { get; set; } = string.Empty;

    public string InGameDescription { get; set; } = string.Empty;
    public string EffectDescription { get; set; } = string.Empty;
    public string CastingTime { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string AreaOfEffect { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;

    public int LevelRequirement { get; set; }
}
