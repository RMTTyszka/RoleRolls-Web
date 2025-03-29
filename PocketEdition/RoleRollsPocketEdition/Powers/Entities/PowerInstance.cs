using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Powers.Entities;

public class PowerInstance : Entity
{
    public PowerTemplate Template { get; set; }
    public Guid TemplateId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UsedCharges { get; set; }
    public ICollection<ItemInstance> ItemInstances { get; set; } = new List<ItemInstance>();
}