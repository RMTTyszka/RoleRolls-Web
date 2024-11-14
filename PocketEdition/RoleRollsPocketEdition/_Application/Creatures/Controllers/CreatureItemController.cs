using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Creatures.Services;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Application.Itens.Services;

namespace RoleRollsPocketEdition._Application.Creatures.Controllers;

[Route("campaigns/{campaingId}/creatures/{creatureId}/itens")]
public class CreatureItemController : ControllerBase
{
    private readonly ICreatureItemService _itemService;

    public CreatureItemController(ICreatureItemService itemService)
    {
        _itemService = itemService;
    }


    [HttpPost("")]
    public async Task<ItemModel?> Instantiate([FromRoute] Guid campaignId, [FromRoute] Guid creatureId, [FromBody] ItemInstantiateInput item)
    {
        return await _itemService.Instantiate(campaignId, creatureId, item);
    }    
    [HttpPut("{id}")]
    public async Task Update([FromRoute] Guid campaignId, [FromRoute] Guid creatureId, [FromRoute] Guid id, [FromBody] ItemInstanceUpdate item)
    {
        await _itemService.Update(campaignId, id, creatureId, item);

    }   
    [HttpDelete("{id}")]
    public async Task Destroy([FromRoute] Guid campaignId, [FromRoute] Guid creatureId, [FromRoute] Guid id)
    {
        await _itemService.Destroy(campaignId, creatureId, id);
    }
}