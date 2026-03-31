using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos;

public class CreatureConditionModel : IEntityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<BonusModel> Bonuses { get; set; } = [];

    public CreatureConditionModel()
    {
    }

    public CreatureConditionModel(CreatureCondition condition)
    {
        Id = condition.Id;
        Name = condition.Name;
        Description = condition.Description;
        Bonuses = condition.Bonuses.Select(bonus => new BonusModel(bonus)).ToList();
    }
}
