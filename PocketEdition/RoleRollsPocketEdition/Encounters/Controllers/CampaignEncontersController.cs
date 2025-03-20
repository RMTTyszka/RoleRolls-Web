using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Encounters.Entities;
using RoleRollsPocketEdition.Encounters.Services;

namespace RoleRollsPocketEdition.Encounters.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CampaignEncontersController : ControllerBase
{
    private readonly IEncounterService _encounterService;

    public CampaignEncontersController(IEncounterService encounterService)
    {
        _encounterService = encounterService ?? throw new ArgumentNullException(nameof(encounterService));
    }

    // GET: api/Encounter
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enconter>>> GetEncounters()
    {
        var encounters = await _encounterService.GetAllAsync();
        return Ok(encounters);
    }

    // GET: api/Encounter/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Enconter>> GetEncounter(Guid id)
    {
        var encounter = await _encounterService.GetByIdAsync(id);

        if (encounter == null)
        {
            return NotFound();
        }

        return Ok(encounter);
    }

    // GET: api/Encounter/Campaign/5
    [HttpGet("Campaign/{campaignId}")]
    public async Task<ActionResult<IEnumerable<Enconter>>> GetEncountersByCampaign(Guid campaignId)
    {
        var encounters = await _encounterService.GetByCampaignIdAsync(campaignId);
        return Ok(encounters);
    }

    // POST: api/Encounter
    [HttpPost]
    public async Task<ActionResult<Enconter>> CreateEncounter(Enconter encounter)
    {
        try
        {
            var createdEncounter = await _encounterService.CreateAsync(encounter);
            return CreatedAtAction(nameof(GetEncounter), new { id = createdEncounter.Id }, createdEncounter);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Encounter/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEncounter(Guid id, Enconter encounter)
    {
        if (id != encounter.Id)
        {
            return BadRequest("ID mismatch between URL and body");
        }

        try
        {
            var updatedEncounter = await _encounterService.UpdateAsync(encounter);
            return Ok(updatedEncounter);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Encounter/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEncounter(Guid id)
    {
        var result = await _encounterService.DeleteAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Encounter/5/Creatures
    [HttpPost("{id}/Creatures")]
    public async Task<IActionResult> AddCreature(Guid id, Creature creature)
    {
        try
        {
            var result = await _encounterService.AddCreatureAsync(id, creature);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Encounter/5/Creatures
    [HttpDelete("{id}/Creatures")]
    public async Task<IActionResult> RemoveCreature(Guid id, Creature creature)
    {
        try
        {
            var result = await _encounterService.RemoveCreatureAsync(id, creature);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Encounter/5/Creatures/7
    [HttpDelete("{id}/Creatures/{creatureId}")]
    public async Task<IActionResult> RemoveCreatureById(Guid id, Guid creatureId)
    {
        var result = await _encounterService.RemoveCreatureByIdAsync(id, creatureId);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}