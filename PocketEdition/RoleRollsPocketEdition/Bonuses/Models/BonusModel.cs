using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Bonuses.Models;

public class BonusModel : IEntityDto
{
    public Guid Id { get; set; }
    public int Value { get; set; }
    public BonusApplication Application { get; set; }
    public Property Property { get; set; }
    public BonusOrigin Origin { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public TargetType Target { get; set; }

    public BonusModel() { }

    public BonusModel(Bonus bonus)
    {
        Id = bonus.Id;
        Value = bonus.Value;
        Application = bonus.Application;
        Property = bonus.Property;
        Origin = bonus.Origin;
        Name = bonus.Name;
        Description = bonus.Description;
    }
}