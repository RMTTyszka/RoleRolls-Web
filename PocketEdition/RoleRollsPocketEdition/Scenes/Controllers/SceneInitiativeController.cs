using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Scenes.Models;
using RoleRollsPocketEdition.Scenes.Services;

namespace RoleRollsPocketEdition.Scenes.Controllers;

[Route("campaigns/{campaignId}/scenes/{sceneId}/initiative")]
public class SceneInitiativeController : ControllerBase
{
    private readonly ISceneInitiativeService _initiativeService;

    public SceneInitiativeController(ISceneInitiativeService initiativeService)
    {
        _initiativeService = initiativeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SceneInitiativeEntryModel>>> GetAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId)
    {
        var initiative = await _initiativeService.GetAsync(campaignId, sceneId);
        return initiative is null ? NotFound() : Ok(initiative);
    }

    [HttpPost("roll-all")]
    public async Task<ActionResult<List<SceneInitiativeEntryModel>>> RollAllAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId)
    {
        var initiative = await _initiativeService.RollAllAsync(campaignId, sceneId);
        return initiative is null ? NotFound() : Ok(initiative);
    }

    [HttpPost("{creatureId}/roll")]
    public async Task<ActionResult<SceneInitiativeEntryModel>> RollCreatureAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid creatureId)
    {
        var initiative = await _initiativeService.RollCreatureAsync(campaignId, sceneId, creatureId);
        return initiative is null ? NotFound() : Ok(initiative);
    }

    [HttpDelete("{creatureId}")]
    public async Task<IActionResult> RemoveCreatureAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid creatureId)
    {
        return await _initiativeService.RemoveCreatureAsync(campaignId, sceneId, creatureId)
            ? NoContent()
            : NotFound();
    }
}
