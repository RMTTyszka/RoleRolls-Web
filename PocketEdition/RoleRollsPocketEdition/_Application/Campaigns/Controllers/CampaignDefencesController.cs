using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Application.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Authentication;

namespace RoleRollsPocketEdition.Application.Campaigns.Controllers
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
