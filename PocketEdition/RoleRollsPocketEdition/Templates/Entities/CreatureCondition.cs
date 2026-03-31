using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities;

public class CreatureCondition : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CampaignTemplateId { get; set; }
    public CampaignTemplate CampaignTemplate { get; set; } = null!;
    public List<Bonus> Bonuses { get; set; } = [];

    public CreatureCondition()
    {
    }

    public CreatureCondition(CreatureConditionModel model)
    {
        Id = model.Id;
        Name = model.Name;
        Description = model.Description;
        Bonuses = model.Bonuses.Select(bonus => new Bonus(bonus)).ToList();
    }

    public void Update(CreatureConditionModel model)
    {
        Name = model.Name;
        Description = model.Description;
    }
}
