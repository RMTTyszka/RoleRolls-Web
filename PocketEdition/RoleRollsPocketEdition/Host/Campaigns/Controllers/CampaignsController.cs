using Microsoft.AspNetCore.Mvc;
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
    [Route("campaigns")]
    public class CampaignsController : ControllerBase
    {

        private readonly ICampaignsService _campaignsService;
        private readonly ICreatureTemplateService _creatureTemplateService;
        private readonly ICreatureService _creatureService;
        private readonly IRollService _rollService;

        public CampaignsController(ICampaignsService campaignsService, ICreatureTemplateService creatureTemplateService, IRollService rollService, ICreatureService creatureService)
        {
            _campaignsService = campaignsService;
            _creatureTemplateService = creatureTemplateService;
            _rollService = rollService;
            _creatureService = creatureService;
        }

        [HttpGet("{campaignId}")]
        public async Task<CampaignModel> GetAsync(Guid campaignId)
        {
            return await _campaignsService.GetAsync(campaignId);
        }    
        [HttpGet()]
        public async Task<PagedResult<CampaignModel>> GetListAsync([FromQuery] PagedRequestInput input)
        {
            return await _campaignsService.GetListAsync(input);
        }
        [HttpPost("")]
        public Task Create([FromBody] CampaignModel template)
        {
            return _campaignsService.CreateAsync(template);
        }
        [HttpPut("{campaignId}")]
        public async Task<IActionResult> Update([FromRoute] Guid campaignId, [FromBody] CampaignModel campaignModel)
        {
            if (!campaignModel.CreatureTemplateId.HasValue)
            {
                return new BadRequestObjectResult("Missing creatureTemplateId");
            }
            await _campaignsService.UpdateAsync(campaignModel);
            await _creatureTemplateService.UpdateAsync(campaignModel.CreatureTemplateId.Value, campaignModel.CreatureTemplate);
            return Ok();
        }
        [HttpDelete("{campaignId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid campaignId)
        {
            await _campaignsService.DeleteAsync(campaignId);
            return Ok();
        }

        [HttpGet("{id}/scenes/{sceneId}/rolls")]
        public async Task<IActionResult> GetRolls([FromRoute] Guid sceneId, [FromRoute] Guid id, [FromQuery] PagedRequestInput input)
        {
            var result = await _rollService.GetAsync(id, sceneId, input);
            return Ok(result);
        }      
        [HttpGet("{campaignId}/scenes/{sceneId}/rolls/{rollId}")]
        public async Task<IActionResult> GetRoll([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid rollId, [FromQuery] PagedRequestInput input)
        {
            var result = await _rollService.GetAsync(campaignId, sceneId, rollId);
            if (result is null) 
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("{campaignId}/scenes/{sceneId}/rolls")]
        public async Task<IActionResult> RollDice([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] RollInput input)
        {
            var result = await _rollService.RollAsync(campaignId, sceneId, input);
            Response.Headers.AccessControlAllowHeaders = "Location";
            Response.Headers.AccessControlExposeHeaders = "Location";
            return CreatedAtAction(nameof(GetRoll), new { campaignId = campaignId, id = result.Id, sceneId = sceneId }, result);
        }    
        [HttpPost("{campaignId}/scenes/{sceneId}/creatures/{creatureId}/rolls")]
        public async Task<IActionResult> RollDiceForCreature([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] RollInput input)
        {
            var result = await _rollService.RollAsync(campaignId, sceneId, creatureId, input);
            Response.Headers.AccessControlAllowHeaders = "Location";
            Response.Headers.AccessControlExposeHeaders = "Location";
            return CreatedAtAction(nameof(GetRoll), new { campaignId = campaignId, rollId = result.Id , sceneId  = sceneId }, result);
        }        
        [HttpPost("{campaignId}/scenes/{sceneId}/creatures/{creatureId}/roll-simulations")]
        public async Task<IActionResult> SimulateCd([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] SimulateCdInput input)
        {
            var result = await _creatureService.SimulateCd(campaignId, sceneId, creatureId, input);
            return Ok(result);
        }       
        [HttpPost("{campaignId}/scenes/{sceneId}/creatures/{creatureId}/damage")]
        public async Task<IActionResult> TakeDamage([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] UpdateLifeInput input)
        {
            await _creatureService.TakeDamage(campaignId, sceneId, creatureId, input);
            return Ok();
        }      
        [HttpPost("{campaignId}/scenes/{sceneId}/creatures/{creatureId}/heal")]
        public async Task<IActionResult> Heal([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] UpdateLifeInput input)
        {
            await _creatureService.Heal(campaignId, sceneId, creatureId, input);
            return Ok();
        }

    }
}
