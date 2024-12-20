using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Itens.Services;
using RoleRollsPocketEdition._Domain.Itens.Templates;
using RoleRollsPocketEdition._Domain.Itens.Templates.Models;

namespace RoleRollsPocketEdition._Application.Itens.Controllers;

[Route("consumable-templates")]
public class ConsumableTemplateController : BaseItemTemplateController<ConsumableTemplateModel, ConsumableTemplate>
{
    public ConsumableTemplateController(IItemTemplateService itemTemplateService) : base(itemTemplateService)
    {
    }

    [HttpPost("")]
    public async Task Insert([FromBody] ConsumableTemplateModel item)
    {
        await _itemTemplateService.InsertItem(item);
    }    
    [HttpPut("{id}")]
    public async Task Update([FromRoute] Guid id, [FromBody] ConsumableTemplateModel item)
    {
        await _itemTemplateService.UpdateItem(id, item);

    }   
    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await _itemTemplateService.DeleteItem(id);
    }
}