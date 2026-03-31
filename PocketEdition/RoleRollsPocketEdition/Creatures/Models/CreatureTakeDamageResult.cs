namespace RoleRollsPocketEdition.Creatures.Models;

public class CreatureTakeDamageResult
{
    public string Name { get; set; }
    public int Value { get; set; }
    public string Vitality { get; set; }
    public Guid ActorId { get; set; }
    public int ExcessDamage { get; set; }
    public int PreviousValue { get; set; }
    public int CurrentValue { get; set; }
    public int MaxValue { get; set; }
    public List<VitalityStatusChange> TriggeredStatuses { get; set; } = [];
}


