namespace RoleRollsPocketEdition.Creatures.Dtos;

public class UpdateLifeInput
{
    public Guid LifeId { get; set; }
    public int Value { get; set; }
    // TODO damage type
    // TODO description
}

public class LifeDamageInput
{
    public Guid LifeId { get; set; }
    public int Damage { get; set; }
}