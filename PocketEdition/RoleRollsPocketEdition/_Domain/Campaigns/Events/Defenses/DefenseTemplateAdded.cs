using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Domain.Campaigns.Events.Defenses;

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