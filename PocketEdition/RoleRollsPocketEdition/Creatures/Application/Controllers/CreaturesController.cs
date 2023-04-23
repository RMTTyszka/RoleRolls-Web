using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Models;

namespace RoleRollsPocketEdition.Creatures.Application.Controllers
{
    [Route("campaigns/{campaignId}/creatures")]
    [ApiController]
    public class CreaturesController : ControllerBase
    {

        private readonly ICreatureService _creatureService;

        public CreaturesController(ICreatureService creatureService)
        {
            _creatureService = creatureService;
        }

        [HttpGet("{creatureId}")]
        public async Task<ActionResult<CreatureModel>> GetAsync([FromRoute] Guid campaignId, [FromRoute] Guid creatureId)
        {
            var creature = await _creatureService.GetAsync(creatureId);
            if (creature is null)
            {
                return NotFound();
            }

            return Ok(creature);
        }    
        [HttpGet("")]
        public async Task<List<CreatureModel>> GetListAsync([FromRoute] Guid campaignId, [FromQuery] GetAllCampaignCreaturesInput input)
        {
            var creatures = await _creatureService.GetAllAsync(campaignId, input);
            return creatures;
        }     
        [HttpGet("new")]
        public async Task<ActionResult<CreatureModel>> NewAsync([FromRoute] Guid campaignId)
        {
            var creature = await _creatureService.InstantiateFromTemplate(campaignId);
            return Ok(creature);
        }  
        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromRoute] Guid campaignId, [FromBody] CreatureModel creatureModel)
        {
            var result = await _creatureService.CreateAsync(campaignId, creatureModel);
            if (result.Validation == CreatureUpdateValidation.Ok)
            {
                Response.Headers.AccessControlAllowHeaders = "Location";
                Response.Headers.AccessControlExposeHeaders = "Location";
                return CreatedAtAction(nameof(GetAsync), new { campaignId = campaignId, creatureId = result.Creature.Id }, null); 
            }

            return new UnprocessableEntityObjectResult(result);

        }      
        [HttpPut("{creatureId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid campaignId, [FromRoute] Guid creatureId, [FromBody] CreatureModel creatureModel)
        {
            var result = await _creatureService.UpdateAsync(creatureId, creatureModel);
            if (result.Validation == CreatureUpdateValidation.Ok)
            {
                return Ok();
            }

            return new UnprocessableEntityObjectResult(result);
        }
    }
}
