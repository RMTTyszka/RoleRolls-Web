namespace RoleRollsPocketEdition.Itens.Dtos;

public class ItemInstanceUpdate
{
    public string? Name { get; set; }
    public int Level { get; set; }
}
public class ItemInstantiateInput : ItemInstanceUpdate
{
    public Guid TemplateId { get; set; }
}


