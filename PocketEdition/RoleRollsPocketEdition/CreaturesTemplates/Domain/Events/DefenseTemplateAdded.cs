using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.CreaturesTemplates.Domain.Events;

public class DefenseTemplateAdded
{
    public DefenseTemplateModel DefenseTemplateModel { get; set; }

    public static DefenseTemplateAdded FromDefenseTemplate(DefenseTemplateModel defense)
    {
        return new DefenseTemplateAdded
        {
            DefenseTemplateModel = defense
        };
    }
}