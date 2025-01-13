using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Scenes.Models;
using RoleRollsPocketEdition.Scenes.Services;

namespace RoleRollsPocketEdition.Scenes.Controllers;

[Route("campaigns/{campaignId}/scenes/{sceneId}/creatures")]
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
    [HttpGet()]

    public async Task<List<CreatureModel>> GetCreatures([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromQuery] CreatureType creatureType)
    {
        return await _scenesService.GetCreatures(campaignId, sceneId, creatureType);
    }        
    [HttpPost()]

    public async Task AddHeroes([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] List<SceneCreatureModel> creatureModels)
    {
        if (creatureModels.Select(e => e.CreatureType).First() == CreatureType.Hero)
        {
            await _scenesService.AddHero(campaignId, sceneId, creatureModels);
        }
        else
        {
            await _scenesService.AddMonster(campaignId, sceneId, creatureModels);
        }

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