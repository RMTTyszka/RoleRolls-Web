using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Models;

namespace RoleRollsPocketEdition.Creatures.Application.Controllers
{
    [Route("campaigns/{campaignId}/creatures")]
    public class CreaturesController : ControllerBase
    {

        private readonly ICreatureService _creatureService;

        public CreaturesController(ICreatureService creatureService)
        {
            _creatureService = creatureService;
        }

        [HttpGet("creatureId")]
        public async Task<CreatureModel> GetAsync([FromRoute] Guid campaignId, [FromRoute] Guid creatureId)
        {
            var creature = await _creatureService.GetAsync(creatureId);
            return creature;
        }    
        [HttpGet("")]
        public async Task<List<CreatureModel>> GetListAsync([FromRoute] Guid campaignId, [FromQuery] GetAllCampaignCreaturesInput input)
        {
            var creatures = await _creatureService.GetAllAsync(campaignId, input);
            return creatures;
        }     
        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromRoute] Guid campaignId, [FromBody] CreatureModel creatureModel)
        {
            var creature = await _creatureService.CreateAsync(campaignId, creatureModel);
            return CreatedAtAction(nameof(GetAsync), new { campaignId = campaignId, creatureId = creature.Id }, creature);
        }      
        [HttpPost("creatureId")]
        public async Task UpdateAsync([FromRoute] Guid campaignId, [FromRoute] Guid creatureId, [FromBody] CreatureModel creatureModel)
        {
            await _creatureService.UpdateAsync(creatureId, creatureModel);
        }
    }
}
