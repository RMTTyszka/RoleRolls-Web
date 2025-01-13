using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Events.Defenses;

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