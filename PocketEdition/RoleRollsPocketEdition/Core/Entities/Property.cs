namespace RoleRollsPocketEdition.Core.Entities;

public record Property(Guid Id, PropertyType Type);

public enum PropertyType
{
    All = 0,
    Attribute = 1,
    Skill = 2,
    MinorSkill = 3,
    Defense = 4,
    Vitality = 5,
}