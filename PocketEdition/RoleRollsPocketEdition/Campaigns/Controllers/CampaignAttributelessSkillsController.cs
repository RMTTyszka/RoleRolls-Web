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
            await _campaignsService.AddSkill(campaignId, null, skill);
            return Ok();
        }       
        [HttpDelete("{attributeId}/skills/{skillId}")]
        public async Task<IActionResult> RemoveSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId)
        {
            await _campaignsService.RemoveSkill(campaignId, attributeId, skillId);
            return Ok();
        }
        [HttpPut("{attributeId}/skills/{skillId}")]
        public async Task<IActionResult> UpdateSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromBody] SkillTemplateModel skill)
        {
            await _campaignsService.UpdateSkill(campaignId, attributeId, skillId, skill);
            return Ok();
        }       
        [HttpPost("{attributeId}/skills/{skillId}/minor-skills")]
        public async Task<IActionResult> AddMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromBody] MinorSkillTemplateModel minorSkill)
        {
            await _campaignsService.AddMinorSkillAsync(campaignId, attributeId, skillId, minorSkill);
            return Ok();
        }       
        [HttpDelete("{attributeId}/skills/{skillId}/minor-skills/{minorSkillId}")]
        public async Task<IActionResult> RemoveMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromRoute] Guid minorSkillId)
        {
            await _campaignsService.RemoveMinorSkillAsync(campaignId, attributeId, skillId, minorSkillId);
            return Ok();
        }
        [HttpPut("{attributeId}/skills/{skillId}/minor-skills/{minorSkillId}")]
        public async Task<IActionResult> UpdateMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromRoute] Guid minorSkillId, [FromBody] MinorSkillTemplateModel minorSkill)
        {
            await _campaignsService.UpdateMinorSkillAsync(campaignId, attributeId, skillId, minorSkillId, minorSkill);
            return Ok();
        }
    }
}
