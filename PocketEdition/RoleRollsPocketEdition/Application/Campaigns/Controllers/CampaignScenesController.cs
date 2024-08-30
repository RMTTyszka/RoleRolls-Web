using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Application.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Authentication;
using RoleRollsPocketEdition.Campaigns.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Services;
using RoleRollsPocketEdition.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Global.Dtos;
using RoleRollsPocketEdition.Rolls.Application;

namespace RoleRollsPocketEdition.Campaigns.Application.Controllers
{
        [Authorize]
    [ApiController]
    [Route("campaigns/{campaignId}/scenes")]
    public class CampaignScenesController : ControllerBase
    {

        private readonly ICreatureService _creatureService;
        private readonly IRollService _rollService;
        private readonly ICampaignSceneHistoryBuilderService _campaignSceneHistoryBuilderService;
        private readonly ICreatureActionsService _creatureActionsService;

        public CampaignScenesController(IRollService rollService, ICreatureService creatureService, ICampaignSceneHistoryBuilderService campaignSceneHistoryBuilderService, ICreatureActionsService creatureActionsService)
        {
            _rollService = rollService;
            _creatureService = creatureService;
            _campaignSceneHistoryBuilderService = campaignSceneHistoryBuilderService;
            _creatureActionsService = creatureActionsService;
        }

        [HttpGet("{sceneId}/rolls")]
        public async Task<IActionResult> GetRolls([FromRoute] Guid sceneId, [FromRoute] Guid campaignId, [FromQuery] PagedRequestInput input)
        {
            var result = await _rollService.GetAsync(campaignId, sceneId, input);
            return Ok(result);
        }  
        [HttpGet("{sceneId}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid sceneId, [FromRoute] Guid campaignId)
        {
            var result = await _campaignSceneHistoryBuilderService.GetListV2(campaignId, sceneId);
            return Ok(result.Select(e => (object)e));
        }      
        [HttpGet("{sceneId}/rolls/{rollId}")]
        public async Task<IActionResult> GetRoll([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid rollId, [FromQuery] PagedRequestInput input)
        {
            var result = await _rollService.GetAsync(campaignId, sceneId, rollId);
            if (result is null) 
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("{sceneId}/rolls")]
        public async Task<IActionResult> RollDice([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] RollInput input)
        {
            var result = await _rollService.RollAsync(campaignId, sceneId, input);
            Response.Headers.AccessControlAllowHeaders = "Location";
            Response.Headers.AccessControlExposeHeaders = "Location";
            return CreatedAtAction(nameof(GetRoll), new { campaignId = campaignId, id = result.Id, sceneId = sceneId }, result);
        }    
        [HttpPost("{sceneId}/creatures/{creatureId}/rolls")]
        public async Task<IActionResult> RollDiceForCreature([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] RollInput input)
        {
            var result = await _rollService.RollAsync(campaignId, sceneId, creatureId, input);
            Response.Headers.AccessControlAllowHeaders = "Location";
            Response.Headers.AccessControlExposeHeaders = "Location";
            return CreatedAtAction(nameof(GetRoll), new { campaignId = campaignId, rollId = result.Id , sceneId  = sceneId }, result);
        }        
        [HttpPost("{sceneId}/creatures/{creatureId}/roll-simulations")]
        public async Task<IActionResult> SimulateCd([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] SimulateCdInput input)
        {
            var result = await _creatureService.SimulateCd(campaignId, sceneId, creatureId, input);
            return Ok(result);
        }       
        [HttpPost("{sceneId}/creatures/{creatureId}/damage")]
        public async Task<IActionResult> TakeDamage([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] UpdateLifeInput input)
        {
            await _creatureActionsService.TakeDamage(campaignId, sceneId, creatureId, input);
            return Ok();
        }      
        [HttpPost("{sceneId}/creatures/{creatureId}/heal")]
        public async Task<IActionResult> Heal([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] UpdateLifeInput input)
        {
            await _creatureActionsService.Heal(campaignId, sceneId, creatureId, input);
            return Ok();
        }

    }
}
