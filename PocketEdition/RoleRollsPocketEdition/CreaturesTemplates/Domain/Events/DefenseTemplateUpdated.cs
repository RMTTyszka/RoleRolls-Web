using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.CreaturesTemplates.Domain.Events;

public class DefenseTemplateUpdated
{
    public DefenseTemplateModel DefenseTemplateModel { get; set; }

    public static DefenseTemplateUpdated FromDefenseTemplate(DefenseTemplateModel defenseModel)
    {
        return new DefenseTemplateUpdated
        {
            DefenseTemplateModel = defenseModel
        };
    }
}