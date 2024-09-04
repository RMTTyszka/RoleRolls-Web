using RoleRollsPocketEdition.Domain.Rolls.Entities;

namespace RoleRollsPocketEdition.Application.Creatures.Dtos;

public class SimulateCdInput
{
    public decimal ExpectedChance { get; set; }
    public Guid PropertyId { get; set; }
    public RollPropertyType PropertyType { get; set; }
}