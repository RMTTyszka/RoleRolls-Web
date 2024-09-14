using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition._Domain.Campaigns.Events.Defenses;

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