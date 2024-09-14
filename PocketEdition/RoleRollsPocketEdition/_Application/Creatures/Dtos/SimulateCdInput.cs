using RoleRollsPocketEdition._Domain.Rolls.Entities;

namespace RoleRollsPocketEdition._Application.Creatures.Dtos;

public class SimulateCdInput
{
    public decimal ExpectedChance { get; set; }
    public Guid PropertyId { get; set; }
    public RollPropertyType PropertyType { get; set; }
}