using System.ComponentModel.DataAnnotations;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Domain.Powers.Entities;

namespace RoleRollsPocketEdition.Domain.Itens;

public class ItemInstance : Entity
{
    [Key]
    public new Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? PowerId { get; set; }
    public PowerTemplate? Power { get; set; }
    public int Level { get; set; }
    
    public Guid TemplateId { get; set; }
    public ItemTemplate Template { get; set; }
}