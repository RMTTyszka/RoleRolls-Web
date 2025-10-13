using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core.Authentication;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Controllers
{
        [Authorize]
    [ApiController]
    [Route("campaigns/{campaignId}/attributeless-skills")]
    public class CampaignAttributelessSkillsController : ControllerBase
    {

        private readonly ICampaignsService _campaignsService;

        public CampaignAttributelessSkillsController(ICampaignsService campaignsService)
        {
            _campaignsService = campaignsService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddSkill([FromRoute] Guid campaignId,[FromBody] SkillTemplateModel skill)
        {
            await _campaignsService.AddSkill(campaignId, skill);
            return Ok();
        }       
        [HttpDelete("{skillId}")]
        public async Task<IActionResult> RemoveSkill([FromRoute] Guid campaignId, [FromRoute] Guid skillId)
        {
            await _campaignsService.RemoveSkill(campaignId, skillId);
            return Ok();
        }
        [HttpPut("{skillId}")]
        public async Task<IActionResult> UpdateSkill([FromRoute] Guid campaignId, [FromRoute] Guid skillId, [FromBody] SkillTemplateModel skill)
        {
            await _campaignsService.UpdateSkill(campaignId, skillId, skill);
            return Ok();
        }       
        [HttpPost("{skillId}/minor-skills")]
        public async Task<IActionResult> AddMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid skillId, [FromBody] SpecificSkillTemplateModel specificSkill)
        {
            await _campaignsService.AddMinorSkillAsync(campaignId, skillId, specificSkill);
            return Ok();
        }       
        [HttpDelete("{skillId}/minor-skills/{minorSkillId}")]
        public async Task<IActionResult> RemoveMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid skillId, [FromRoute] Guid minorSkillId)
        {
            await _campaignsService.RemoveMinorSkillAsync(campaignId, skillId, minorSkillId);
            return Ok();
        }
        [HttpPut("{skillId}/minor-skills/{minorSkillId}")]
        public async Task<IActionResult> UpdateMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid skillId, [FromRoute] Guid minorSkillId, [FromBody] SpecificSkillTemplateModel specificSkill)
        {
            await _campaignsService.UpdateMinorSkillAsync(campaignId, skillId, minorSkillId, specificSkill);
            return Ok();
        }
    }
}

