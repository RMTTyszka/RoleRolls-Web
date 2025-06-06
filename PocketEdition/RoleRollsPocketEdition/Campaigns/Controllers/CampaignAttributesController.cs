﻿using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core.Authentication;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Controllers
{
        [Authorize]
    [ApiController]
    [Route("campaigns/{campaignId}/attributes")]
    public class CampaignAttributesController : ControllerBase
    {

        private readonly ICampaignsService _campaignsService;

        public CampaignAttributesController(ICampaignsService campaignsService)
        {
            _campaignsService = campaignsService;
        }


        [HttpPost("")]
        public async Task<IActionResult> AddAttribute([FromRoute] Guid campaignId, [FromBody] AttributeTemplateModel attribute)
        {
            await _campaignsService.AddAttribute(campaignId, attribute);
            return Ok();
        }       
        [HttpDelete("{attributeId}")]
        public async Task<IActionResult> RemoveAttribute([FromRoute] Guid campaignId, [FromRoute] Guid attributeId)
        {
            await _campaignsService.RemoveAttribute(campaignId, attributeId);
            return Ok();
        }
        [HttpPut("{attributeId}")]
        public async Task<IActionResult> UpdateAttribute([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromBody] AttributeTemplateModel attribute)
        {
            await _campaignsService.UpdateAttribute(campaignId, attributeId, attribute);
            return Ok();
        }         
        [HttpPost("{attributeId}/skills")]
        public async Task<IActionResult> AddSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromBody] SkillTemplateModel skill)
        {
            await _campaignsService.AddSkill(campaignId, attributeId, skill);
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
        public async Task<IActionResult> AddMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromBody] SpecificSkillTemplateModel specificSkill)
        {
            await _campaignsService.AddMinorSkillAsync(campaignId, attributeId, skillId, specificSkill);
            return Ok();
        }       
        [HttpDelete("{attributeId}/skills/{skillId}/minor-skills/{minorSkillId}")]
        public async Task<IActionResult> RemoveMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromRoute] Guid minorSkillId)
        {
            await _campaignsService.RemoveMinorSkillAsync(campaignId, attributeId, skillId, minorSkillId);
            return Ok();
        }
        [HttpPut("{attributeId}/skills/{skillId}/minor-skills/{minorSkillId}")]
        public async Task<IActionResult> UpdateMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromRoute] Guid minorSkillId, [FromBody] SpecificSkillTemplateModel specificSkill)
        {
            await _campaignsService.UpdateMinorSkillAsync(campaignId, attributeId, skillId, minorSkillId, specificSkill);
            return Ok();
        }
    }
}
