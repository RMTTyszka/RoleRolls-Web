namespace RoleRollsPocketEdition.Creatures.Dtos;

public class UpdateVitalityInput
{
    public Guid VitalityId { get; set; }
    public int Value { get; set; }
    // TODO damage type
    public string? Description { get; set; }
}

public class VitalityDamageInput
{
    public Guid VitalityId { get; set; }
    public int Damage { get; set; }
}


