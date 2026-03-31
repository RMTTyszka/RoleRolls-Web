namespace RoleRollsPocketEdition.Creatures.Models;

public class VitalityStatusChange
{
    public string Vitality { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int ThresholdPercent { get; set; }
}
