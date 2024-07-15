using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.CreaturesTemplates.Domain.Entities;

public interface IDefenseTemplate
{
    string Name { get; set; }
    string Formula { get; set; }
    Guid Id { get; set; }
}