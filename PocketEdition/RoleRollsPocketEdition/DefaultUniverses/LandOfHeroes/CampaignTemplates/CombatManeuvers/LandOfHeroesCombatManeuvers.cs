using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.CombatManeuvers;

public class LandOfHeroesCombatManeuvers
{
    public List<PowerTemplate> Maneuvers = new List<PowerTemplate>
    {
        new PowerTemplate
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
                    Application = BonusApplication.Advantage,
                    Origin = BonusOrigin.Innate
                }
            },
            Instances = null,
            ItemTemplates = null
        }
    };
}