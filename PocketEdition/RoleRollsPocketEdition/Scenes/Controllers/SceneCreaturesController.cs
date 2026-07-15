using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Attacks.Models;
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
    private readonly IBasicAttackService _basicAttackService;
    private readonly ISpecialAttackService _specialAttackService;
    private readonly IEvadeService _evadeService;
    private readonly IScenesService _scenesService;

    public SceneCreaturesController(
        ILogger<SceneCreaturesController> logger,
        IBasicAttackService basicAttackService,
        ISpecialAttackService specialAttackService,
        IEvadeService evadeService,
        IScenesService scenesService)
    {
        _logger = logger;
        _basicAttackService = basicAttackService;
        _specialAttackService = specialAttackService;
        _evadeService = evadeService;
        _scenesService = scenesService;
    }
    [HttpGet()]

    public async Task<List<CreatureModel>> GetCreatures([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromQuery] CreatureCategory creatureCategory)
    {
        return await _scenesService.GetCreatures(campaignId, sceneId, creatureCategory);
    }        
    [HttpPost()]

    public async Task AddHeroes([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] List<SceneCreatureModel> creatureModels)
    {
        if (creatureModels.Select(e => e.CreatureCategory).First() == CreatureCategory.Hero)
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

    [HttpPost("{creatureId}/basic-attacks")]
    public async Task<BasicAttackResponse> BasicAttack(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid creatureId,
        [FromBody] BasicAttackInput input)
    {
        return await _basicAttackService.Attack(campaignId, sceneId, creatureId, input);
    }

    [HttpPost("{creatureId}/special-attacks")]
    public async Task<SpecialAttackResponse> SpecialAttack(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid creatureId,
        [FromBody] SpecialAttackInput input)
    {
        return await _specialAttackService.Attack(campaignId, sceneId, creatureId, input);
    }

    [HttpPost("{defenderId}/evades")]
    public async Task<EvadeResponse> Evade(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid defenderId,
        [FromBody] EvadeInput input)
    {
        return await _evadeService.Evade(campaignId, sceneId, defenderId, input);
    }

    [HttpPost("{creatureId}/attacks")]
    public async Task<BasicAttackResponse> Attack(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid creatureId,
        [FromBody] BasicAttackInput input)
    {
        return await _basicAttackService.Attack(campaignId, sceneId, creatureId, input);
    }
}
