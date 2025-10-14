using Microsoft.AspNetCore.Mvc;
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
    }
}
