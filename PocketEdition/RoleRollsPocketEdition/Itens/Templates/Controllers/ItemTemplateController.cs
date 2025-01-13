using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Itens.Templates.Models;
using RoleRollsPocketEdition.Itens.Templates.Services;

namespace RoleRollsPocketEdition.Itens.Templates.Controllers;

[Route("item-templates")]
public class ItemTemplateController : BaseItemTemplateController<ItemTemplateModel, ItemTemplate>
{
    public ItemTemplateController(IItemTemplateService itemTemplateService) : base(itemTemplateService)
    {
    }
}