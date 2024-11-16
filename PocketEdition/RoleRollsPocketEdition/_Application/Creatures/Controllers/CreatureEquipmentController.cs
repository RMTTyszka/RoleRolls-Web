using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Creatures.Dtos;
using RoleRollsPocketEdition._Application.Creatures.Services;
using RoleRollsPocketEdition._Application.Itens.Dtos;
using RoleRollsPocketEdition._Application.Itens.Services;
using RoleRollsPocketEdition._Domain.Itens;

namespace RoleRollsPocketEdition._Application.Creatures.Controllers;

[Route("campaigns/{campaingId}/creatures/{creatureId}/equipments")]
public class CreatureEquipmentController : ControllerBase
{
    private readonly ICreatureEquipmentService _itemService;

    public CreatureEquipmentController(ICreatureEquipmentService itemService)
    {
        _itemService = itemService;
    }


    [HttpPost("")]
    public async Task Equip([FromRoute] Guid campaignId, [FromRoute] Guid creatureId, [FromBody] EquipItemInput input)
    {
        await _itemService.Equip(campaignId, creatureId, input);
    }    
    [HttpDelete("slots/{slot}")]
    public async Task Unequip([FromRoute] Guid campaignId, [FromRoute] Guid creatureId, [FromRoute] EquipableSlot slot)
    {
        await _itemService.Unequip(campaignId, creatureId, slot);
    }   

}