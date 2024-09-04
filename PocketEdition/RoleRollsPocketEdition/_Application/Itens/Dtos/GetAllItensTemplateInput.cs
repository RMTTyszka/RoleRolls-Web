using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Domain.Itens;

namespace RoleRollsPocketEdition._Application.Itens.Dtos;

public class GetAllItensTemplateInput : PagedRequestInput
{
    public ItemType? ItemType { get; set; }
}