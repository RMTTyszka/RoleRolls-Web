using RoleRollsPocketEdition.Rolls.Domain.Entities;

namespace RoleRollsPocketEdition.Creatures.Application.Dtos;

public class SimulateCdInput
{
    public decimal ExpectedChance { get; set; }
    public Guid PropertyId { get; set; }
    public RollPropertyType PropertyType { get; set; }
}