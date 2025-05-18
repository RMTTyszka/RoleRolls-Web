using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.CombatManeuvers;

public class LandOfHeroesCombatManeuvers
{
    public List<PowerTemplate> Maneuvers = [OpenShot, FullAttack];

    private static PowerTemplate OpenShot => new()
    {
        Id = Guid.Parse("DA612BD0-15D3-442D-BCA0-CC94D8E8F1D6"),
        Campaign = null,
        CampaignId = default,
        Type = PowerType.Instant,
        PowerDurationType = PowerDurationType.Instant,
        Duration = null,
        ActionType = PowerActionType.Active,
        Name = "Open Shot",
        Description = "",
        CastFormula = null,
        CastDescription = null,
        UseAttributeId = null,
        TargetDefenseId = null,
        UsagesFormula = null,
        UsageType = null,
        Bonuses = new List<Bonus>
        {
            new Bonus
            {
                Id = Guid.Parse("FB184927-6A5C-471D-B93D-25EC3BCA5BE9"),
                Description = null,
                Value = 2,
                Active = false,
                Property = null,
                Name = "Open Shot",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Advantage,
            }
        },
    }; 
    
    private static PowerTemplate FullAttack => new()
    {
        Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
        Campaign = null,
        CampaignId = default,
        Type = PowerType.Instant,
        PowerDurationType = PowerDurationType.Instant,
        Duration = null,
        ActionType = PowerActionType.Active,
        Name = "Full Attack",
        Description = "",
        CastFormula = null,
        CastDescription = null,
        UseAttributeId = null,
        TargetDefenseId = null,
        UsagesFormula = null,
        UsageType = null,
        Bonuses = new List<Bonus>
        {
            new Bonus
            {
                Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
                Description = null,
                Value = 2,
                Active = false,
                Property = null,
                Name = "Full Attack",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Advantage
            }
        },
    };
    private static PowerTemplate PartialAttack => new()
    {
        Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
        Campaign = null,
        CampaignId = default,
        Type = PowerType.Instant,
        PowerDurationType = PowerDurationType.Instant,
        Duration = null,
        ActionType = PowerActionType.Active,
        Name = "Partial Attack",
        Description = "",
        CastFormula = null,
        CastDescription = null,
        UseAttributeId = null,
        TargetDefenseId = null,
        UsagesFormula = null,
        UsageType = null,
        Bonuses = new List<Bonus>
        {
            new Bonus
            {
                Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
                Description = null,
                Value = -1,
                Active = false,
                Property = null,
                Name = "Partial Attack",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Advantage
            }
        },
    };
}