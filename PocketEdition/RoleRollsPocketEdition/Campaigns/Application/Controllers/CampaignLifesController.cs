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
    [Route("campaigns/{campaignId}/lifes")]
    public class CampaignLifesController : ControllerBase
    {

        private readonly ICampaignsService _campaignsService;

        public CampaignLifesController(ICampaignsService campaignsService)
        {
            _campaignsService = campaignsService;
        }


        [HttpPost("lifes")]
        public async Task<IActionResult> AddLife([FromRoute] Guid campaignId, [FromBody] LifeTemplateModel life)
        {
            await _campaignsService.AddLife(campaignId, life);
            return Ok();
        }
        [HttpDelete("{lifeId}")]
        public async Task<IActionResult> RemoveLife([FromRoute] Guid campaignId, [FromRoute] Guid lifeId)
        {
            await _campaignsService.RemoveLife(campaignId, lifeId);
            return Ok();
        }
        [HttpPut("{lifeId}")]
        public async Task<IActionResult> UpdateLife([FromRoute] Guid campaignId, [FromRoute] Guid lifeId, [FromBody] LifeTemplateModel life)
        {
            await _campaignsService.UpdateLife(campaignId, lifeId, life);
            return Ok();
        }

    }
}
