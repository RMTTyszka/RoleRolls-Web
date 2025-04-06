using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Powers.Models; 
public class PowerTemplateModel : IEntityDto
{
    public PowerType Type { get; set; }
    public PowerDurationType PowerDurationType { get; set; }
    public int? Duration { get; set; }
    public PowerActionType  ActionType { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CastFormula { get; set; }
    public string CastDescription { get; set; }
    public Guid? UseAttributeId { get; set; }
    public Guid? TargetDefenseId { get; set; }
    public string UsagesFormula { get; set; }
    public UsageType? UsageType { get; set; }
    public Guid Id { get; set; }

    public PowerTemplateModel()
    {
    }

    public PowerTemplateModel(PowerTemplate power)
    {
        Type = power.Type;
        PowerDurationType = power.    PowerDurationType;
        Duration = power.Duration;
        ActionType = power.    ActionType;
        Name = power.Name;
        Description = power.    Description;
        CastFormula = power.CastFormula;
        CastDescription = power.    CastDescription;
        UseAttributeId = power.UseAttributeId;
        TargetDefenseId = power.    TargetDefenseId;
        UsagesFormula = power.UsagesFormula;
        UsageType = power.    UsageType;
        Id = power.Id;
    }
}