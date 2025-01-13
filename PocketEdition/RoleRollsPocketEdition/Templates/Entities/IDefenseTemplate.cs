namespace RoleRollsPocketEdition.Templates.Entities;

public interface IDefenseTemplate
{
    string Name { get; set; }
    string Formula { get; set; }
    Guid Id { get; set; }
}