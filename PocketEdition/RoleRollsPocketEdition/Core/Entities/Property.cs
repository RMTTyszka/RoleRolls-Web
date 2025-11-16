using RoleRollsPocketEdition.Creatures.Models;

namespace RoleRollsPocketEdition.Core.Entities;

public record Property(Guid Id, PropertyType? Type = null);

public enum PropertyType
{
    All = 0,
    Attribute = 1,
    Skill = 2,
    MinorSkill = 3,
    Defense = 4,
    Vitality = 5,
}
public enum PropertyValueOrigin
{
    CreatureProperty = 0,
    TemplateProperty = 1
}
public class PropertyValue
{
    public int Value { get; set; }
    public int Bonus { get; set; }
}


