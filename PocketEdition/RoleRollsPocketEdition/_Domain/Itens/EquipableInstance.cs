namespace RoleRollsPocketEdition.Domain.Itens;

public class EquipableInstance : ItemInstance
{
    public Guid TemplateId { get; set; }
    public EquipableTemplate Template { get; set; }
}