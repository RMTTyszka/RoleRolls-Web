using System.ComponentModel.DataAnnotations;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Powers.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Itens;

public class ItemInstance : Entity
{
    public string Name { get; set; }
    public Guid? PowerId { get; set; }
    public PowerTemplate? Power { get; set; }
    public int Level { get; set; }
    public int GetBonus => Level / 2;
    /*public Guid? InventoryId { get; set; }
    public Inventory? Inventory { get; set; }*/
    public Guid TemplateId { get; set; }

    public ItemTemplate Template { get; set; }
    public ArmorTemplate? ArmorTemplate => Template as ArmorTemplate;

    public void Update(ItemInstanceUpdate input)
    {
        throw new NotImplementedException();
    }
}