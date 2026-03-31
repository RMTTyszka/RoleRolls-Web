namespace RoleRollsPocketEdition.Creatures.Models;

public class VitalityStatusChange
{
    public Guid? ConditionId { get; set; }
    public string Vitality { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ThresholdPercent { get; set; }
}
