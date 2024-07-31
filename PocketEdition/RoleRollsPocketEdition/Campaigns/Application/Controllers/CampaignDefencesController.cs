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
    [Route("campaigns/{campaignId}/defenses")]
    public class CampaignDefencesController : ControllerBase
    {

        private readonly ICampaignsService _campaignsService;

        public CampaignDefencesController(ICampaignsService campaignsService)
        {
            _campaignsService = campaignsService;
        }


        [HttpPost("")]
        public async Task<IActionResult> AddDefense([FromRoute] Guid campaignId, [FromBody] DefenseTemplateModel defense)
        {
            await _campaignsService.AddDefense(campaignId, defense);
            return Ok();
        }
        [HttpDelete("{defenseId}")]
        public async Task<IActionResult> RemoveDefense([FromRoute] Guid campaignId, [FromRoute] Guid defenseId)
        {
            await _campaignsService.RemoveDefense(campaignId, defenseId);
            return Ok();
        }
        [HttpPut("{defenseId}")]
        public async Task<IActionResult> UpdateDefense([FromRoute] Guid campaignId, [FromRoute] Guid defenseId, [FromBody] DefenseTemplateModel defense)
        {
            await _campaignsService.UpdateDefense(campaignId, defenseId, defense);
            return Ok();
        }

    }
}
