using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Encounters.Models;
using RoleRollsPocketEdition.Encounters.Services;

[ApiController]
[Route("api/campaigns/{campaignId}/encounters")]
public class EncounterController : ControllerBase
{
    private readonly IEncounterService _encounterService;

    public EncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid campaignId, [FromQuery] PagedRequestInput input)
    {
        var encounters = await _encounterService.GetAllAsync(campaignId, input);
        return Ok(encounters);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid campaignId, Guid id)
    {
        var encounter = await _encounterService.GetAsync(campaignId, id);
        
        if (encounter == null)
            return NotFound();
            
        return Ok(encounter);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid campaignId, [FromBody] EnconterModel encounter)
    {
        await _encounterService.CreateAsync(campaignId, encounter);
        return CreatedAtAction(nameof(Get), new { campaignId, id = encounter.Id }, encounter);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid campaignId, Guid id, [FromBody] EnconterModel encounter)
    {
        if (id != encounter.Id)
            return BadRequest("ID no caminho não corresponde ao ID do modelo");
            
        await _encounterService.UpdateAsync(campaignId, encounter);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _encounterService.DeleteAsync(id);

        if (!result.Success)
            return UnprocessableEntity();
        return NoContent();
    }

    [HttpPost("{encounterId}/creatures")]
    public async Task<IActionResult> AddCreature(Guid campaignId, Guid encounterId, [FromBody] CreatureModel creature)
    {
        var result = await _encounterService.AddCreatureAsync(campaignId, encounterId, creature);
        
        if (!result.Success)
            return UnprocessableEntity();
            
        return CreatedAtAction(nameof(Get), new { campaignId, id = encounterId }, null);
    }

    [HttpDelete("{encounterId}/creatures/{creatureId}")]
    public async Task<IActionResult> RemoveCreature(Guid encounterId, Guid creatureId, [FromQuery] bool delete = false)
    {
        var result = await _encounterService.RemoveCreatureAsync(encounterId, creatureId, delete);
        
        if (!result.Success)
            return UnprocessableEntity();
            
        return NoContent();
    }
}