using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition.Core.Dtos;

namespace RoleRollsPocketEdition._Application.Itens.Dtos;

public class GetAllItensTemplateInput : PagedRequestInput
{
    public ItemType? ItemType { get; set; }
}