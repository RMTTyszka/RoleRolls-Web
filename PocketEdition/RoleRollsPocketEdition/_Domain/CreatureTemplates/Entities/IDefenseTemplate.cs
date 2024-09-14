namespace RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;

public interface IDefenseTemplate
{
    string Name { get; set; }
    string Formula { get; set; }
    Guid Id { get; set; }
}