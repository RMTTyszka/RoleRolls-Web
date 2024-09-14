using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;

public class DefenseTemplate : Entity, IDefenseTemplate
{
    public DefenseTemplate()
    {
        // EF CORE
    }
    public DefenseTemplate(DefenseTemplateModel model)
    {
        Id = model.Id;
        Name = model.Name;
        Formula = model.Formula;
    }

    public string Name { get; set; }
    public string Formula { get; set; }

    public void Update(DefenseTemplateModel defenseModel)
    {
        Name = defenseModel.Name;
        Formula = defenseModel.Formula;
    }
}