using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Itens.Templates;

namespace RoleRollsPocketEdition.Itens.Dtos;

public class GetAllItensInput : PagedRequestInput
{
    public ItemType? ItemType { get; set; }
}


