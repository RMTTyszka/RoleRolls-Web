namespace RoleRollsPocketEdition._Domain.Creatures.Models;

public class CreatureTakeDamageResult
{
    public string Name { get; set; }
    public int Value { get; set; }
    public string Life { get; set; }
    public Guid ActorId { get; set; }
}