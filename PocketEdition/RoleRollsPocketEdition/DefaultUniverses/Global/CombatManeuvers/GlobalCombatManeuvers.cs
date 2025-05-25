using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.Global.CombatManeuvers;

public class GlobalCombatManeuvers
{
    public static PowerTemplate OpenShot => new()
    {
        Id = Guid.Parse("DA612BD0-15D3-442D-BCA0-CC94D8E8F1D6"),
        Campaign = null,
        CampaignId = default,
        Type = PowerType.Instant,
        PowerDurationType = PowerDurationType.Instant,
        Duration = null,
        ActionType = PowerActionType.AttackAction,
        Name = "Open Shot",
        Description = "",
        CastFormula = null,
        CastDescription = null,
        UseAttributeId = null,
        TargetDefenseId = null,
        UsagesFormula = null,
        UsageType = null,
        TargetType = TargetType.Self,
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
    
    public static PowerTemplate FullAttack => new()
    {
        Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
        Campaign = null,
        CampaignId = default,
        Type = PowerType.Instant,
        PowerDurationType = PowerDurationType.Instant,
        Duration = null,
        ActionType = PowerActionType.AttackAction,
        Name = "Full Attack",
        Description = "",
        CastFormula = null,
        CastDescription = null,
        UseAttributeId = null,
        TargetDefenseId = null,
        UsagesFormula = null,
        UsageType = null,
        TargetType = TargetType.Self,
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
                Description = null,
                Value = 1,
                Active = false,
                Property = null,
                Name = "Full Attack Hit Advantage",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Advantage
            },      
            new Bonus
            {
                Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
                Description = null,
                Value = 1,
                Active = false,
                Property = null,
                Name = "Full Attack Evasion Disadvantage",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Disadvantage
            },        
            new Bonus
            {
                Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
                Description = null,
                Value = 1,
                Active = false,
                Property = null,
                Name = "Full Attack Evasion Debuff",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Debuff
            },
        ],
    };
    public static PowerTemplate PartialAttack => new()
    {
        Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
        Campaign = null,
        CampaignId = default,
        Type = PowerType.Instant,
        PowerDurationType = PowerDurationType.Instant,
        Duration = null,
        ActionType = PowerActionType.AttackAction,
        Name = "Partial Attack",
        Description = "",
        CastFormula = null,
        CastDescription = null,
        UseAttributeId = null,
        TargetDefenseId = null,
        UsagesFormula = null,
        UsageType = null,
        TargetType = TargetType.Self,
        Bonuses =
        [
            new()
            {
                Id = Guid.Parse("CB9D26A7-165B-4838-82D6-D8E10830C5D6"),
                Description = null,
                Value = 1,
                Active = false,
                Property = null,
                Name = "Partial Attack",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Disadvantage
            }
        ],
    };
    public static PowerTemplate CautiousAttack => new()
    {
        Id = Guid.Parse("1F25C2A5-32CE-4D93-9BA0-980282B4EE98"),
        Campaign = null,
        CampaignId = default,
        Type = PowerType.Instant,
        PowerDurationType = PowerDurationType.Instant,
        Duration = null,
        ActionType = PowerActionType.AttackAction,
        Name = "Cautious Attack",
        Description = "",
        CastFormula = null,
        CastDescription = null,
        UseAttributeId = null,
        TargetDefenseId = null,
        UsagesFormula = null,
        UsageType = null,
        TargetType = TargetType.Self,
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("1F25C2A5-32CE-4D93-9BA0-980282B4EE98"),
                Description = null,
                Value = 1,
                Active = false,
                Property = null,
                Name = "Cautious Attack Disadvantage",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Disadvantage
            },     
            new Bonus
            {
                Id = Guid.Parse("7136A7D9-079C-4577-B0F1-4E7F949BBD43"),
                Description = null,
                Value = 1,
                Active = false,
                Property = null,
                Name = "Cautious Evasion Advantage",
                Application = BonusApplication.Evasion,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Advantage
            },
        ],
    };
    public static PowerTemplate FullDefence => new()
    {
        Id = Guid.Parse("B8685A16-BDE3-4407-92E7-2C7917195B82"),
        Campaign = null,
        CampaignId = default,
        Type = PowerType.Buff,
        PowerDurationType = PowerDurationType.Turns,
        Duration = 1,
        ActionType = PowerActionType.FullAction,
        Name = "Full Defense",
        Description = "",
        CastFormula = null,
        CastDescription = null,
        UseAttributeId = null,
        TargetDefenseId = null,
        UsagesFormula = null,
        UsageType = null,
        TargetType = TargetType.Self,
        Bonuses =
        [
            new Bonus
            {
                Id = Guid.Parse("B8685A16-BDE3-4407-92E7-2C7917195B82"),
                Description = null,
                Value = 3,
                Active = false,
                Property = null,
                Name = "Full Defence Hit Disadvantage",
                Application = BonusApplication.Hit,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Disadvantage
            },     
            new Bonus
            {
                Id = Guid.Parse("7136A7D9-079C-4577-B0F1-4E7F949BBD43"),
                Description = null,
                Value = 2,
                Active = false,
                Property = null,
                Name = "Full Defense Evasion Advantage",
                Application = BonusApplication.Evasion,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Advantage
            },    
            new Bonus
            {
                Id = Guid.Parse("B00BEA51-03A4-46F3-8B72-D2F30BD00CFD"),
                Description = null,
                Value = 2,
                Active = false,
                Property = null,
                Name = "Full Defense Evasion Value",
                Application = BonusApplication.Evasion,
                Origin = BonusOrigin.Innate,
                Type = BonusType.Buff
            },
        ],
    };
}