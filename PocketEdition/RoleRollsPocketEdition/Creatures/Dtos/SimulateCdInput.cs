using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Creatures.Dtos;

public class SimulateCdInput
{
    public decimal ExpectedChance { get; set; }
    public Guid PropertyId { get; set; }
    public RollPropertyType PropertyType { get; set; }
}