using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition._Domain.Campaigns.Events.Defenses;

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