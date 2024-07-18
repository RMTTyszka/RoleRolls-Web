using RoleRollsPocketEdition.Global;
using RoleRollsPocketEdition.Powers.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities;

public class CreaturePower : Entity
{
    public Creature Creature { get; set; }
    public Guid CreatureId { get; set; }
    public PowerTemplate PowerTemplate { get; set; }
    public Guid PowerTemplateId { get; set; }
    public int? ConsumedUsages { get; set; }
}