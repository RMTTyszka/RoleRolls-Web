using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Itens.Templates.Models;
using RoleRollsPocketEdition.Itens.Templates.Services;

namespace RoleRollsPocketEdition.Itens.Templates.Controllers;

[Route("armor-templates")]
public class ArmorsTemplateController : BaseItemTemplateController<ArmorTemplateModel, ArmorTemplate>
{
    public ArmorsTemplateController(IItemTemplateService itemTemplateService) : base(itemTemplateService)
    {
    }

    [HttpPost("")]
    public async Task InsertArmor([FromBody] ArmorTemplateModel item)
    {
        await _itemTemplateService.InsertArmor(item);
    }    
    [HttpPut("{id}")]
    public async Task UpdateItem([FromRoute] Guid id, [FromBody] ArmorTemplateModel item)
    {
        await _itemTemplateService.UpdateArmor(id, item);

    }   
    [HttpDelete("{id}")]
    public async Task DeleteItem([FromRoute] Guid id)
    {
        await _itemTemplateService.DeleteItem(id);
    }
}