using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Creatures.Dtos;

public class SimulateCdInput
{
    public decimal ExpectedChance { get; set; }
    public Property Property { get; set; }
}