namespace RoleRollsPocketEdition.Creatures.Models;

public class VitalityConditionModel
{
    public Guid? ConditionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ThresholdPercent { get; set; }
}
