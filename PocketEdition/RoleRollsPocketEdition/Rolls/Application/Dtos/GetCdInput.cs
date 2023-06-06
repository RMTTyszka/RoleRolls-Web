using RoleRollsPocketEdition.Rolls.Domain.Entities;

namespace RoleRollsPocketEdition.Rolls.Application.Dtos;

public class GetCdInput
{
    public Guid PropertyId { get; set; }
    public RollPropertyType PropertyType  { get; set; }
    public decimal TargetChance { get; set; }
}