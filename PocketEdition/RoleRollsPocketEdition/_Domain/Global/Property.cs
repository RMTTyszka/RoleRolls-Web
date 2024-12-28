namespace RoleRollsPocketEdition._Domain.Global;

public record Property(Guid IdProperty, PropertyType Type);

public enum PropertyType
{
    Attribute = 0,
    Skill = 1,
    MinorSkill = 2,
    Power = 3
}