using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Domain.Events.Defenses;

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