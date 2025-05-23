using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Itens.Templates.Models;
using RoleRollsPocketEdition.Itens.Templates.Services;

namespace RoleRollsPocketEdition.Itens.Templates.Controllers;

[Route("weapon-templates")]
public class WeaponTemplateController : BaseItemTemplateController<WeaponTemplateModel, WeaponTemplate>
{
    public WeaponTemplateController(IItemTemplateService itemTemplateService) : base(itemTemplateService)
    {
    }

    [HttpPost("")]
    public async Task InsertWeapon([FromBody] WeaponTemplateModel item)
    {
        await _itemTemplateService.InsertWeapon(item);
    }    
    [HttpPut("{id}")]
    public async Task UpdateItem([FromRoute] Guid id, [FromBody] WeaponTemplateModel item)
    {
        await _itemTemplateService.UpdateWeapon(id, item);

    }   
    [HttpDelete("{id}")]
    public async Task DeleteItem([FromRoute] Guid id)
    {
        await _itemTemplateService.DeleteItem(id);
    }
}