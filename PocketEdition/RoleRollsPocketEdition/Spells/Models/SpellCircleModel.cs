using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Spells.Entities;

namespace RoleRollsPocketEdition.Spells.Models;

public class SpellCircleModel : IEntityDto
{
    public Guid Id { get; set; }
    public int Circle { get; set; }
    public string Title { get; set; } = string.Empty;
    public string InGameDescription { get; set; } = string.Empty;
    public string EffectDescription { get; set; } = string.Empty;
    public string CastingTime { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string AreaOfEffect { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public int LevelRequirement { get; set; }

    public SpellCircleModel()
    {
    }

    public SpellCircleModel(SpellCircle circle)
    {
        Id = circle.Id;
        Circle = circle.Circle;
        Title = circle.Title;
        InGameDescription = circle.InGameDescription;
        EffectDescription = circle.EffectDescription;
        CastingTime = circle.CastingTime;
        Duration = circle.Duration;
        AreaOfEffect = circle.AreaOfEffect;
        Requirements = circle.Requirements;
        LevelRequirement = circle.LevelRequirement;
    }
}


