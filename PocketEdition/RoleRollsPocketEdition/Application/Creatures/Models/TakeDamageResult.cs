namespace RoleRollsPocketEdition.Application.Creatures.Models;

public class TakeDamageResult
{
    public Guid TargetId { get; set; }
    public int Damage { get; set; }
    public string Description { get; set; }
    
}