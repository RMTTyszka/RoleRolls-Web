using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Itens.Templates.Models;
using RoleRollsPocketEdition.Itens.Templates.Services;

namespace RoleRollsPocketEdition.Itens.Templates.Controllers;

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