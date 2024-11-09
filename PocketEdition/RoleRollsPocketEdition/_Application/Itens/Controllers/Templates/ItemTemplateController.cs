using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Application.Itens.Services;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;
using RoleRollsPocketEdition.Core.Dtos;

namespace RoleRollsPocketEdition._Application.Itens.Controllers;

[Route("item-templates")]
public class ItemTemplateController : BaseItemTemplateController<ItemTemplateModel, ItemTemplate>
{
    public ItemTemplateController(IItemTemplateService itemTemplateService) : base(itemTemplateService)
    {
    }
}