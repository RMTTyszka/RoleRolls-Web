namespace RoleRollsPocketEdition.Creatures.Models;

public class CreatureTakeDamageResult
{
    public string Name { get; set; }
    public int Value { get; set; }
    public string Vitality { get; set; }
    public Guid ActorId { get; set; }
    public int ExcessDamage { get; set; }
}