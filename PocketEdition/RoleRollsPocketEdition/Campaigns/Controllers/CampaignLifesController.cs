using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core.Authentication;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Controllers
{
        [Authorize]
    [ApiController]
    [Route("campaigns/{campaignId}/vitalities")]
    public class CampaignVitalitiesController : ControllerBase
    {

        private readonly ICampaignsService _campaignsService;

        public CampaignVitalitiesController(ICampaignsService campaignsService)
        {
            _campaignsService = campaignsService;
        }


        [HttpPost("")]
        public async Task<IActionResult> AddVitality([FromRoute] Guid campaignId, [FromBody] VitalityTemplateModel vitality)
        {
            await _campaignsService.AddVitality(campaignId, vitality);
            return Ok();
        }
        [HttpDelete("{vitalityId}")]
        public async Task<IActionResult> RemoveVitality([FromRoute] Guid campaignId, [FromRoute] Guid vitalityId)
        {
            await _campaignsService.RemoveVitality(campaignId, vitalityId);
            return Ok();
        }
        [HttpPut("{vitalityId}")]
        public async Task<IActionResult> UpdateVitality([FromRoute] Guid campaignId, [FromRoute] Guid vitalityId, [FromBody] VitalityTemplateModel vitality)
        {
            await _campaignsService.UpdateVitality(campaignId, vitalityId, vitality);
            return Ok();
        }

    }
}
