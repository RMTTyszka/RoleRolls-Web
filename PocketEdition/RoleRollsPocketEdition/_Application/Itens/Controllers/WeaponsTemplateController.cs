using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Domain.Itens;
using RoleRollsPocketEdition.Domain.Itens.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.Itens.Services;

[Route("weapon-templates")]
public class WeaponTemplateController : ControllerBase
{
    private readonly IItemTemplateService _itemTemplateService;

    public WeaponTemplateController(IItemTemplateService itemTemplateService)
    {
        _itemTemplateService = itemTemplateService;
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