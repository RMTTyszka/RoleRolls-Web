using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos;

public class DefenseTemplateModel : IDefenseTemplate
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Formula { get; set; }

    public DefenseTemplateModel()
    {
        
    }

    public DefenseTemplateModel(DefenseTemplate defense)
    {
        Id = defense.Id;
        Name = defense.Name;
        Formula = defense.Formula;
    }
}