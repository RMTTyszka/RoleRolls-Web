using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Damages.Entities;

public class DamageType : Entity
{
    public string Name { get; set; }
    public Guid CampaignTemplateId { get; set; }
    public CampaignTemplate CampaignTemplate { get; set; }
}

public enum DamageCategory
{
    Physical = 0,
    Energy = 1, // Magical
    Psychological = 2,
}