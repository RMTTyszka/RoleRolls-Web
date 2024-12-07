using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Attacks.Services;
using RoleRollsPocketEdition._Application.Creatures.Models;
using RoleRollsPocketEdition._Application.Scenes.Services;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Scenes.Models;

namespace RoleRollsPocketEdition._Application.Scenes.Controllers;

[Route("campaigns/{campaignId}/scenes{sceneId}/creatures")]
public class SceneCreaturesController : ControllerBase
{
    private readonly ILogger<SceneCreaturesController> _logger;
    private readonly IAttackService _attackService;
    private readonly IScenesService _scenesService;

    public SceneCreaturesController(ILogger<SceneCreaturesController> logger, IAttackService attackService, IScenesService scenesService)
    {
        _logger = logger;
        _attackService = attackService;
        _scenesService = scenesService;
    }
    [HttpGet("")]

    public async Task<List<CreatureModel>> GetCreatures([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromQuery] CreatureType creatureType)
    {
        return await _scenesService.GetCreatures(campaignId, sceneId, creatureType);
    }        
    [HttpPost("")]

    public async Task AddHeroes([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] List<SceneCreatureModel> creatureModels)
    {
        await _scenesService.AddHero(campaignId, sceneId, creatureModels);
    }       

    [HttpDelete("{creatureId}")]
    public async Task RemoveCreature([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId)
    {
        await _scenesService.RemoveCreature(campaignId, sceneId, creatureId);
    }    
    [HttpPost("{creatureId}/attacks")]

    public async Task Attack([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] AttackInput input)
    {
        await _attackService.Attack(campaignId, sceneId, creatureId, input);
    }     
}